using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Vatsim.Network;

namespace Vatsim.Vatis.Config;

public class AppConfig : IAppConfig
{
    [JsonIgnore] public string AppPath { get; }

    [JsonIgnore] public string ConfigPath { get; }

    [JsonIgnore] public bool ConfigRequired => string.IsNullOrEmpty(VatsimId) || 
                                               string.IsNullOrEmpty(VatsimPasswordDecrypted) || string.IsNullOrEmpty(Name);

    [JsonIgnore]
    public string VatsimPasswordDecrypted
    {
        get => Decrypt(VatsimPassword);
        set => VatsimPassword = Encrypt(value);
    }

    [JsonIgnore] public Profile CurrentProfile { get; set; }

    [JsonIgnore] public AtisComposite CurrentComposite { get; set; }

    public string VatsimId { get; set; }

    public string VatsimPassword { get; set; }

    public NetworkRating NetworkRating { get; set; }

    public List<NetworkServerInfo> CachedServers { get; set; }

    public string Name { get; set; }

    public string ServerName { get; set; }

    public bool SuppressNotifications { get; set; }

    public WindowProperties WindowProperties { get; set; }

    public WindowProperties ProfileListWindowProperties { get; set; }

    public WindowProperties MiniDisplayWindowProperties { get; set; }

    public List<Profile> Profiles { get; set; }

    public string MicrophoneDevice { get; set; }

    public string PlaybackDevice { get; set; }

    public AppConfig()
    {
        AppPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        ConfigPath = Path.Combine(AppPath, "AppConfig.json");
        Profiles = new List<Profile>();

        try
        {
            LoadConfig(ConfigPath);
        }
        catch (FileNotFoundException)
        {
            SaveConfig();
        }
        catch (Exception)
        {
            SaveConfig();
        }
    }

    public void LoadConfig(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs);
        JsonConvert.PopulateObject(sr.ReadToEnd(), this);

        ValidateConfig();
    }

    public void SaveConfig()
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new OrderedContractResolver(),
        };
        File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(this, Formatting.Indented, jsonSerializerSettings));
    }

    private void ValidateConfig()
    {
        foreach (var composite in Profiles.SelectMany(x => x.Composites))
        {
            if (composite.UseFaaFormat)
            {
                composite.UseTransitionLevelPrefix = false;
                composite.UseMetricUnits = false;
                composite.UseSurfaceWindPrefix = false;
                composite.UseVisibilitySuffix = false;
            }
        }
        SaveConfig();
    }

    private string Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";
        var desCryptoProvider = TripleDES.Create();
        var hashMD5Provider = MD5.Create();

        byte[] byteHash;
        byte[] byteBuff;

        byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey));
        desCryptoProvider.Key = byteHash;
        desCryptoProvider.Mode = CipherMode.ECB;
        byteBuff = Encoding.UTF8.GetBytes(value);

        return Convert.ToBase64String(desCryptoProvider.CreateEncryptor()
            .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
    }

    private string Decrypt(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";
        var desCryptoProvider = TripleDES.Create();
        var hashMD5Provider = MD5.Create();

        byte[] byteHash;
        byte[] byteBuff;

        byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey));
        desCryptoProvider.Key = byteHash;
        desCryptoProvider.Mode = CipherMode.ECB;
        byteBuff = Convert.FromBase64String(value);

        return Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor()
            .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
    }

    private static string EncryptionKey => new Guid(0xa1afdec5, 0x1f5c, 0x498b, 0xb4, 0xf1, 0x83, 0x49, 0x50, 0xfb, 0xea, 0xf0).ToString();

    private class OrderedContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var @base = base.CreateProperties(type, memberSerialization);
            var ordered = @base
                .OrderBy(p => p.Order ?? int.MaxValue)
                .ThenBy(p => p.PropertyName)
                .ToList();
            return ordered;
        }
    }
}