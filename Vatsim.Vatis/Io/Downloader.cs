using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Vatsim.Vatis.Io;

public class Downloader : IDownloader
{
    private readonly HttpClient mHttpClient;
    private const int BufferSize = 131072;

    public Downloader()
    {
        mHttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(20)
        };

        var productVersion =
            GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            throw new ApplicationException("AssemblyInformationalVersionAttribute not found");

        mHttpClient.DefaultRequestHeaders.Add("User-Agent", "Vatsim.Vatis/" + productVersion);
    }

    public async Task<string> DownloadStringAsync(string url)
    {
        var result = await mHttpClient.GetAsync(url);
        result.EnsureSuccessStatusCode();
        return await result.Content.ReadAsStringAsync();
    }

    public async Task DownloadFileAsync(string url, string path, IProgress<int> progress)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? throw new IOException("Destination path is null"));
        await using var fileStream =
            new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, BufferSize, useAsync: true);
        await DownloadToStreamAsync(url, fileStream, progress);
    }

    public async Task<byte[]> DownloadBytesAsync(string url, IProgress<int> progress)
    {
        using var stream = new MemoryStream();
        await DownloadToStreamAsync(url, stream, progress);
        return stream.ToArray();
    }

    private async Task DownloadToStreamAsync(string url, Stream stream, IProgress<int>? progress)
    {
        var response = await mHttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength ?? -1;
        var canReportProgress = totalBytes != -1 && progress != null;
        long nextProgressReportTime = 250;
        var stopWatch = Stopwatch.StartNew();

        await using (var contentStream = await response.Content.ReadAsStreamAsync())
        {
            long totalBytesRead = 0;
            var buffer = new byte[BufferSize];
            var hasMoreToRead = true;
            do
            {
                var bytesRead = await contentStream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                if (bytesRead == 0)
                {
                    hasMoreToRead = false;
                    continue;
                }

                await stream.WriteAsync(buffer.AsMemory(0, bytesRead));
                totalBytesRead += bytesRead;

                if (!canReportProgress)
                {
                    continue;
                }

                var elapsedMs = stopWatch.ElapsedMilliseconds;
                if (elapsedMs < nextProgressReportTime) continue;
                if (progress != null)
                {
                    var percent = (int)(totalBytesRead / (double)totalBytes * 100.0);
                    progress.Report(percent);
                }

                nextProgressReportTime = elapsedMs + 250;
            } while (hasMoreToRead);

            if (canReportProgress && progress != null)
            {
                var percent = (int)(totalBytesRead / (double)totalBytes * 100.0);
                progress.Report(percent);
            }
        }

        stopWatch.Stop();
    }
}