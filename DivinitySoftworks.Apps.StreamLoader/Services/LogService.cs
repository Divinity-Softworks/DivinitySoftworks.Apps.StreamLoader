using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using System.Collections.ObjectModel;

namespace DivinitySoftworks.Apps.StreamLoader.Services {

    public interface ILogService {

        public FixedObservableCollection<LogItem> LogItems { get; set; }

        void LogInfo(string message, string details);

        void LogWarning(string message, string details);

        void LogError(string message, string details);

        void LogSuccess(string message, string details);
    }

    public class LogService : ILogService {
        static int _maxItems = 50;

        public LogService() { }

        public FixedObservableCollection<LogItem> LogItems { get; set; } = new FixedObservableCollection<LogItem>(_maxItems);

        private void Log(State state, string message, string details) {
            LogItems.Add(new LogItem() { 
                State = state,
                Message = message,
                Details = details,
                DateTime = System.DateTime.Now
            });
        }

        public void LogError(string message, string details) {
            Log(State.Error, message, details);
        }

        public void LogInfo(string message, string details) {
            Log(State.Info, message, details);
        }

        public void LogSuccess(string message, string details) {
            Log(State.Success, message, details);
        }

        public void LogWarning(string message, string details) {
            Log(State.Warning, message, details);
        }
    }
}
