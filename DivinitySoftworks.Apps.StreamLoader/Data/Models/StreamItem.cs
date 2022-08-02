using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DivinitySoftworks.Apps.StreamLoader.Data.Models {
    public class StreamItem : ViewModel, IRequest<object> {
        readonly Dictionary<int, float> _progresses = new();

        #region Constructors

        public StreamItem(Uri uri, string folder, StreamType streamType, string fileType) {
            Uri = uri;
            Folder = folder;
            StreamType = streamType;
            FileType = fileType;
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; } = Guid.NewGuid();

        public Uri Uri { get; private set; }

        public StreamType StreamType { get; set; }

        public string FileType { get; set; }

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

        internal void UpdateProgress(int id, float progress) {
            if (!_progresses.ContainsKey(id))
                _progresses.Add(id, progress);
            else _progresses[id] = progress;

            Progress = _progresses.Sum(x => x.Value) / _progresses.Count;
        }

        #endregion Properties
    }
}
