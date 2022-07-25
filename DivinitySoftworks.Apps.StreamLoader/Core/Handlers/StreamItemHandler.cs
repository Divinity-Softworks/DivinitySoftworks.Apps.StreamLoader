using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using DotNetTools.SharpGrabber;
using DotNetTools.SharpGrabber.Grabbed;
using MediatR;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.StreamLoader.Core.Handlers {
    public class StreamItemHandler : IRequestHandler<StreamItemRequest, StreamItemResponse> {
        readonly HttpClient _httpClient;
        readonly Progress<float> _progress = new();
        readonly Guid _identifier = Guid.NewGuid();

        StreamItemRequest? _request;

        public StreamItemHandler() {
            _httpClient = new HttpClient();

            _progress.ProgressChanged += Progress_ProgressChanged;
        }

        public async Task<StreamItemResponse> Handle(StreamItemRequest request, CancellationToken cancellationToken) {
            try {
                if (request is null) throw new ArgumentNullException(nameof(request));

                _request = request;
                IMultiGrabber grabber = GrabberBuilder.New()
                  .UseDefaultServices()
                  .AddYouTube()
                  .Build();


                GrabResult result = await grabber.GrabAsync(_request.Uri, cancellationToken);
                _request.Name = result.Title;

                GrabbedMedia[] sortedMediaFiles = result
                    .Resources<GrabbedMedia>()
                    .OrderByResolutionDescending()
                    .ThenByBitRateDescending()
                    .ToArray();

                GrabbedMedia media = sortedMediaFiles.GetHighestQualityAudio();
                if (media is null) {
                    _request.Completed = DateTime.Now;
                    return new StreamItemResponse();
                }
                using FileStream file = new(Path.Combine(_request.Folder, $"{result.Title}.{media.Container}"), FileMode.Create, FileAccess.Write, FileShare.None);

                await _httpClient.DownloadAsync(media.ResourceUri, file, _progress, cancellationToken);
            }
            catch (Exception exception) {
                if (_request is not null) {
                    _request.ErrorMessage = exception.Message;
                    _request.State = Data.Enums.State.Error;
                }
            }
            finally {
                if (_request is not null) {
                    _request.Completed = DateTime.Now;
                    _request.State = Data.Enums.State.Success;
                }
            }

            return new StreamItemResponse();
        }

        private void Progress_ProgressChanged(object? sender, float e) {
            if (_request is null) return;

            if (_request.State == Data.Enums.State.Initial)
                _request.State = Data.Enums.State.Info;

            _request.Progress = e;


            Debug.WriteLine($"{_identifier} : {e}%");
        }
    }
}
