using System;

namespace Vatsim.Vatis.Updates;

public class ClientVersionInfo
{
    public Version LatestVersion { get; set; }
    public string LatestProductVersion { get; set; }
    public string LatestVersionUrl { get; set; }
}