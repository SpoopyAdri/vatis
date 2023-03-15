using System;
using System.Globalization;
using System.Management;

namespace Vatsim.Vatis.Utils;

public static class SystemIdentifier
{
    private static string mSystemDriveVolumeId = "";
    public static string GetSystemDriveVolumeId()
    {
        if (mSystemDriveVolumeId == "")
        {
            string environmentVariable = Environment.GetEnvironmentVariable("SystemDrive");
            mSystemDriveVolumeId = int.Parse(new ManagementObject($"win32_logicaldisk.deviceid=\"{environmentVariable.ToUpper()}\"").Properties["VolumeSerialNumber"].Value.ToString(), NumberStyles.HexNumber).ToString();
        }
        return mSystemDriveVolumeId;
    }
}