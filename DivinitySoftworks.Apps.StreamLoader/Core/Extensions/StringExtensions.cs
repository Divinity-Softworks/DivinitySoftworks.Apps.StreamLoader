namespace System {
    public static class StringExtensions {
        public static string ToValidFileName(this string fileName) {
            foreach (char c in IO.Path.GetInvalidFileNameChars()) 
                fileName = fileName.Replace(c, '_');
            return fileName;
        }
    }
}
