using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using DivinitySoftworks.Apps.StreamLoader.Services;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels.Base;
using System;
using System.Collections.ObjectModel;

namespace DivinitySoftworks.Apps.StreamLoader.UI.ViewModels {

    public interface IYouTubePageViewModel {
        string Url { get; set; }

        void AddStreamItem(string url);
        void AddStreamItem(Uri uri);
    }

    internal class YouTubePageViewModel : ViewModel, IYouTubePageViewModel {
        readonly IStreamService _streamService;

        public YouTubePageViewModel(IStreamService streamService) {
            _streamService = streamService;
        }

        ObservableCollection<StreamItemRequest> _streamItems = new();
        public ObservableCollection<StreamItemRequest> StreamItems {
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

        public void AddStreamItem(string url) {
            _streamItems.Add(_streamService.Add(new Uri(url)));
        }
        public void AddStreamItem(Uri uri) {
            _streamItems.Add(_streamService.Add(uri));
        }
    }
}
