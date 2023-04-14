using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CredentialManagement;
using Newtonsoft.Json;
using Vatsim.Network;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.UI;

namespace Vatsim.Vatis.Config;

public class AppConfig : IAppConfig
{
    [JsonIgnore] 
    public bool ConfigRequired => string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name);

    [JsonIgnore]
    public string UserId { get; set; } = "";

    [JsonIgnore]
    public string Password { get; set; } = "";

    [JsonIgnore] 
    public Profile CurrentProfile { get; set; }

    [JsonIgnore] 
    public Composite CurrentComposite { get; set; }

    [JsonIgnore]
    public string AuthToken { get; set; }

    [JsonIgnore]
    public DateTime AuthTokenGeneratedAt { get; set; }

    public string Name { get; set; }

    public NetworkRating NetworkRating { get; set; }

    public string ServerName { get; set; }

    public bool SuppressNotifications { get; set; }

    public string MicrophoneDevice { get; set; }

    public string PlaybackDevice { get; set; }

    public WindowProperties WindowProperties { get; set; }

    public List<Profile> Profiles { get; set; }

    public List<VoiceMetaData> Voices { get; set; }

    public List<NetworkServerInfo> CachedServers { get; set; }

    public AppConfig()
    {
        Profiles = new List<Profile>();
        CachedServers = new List<NetworkServerInfo>();

        try
        {
            LoadConfig(PathProvider.AppConfigFilePath);
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

        using (var credentialSet = new CredentialSet("org.vatsim.vatis"))
        {
            credentialSet.Load();
            if (credentialSet.Count > 0)
            {
                UserId = credentialSet[0].Username;
                Password = credentialSet[0].Password;
            }
        }

        ValidateConfig();
    }

    public void SaveConfig()
    {
        using (var credentials = new Credential(UserId, Password, "org.vatsim.vatis", CredentialType.Generic))
        {
            credentials.PersistanceType = PersistanceType.LocalComputer;
            credentials.Save();
        }

        File.WriteAllText(PathProvider.AppConfigFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
    }

    private void ValidateConfig()
    {
        foreach (var composite in Profiles.SelectMany(x => x.Composites))
        {
            if (!char.IsLetter(composite.CodeRange.Low))
            {
                composite.CodeRange.Low = 'A';
            }

            if (!char.IsLetter(composite.CodeRange.High))
            {
                composite.CodeRange.Low = 'Z';
            }

            if (char.ToLower(composite.CodeRange.High) < char.ToLower(composite.CodeRange.Low))
            {
                composite.CodeRange.Low = 'A';
                composite.CodeRange.High = 'Z';
            }
        }

        SaveConfig();
    }
}