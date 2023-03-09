using System.IO;

namespace Vatsim.Vatis.Io;

public static class PathProvider
{
    private static string mInstallPath = "";
    public static string LogsFolderPath => Path.Combine(mInstallPath, "Logs");
    public static string SoundsFolderPath => Path.Combine(mInstallPath, "Sounds");
    public static void SetInstallPath(string path)
    {
        mInstallPath = path;
    }
}