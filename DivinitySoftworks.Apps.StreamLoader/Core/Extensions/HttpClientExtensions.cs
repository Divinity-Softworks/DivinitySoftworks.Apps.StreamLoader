using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http {

    public class DownloadProgress {

        public DownloadProgress(int id, float progress) {
            Id = id;
            Progress = progress;
        }

        public int Id { get; set; }

        public float Progress { get; set; }
    }

    public static class HttpClientExtensions {

        public static async Task DownloadAsync(this HttpClient client, Uri requestUrl, Stream destination, IProgress<DownloadProgress>? progress = null, int id = 0, CancellationToken cancellationToken = default(CancellationToken)) {
            HttpResponseMessage response = await client.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            long? contentLength = response.Content.Headers.ContentLength;

            using Stream download = await response.Content.ReadAsStreamAsync();

            if (progress is null || !contentLength.HasValue) {
                await download.CopyToAsync(destination);
                return;
            }

            Progress<long> progressWrapper = new(totalBytes => progress.Report(new DownloadProgress(id, GetProgressPercentage(totalBytes, contentLength.Value))));
            await download.CopyToAsync(destination, 163840, progressWrapper, cancellationToken);

            float GetProgressPercentage(float totalBytes, float currentBytes) => (totalBytes / currentBytes) * 100f;
        }
    }
}
