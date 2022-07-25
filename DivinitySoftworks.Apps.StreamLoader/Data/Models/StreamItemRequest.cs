using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels.Base;
using MediatR;
using System;

namespace DivinitySoftworks.Apps.StreamLoader.Data.Models {
    public class StreamItemRequest : ViewModel, IRequest<StreamItemResponse> {

        #region Constructors

        public StreamItemRequest(Uri uri, string folder) {
            Uri = uri;
            Folder = folder;
        }

        #endregion Constructors

        #region Properties

        public Uri Uri { get; private set; }

        State _state = State.Initial;
        public State State {
            get {
                return _state;
            }
            set {
                ChangeAndNotify(ref _state, value);
            }
        }

        string _name = "Pending...";
        public string Name {
            get { 
                return _name;
            }
            set { 
                ChangeAndNotify(ref _name, value);
            } 
        }

        public string Folder { get; private set; }

        float _progress = 0.0f;
        public float Progress {
            get {
                return _progress;
            }
            set {
                ChangeAndNotify(ref _progress, value);
            }
        }

        DateTime? _start;
        public DateTime? Start {
            get {
                return _start;
            }
            set {
                ChangeAndNotify(ref _start, value);
            }
        }

        DateTime? _completed;
        public DateTime? Completed {
            get {
                return _completed;
            }
            set {
                ChangeAndNotify(ref _completed, value);
            }
        }

        TimeSpan? _elapsed;
        public TimeSpan? Elapsed {
            get {
                return _elapsed;
            }
            set {
                ChangeAndNotify(ref _elapsed, value);
            }
        }

        string _errorMessage = string.Empty;
        public string ErrorMessage {
            get {
                return _errorMessage;
            }
            set {
                ChangeAndNotify(ref _errorMessage, value);
            }
        }

        #endregion Properties
    }
}
