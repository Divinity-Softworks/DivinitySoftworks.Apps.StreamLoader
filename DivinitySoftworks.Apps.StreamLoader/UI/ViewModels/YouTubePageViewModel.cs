using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using DivinitySoftworks.Apps.StreamLoader.Services;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.StreamLoader.UI.ViewModels {

    public interface IYouTubePageViewModel {
        string Url { get; set; }

        StreamType StreamType { get; set; }

        Task AddStreamItemAsync(string url, StreamType streamType);

        Task AddStreamItemAsync(Uri uri, StreamType streamType);
    }

    internal class YouTubePageViewModel : ViewModel, IYouTubePageViewModel {
        readonly IStreamService _streamService;

        public YouTubePageViewModel(IStreamService streamService) {
            _streamService = streamService;
        }

        ObservableCollection<StreamItem> _streamItems = new();
        public ObservableCollection<StreamItem> StreamItems {
            get {
                return _streamItems;
            }
            set {
                ChangeAndNotify(ref _streamItems, value);
            }
        }

        public string _url = string.Empty;
        public string Url {
            get {
                return _url;
            }
            set {
                ChangeAndNotify(ref _url, value);
            }
        }

        public StreamType _streamType = StreamType.Audio;
        public StreamType StreamType {
            get {
                return _streamType;
            }
            set {
                ChangeAndNotify(ref _streamType, value);
            }
        }

        public async Task AddStreamItemAsync(string url, StreamType streamType) {
            _streamItems.Add(await _streamService.AddAsync(new Uri(url), streamType));
        }

        public async Task AddStreamItemAsync(Uri uri, StreamType streamType) {
            _streamItems.Add(await _streamService.AddAsync(uri, streamType));
        }
    }
}
