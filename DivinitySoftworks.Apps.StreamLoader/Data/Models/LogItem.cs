using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using System;

namespace DivinitySoftworks.Apps.StreamLoader.Data.Models {
    public class LogItem {
        public State State { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
