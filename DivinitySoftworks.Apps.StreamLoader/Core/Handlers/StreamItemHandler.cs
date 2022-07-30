using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using DivinitySoftworks.Apps.StreamLoader.Services;
using DotNetTools.SharpGrabber;
using DotNetTools.SharpGrabber.Converter;
using DotNetTools.SharpGrabber.Grabbed;
using MediaToolkit;
using MediaToolkit.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.StreamLoader.Core.Handlers {
    public class StreamItemHandler : IRequestHandler<StreamItem, object> {
        readonly ILogService _logService;

        readonly HttpClient _httpClient;
        readonly Progress<DownloadProgress> _progress = new();
        readonly Guid _identifier = Guid.NewGuid();
        readonly List<FileStream> _streams = new();

        StreamItem? _streamItem;
        DirectoryInfo? _directoryInfo;

        public StreamItemHandler(ILogService logService) {
            _logService = logService;

            _httpClient = new HttpClient();

            _progress.ProgressChanged += Progress_ProgressChanged;
        }

        public async Task<object> Handle(StreamItem streamItem, CancellationToken cancellationToken) {
            try {
                if (streamItem is null) throw new ArgumentNullException(nameof(streamItem));
                _directoryInfo = new(Path.Combine(streamItem.Folder, "Downloading", $"{streamItem.Id}"));
                if (!_directoryInfo.Exists) _directoryInfo.Create();
                if (_directoryInfo.Parent is not null)
                    _directoryInfo.Parent.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                FFmpeg.AutoGen.ffmpeg.RootPath = @"D:\Development\Divinity Softworks\DivinitySoftworks.Apps.StreamLoader\DivinitySoftworks.Apps.StreamLoader.Libraries\";

                _streamItem = streamItem;
                IMultiGrabber grabber = GrabberBuilder.New()
                  .UseDefaultServices()
                  .AddYouTube()
                  .Build();

                GrabResult result = await grabber.GrabAsync(_streamItem.Uri, cancellationToken);
                _streamItem.Name = result.Title;

                MediaChannels mediaChannels = streamItem.StreamType == Data.Enums.StreamType.Audio
                    ? MediaChannels.Audio
                    : streamItem.StreamType == Data.Enums.StreamType.Movie ? MediaChannels.Both : MediaChannels.None;

                if (mediaChannels == MediaChannels.None)
                    throw new InvalidOperationException($"Unable to download media, invalid Stream Type [{streamItem.StreamType}]!");

                GrabbedMedia[] sortedMediaFiles = result
                    .Resources<GrabbedMedia>()
                    .Where(m => m.Container == streamItem.FileType.ToLower())
                    .OrderByResolutionDescending()
                    .ThenByBitRateDescending()
                    .ToArray();

                List<GrabbedMedia> mediaList = new();
                if (mediaChannels == MediaChannels.Video || mediaChannels == MediaChannels.Both)
                    mediaList.Add(sortedMediaFiles.GetHighestQualityVideo());
                if (mediaChannels == MediaChannels.Audio || mediaChannels == MediaChannels.Both)
                    mediaList.Add(sortedMediaFiles.GetHighestQualityAudio());

                if (mediaList.Count == 0) {
                    _streamItem.Completed = DateTime.Now;
                    _streamItem.State = Data.Enums.State.Error;
                    _logService.LogError("Error!", $"Media was not found for: {_streamItem.Uri}!");
                    return new object();
                }

                List<Task> tasks = new();
                for (int i = 0; i < mediaList.Count; i++) {
                    GrabbedMedia media = mediaList[i];
                    FileStream file = new(Path.Combine(_directoryInfo.FullName, $"{media.Channels}.{media.Container}".ToValidFileName()), FileMode.Create, FileAccess.Write, FileShare.None);
                    _streams.Add(file);
                    tasks.Add(_httpClient.DownloadAsync(media.ResourceUri, file, _progress, i, cancellationToken));
                }

                await Task.WhenAll(tasks);

                _streams.ForEach(stream => {
                    stream.Dispose();
                });


                switch (_streamItem.StreamType) {
                    case Data.Enums.StreamType.Audio:
                        SaveAsAudioFile(_directoryInfo.FullName, Path.Combine(_streamItem.Folder, $"{result.Title}.mp3".ToValidFileName()), mediaList[0]);
                        break;
                    case Data.Enums.StreamType.Movie:
                        SaveAsVideoFile(_directoryInfo.FullName, Path.Combine(_streamItem.Folder, $"{result.Title}.mp4".ToValidFileName()), mediaList[0], mediaList[1]);
                        break;
                    default:
                        throw new NotImplementedException($"The stream type [{_streamItem.StreamType}] is not implemented!");
                }

                _streamItem.State = Data.Enums.State.Success;
                _logService.LogSuccess("Download Finished", $"Media for '{_streamItem.Name}' finished!");
            }
            catch (Exception exception) {
                _logService.LogError("Error!", exception.Message);
                if (_streamItem is not null) {
                    _streamItem.ErrorMessage = exception.Message;
                    _streamItem.State = Data.Enums.State.Error;
                }
            }
            finally {
                if (_streamItem is not null) {
                    _streamItem.Completed = DateTime.Now;
                }
                _streams.ForEach(stream => {
                    stream.Dispose();
                });

                if (_directoryInfo is not null && _directoryInfo.Exists)
                    _directoryInfo.Delete(true);
            }

            return new object();
        }

        private void Progress_ProgressChanged(object? sender, DownloadProgress e) {
            if (_streamItem is null) return;

            if (_streamItem.State == Data.Enums.State.Initial) {
                _streamItem.State = Data.Enums.State.Info;
                _logService.LogInfo("Download Started", $"Media for '{_streamItem.Name}' is now downloading!");
            }

            _streamItem.UpdateProgress(e.Id, e.Progress);

            Debug.WriteLine($"{_identifier} | [{e.Id}] : {_streamItem.Progress}%");
        }

        private void SaveAsAudioFile(string inputDirectory, string fileName, GrabbedMedia audioMedia) {
            if(File.Exists(fileName))
                File.Delete(fileName);
            
            MediaFile inputFile = new(Path.Combine(inputDirectory, $"{audioMedia.Channels}.{audioMedia.Container}".ToValidFileName()));
            MediaFile outputFile = new(fileName);

            using (var engine = new Engine()) {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }

        private void SaveAsVideoFile(string inputDirectory, string fileName, GrabbedMedia videoMedia, GrabbedMedia audioMedia) {
            if (File.Exists(fileName))
                File.Delete(fileName);

            MediaMerger merger = new(fileName) {
                OutputMimeType = videoMedia.Format.Mime,
                OutputShortName = videoMedia.Format.Extension
            };

            merger.AddStreamSource(Path.Combine(inputDirectory, $"{videoMedia.Channels}.{videoMedia.Container}".ToValidFileName()), MediaStreamType.Video);
            merger.AddStreamSource(Path.Combine(inputDirectory, $"{audioMedia.Channels}.{audioMedia.Container}".ToValidFileName()), MediaStreamType.Audio);
            merger.Build();
        }
    }
}
