using System;
using System.Threading.Tasks;

namespace Vatsim.Vatis.Io;

public interface IDownloader
{
    Task<string> DownloadStringAsync(string url);

    Task DownloadFileAsync(string url, string path, IProgress<int> progress);

    Task<byte[]> DownloadBytesAsync(string url, IProgress<int> progress);
}