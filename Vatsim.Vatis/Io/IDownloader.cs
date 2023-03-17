using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Vatsim.Vatis.Io;

public interface IDownloader
{
    Task<string> DownloadStringAsync(string url);

    Task DownloadFileAsync(string url, string path, IProgress<int> progress);

    Task<byte[]> DownloadBytesAsync(string url, IProgress<int> progress);

    Task<Stream> PostJsonDownloadAsync(string url, object content, 
        CancellationToken? cancellationToken = null);

    Task<T> PostJsonAsyncResponse<T>(string url, object content, 
        CancellationToken? cancellationToken = null);

    Task PostJsonAsync(string url, object content,
        CancellationToken? cancellationToken = null);
}