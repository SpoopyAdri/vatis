using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Vatsim.Vatis.Io;

public class Downloader : IDownloader
{
    private readonly HttpClient mHttpClient;
    private const int BufferSize = 131072;

    public Downloader()
    {
        mHttpClient = new HttpClient(new SocketsHttpHandler()
        {
            // Force HttpClient to use IPv4 address
            ConnectCallback = async (context, cancellationToken) =>
            {
                // Use DNS to look up the IP address of the target host
                // SocketException is thrown if there is no IP address for the host
                var entry = await Dns.GetHostEntryAsync(context.DnsEndPoint.Host, AddressFamily.InterNetwork, cancellationToken);

                // Open the connection to the target host/port and disable Nagle's algorithm
                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.NoDelay = true;

                try
                {
                    await socket.ConnectAsync(entry.AddressList, context.DnsEndPoint.Port, cancellationToken);
                    return new NetworkStream(socket, true);
                }
                catch
                {
                    socket.Dispose();
                    throw;
                }
            }
        });

        mHttpClient.Timeout = TimeSpan.FromSeconds(20);

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

    public async Task<Stream> PostJsonDownloadAsync(string url, object content, CancellationToken? cancellationToken = null)
    {
        var response = await mHttpClient.PostAsJsonAsync(url, content, 
            cancellationToken.GetValueOrDefault());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<T> PostJsonAsyncResponse<T>(string url, object content, CancellationToken? cancellationToken = null)
    {
        var response = await mHttpClient.PostAsJsonAsync(url, content,
            cancellationToken.GetValueOrDefault());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task PostJsonAsync(string url, object content, CancellationToken? cancellationToken = null)
    {
        var response = await mHttpClient.PostAsJsonAsync(url, content,
            cancellationToken.GetValueOrDefault());
        response.EnsureSuccessStatusCode();
    }

    public async Task<T> DownloadJsonStringAsync<T>(string url)
    {
        var response = await mHttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
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