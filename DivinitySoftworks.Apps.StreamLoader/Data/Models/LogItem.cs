using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using System;

namespace DivinitySoftworks.Apps.StreamLoader.Data.Models {
    public class LogItem : ViewModel {

        public LogItem() {

        }

        State _state = State.Initial;
        public State State {
            get {
                return _state;
            }
            set {
                ChangeAndNotify(ref _state, value);
            }
        }

        string _message = string.Empty;
        public string Message {
            get {
                return _message;
            }
            set {
                ChangeAndNotify(ref _message, value);
            }
        }

        string _details = string.Empty;
        public string Details {
            get {
                return _details;
            }
            set {
                ChangeAndNotify(ref _details, value);
            }
        }

        DateTime _dateTime = DateTime.Now;
        public DateTime DateTime {
            get {
                return _dateTime;
            }
            set {
                ChangeAndNotify(ref _dateTime, value);
            }
        }
    }
}
