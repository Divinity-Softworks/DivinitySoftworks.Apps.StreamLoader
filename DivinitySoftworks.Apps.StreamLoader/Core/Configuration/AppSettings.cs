namespace DivinitySoftworks.Apps.StreamLoader.Core.Configuration {

    public interface IAppSettings {
        public string Folder { get; set; }
    }

    public class AppSettings : IAppSettings {
        public string Folder { get; set; } = string.Empty;
    }
}
