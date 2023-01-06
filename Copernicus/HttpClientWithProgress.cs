using System;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json;
using System.Net.Http;

namespace Copernicus
{
    public class HttpClientWithProgress : HttpClient
    {
        public string? downloadUrl { get; private set; }
        public string destinationPath { get; set; } = "";
        public string authorization { get; set; } = "";

        public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);
        public event ProgressChangedHandler? ProgressChanged;

        public HttpClientWithProgress(string url, string destination, string auth)
        {
            this.downloadUrl = url;
            this.destinationPath = destination;
            this.authorization = auth;
        }

        public async Task StartDownload()
        {
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Add("Authorization", this.authorization);
            Timeout = TimeSpan.FromMinutes(5);

            using (var response = await GetAsync(this.downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                await DownloadFileFromHttpResponseMessage(response);
        }

        public async Task StartDownload(CancellationToken cancellationToken)
        {
            using (var response = await GetAsync(this.downloadUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                await DownloadFileFromHttpResponseMessage(response);
        }

        private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            long? totalBytes = response.Content.Headers.ContentLength;
            using (var contentStream = await response.Content.ReadAsStreamAsync())
                await ProcessContentStream(totalBytes, contentStream);
        }

        private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
        {
            long totalBytesRead = 0L;
            long readCount = 0L;
            byte[] buffer = new byte[8192];
            bool isMoreToRead = true;

            using (FileStream fileStream = new FileStream(this.destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                do
                {
                    int bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        continue;
                    }

                    await fileStream.WriteAsync(buffer, 0, bytesRead);

                    totalBytesRead += bytesRead;
                    readCount += 1;

                    if (readCount % 10 == 0)
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                }
                while (isMoreToRead);
            }
            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
        }

        private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
        {
            if (ProgressChanged == null)
                return;
            
            double? progressPercentage = null;
            if (totalDownloadSize.HasValue)
                progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

            ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
        }

    }
}

