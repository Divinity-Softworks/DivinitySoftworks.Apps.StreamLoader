using System.Collections.ObjectModel;

namespace DivinitySoftworks.Apps.StreamLoader.Services {

    public interface ILogService {

        public ObservableCollection<LogItem> LogItems { get; set; }

        void LogInfo(string message, string details);

        void LogWarning(string message, string details);

        void LogError(string message, string details);

        void LogSuccess(string message, string details);
    }

    public class LogService : ILogService {
        public ObservableCollection<LogItem> LogItems {
            get; set;
        }

        public void LogError(string message, string details) {
            throw new System.NotImplementedException();
        }

        public void LogInfo(string message, string details) {
            throw new System.NotImplementedException();
        }

        public void LogSuccess(string message, string details) {
            throw new System.NotImplementedException();
        }

        public void LogWarning(string message, string details) {
            throw new System.NotImplementedException();
        }
    }
}
