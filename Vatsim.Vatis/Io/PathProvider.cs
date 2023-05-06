using System.IO;

namespace Vatsim.Vatis.Io;

public static class PathProvider
{
    private static string mInstallPath = "";
    public static string LogsFolderPath => Path.Combine(mInstallPath, "Logs");
    public static string AppConfigFilePath => Path.Combine(mInstallPath, "AppConfig.json");
    public static string AirportsFilePath => Path.Combine(mInstallPath, "Airports.json");
    public static string NavaidsFilePath => Path.Combine(mInstallPath, "Navaids.json");
    public static string NavDataSerialFilePath => Path.Combine(mInstallPath, "NavDataSerial.json");
    public static void SetInstallPath(string path)
    {
        mInstallPath = path;
    }
}