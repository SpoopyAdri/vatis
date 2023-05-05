using System;
using System.Collections.Generic;
using Vatsim.Network;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.UI;

namespace Vatsim.Vatis.Config;

public interface IAppConfig
{
    string UserId { get; set; }
    string Password { get; set; }
    NetworkRating NetworkRating { get; set; }
    string Name { get; set; }
    List<NetworkServerInfo> CachedServers { get; set; }
    string ServerName { get; set; }
    bool SuppressNotifications { get; set; }
    bool ConfigRequired { get; }
    WindowProperties WindowProperties { get; set; }
    List<Profile> Profiles { get; set; }
    Profile CurrentProfile { get; set; }
    Composite CurrentComposite { get; set; }
    string MicrophoneDevice { get; set; }
    string PlaybackDevice { get; set; }
    List<VoiceMetaData> Voices { get; set; }
    void LoadConfig(string path);
    void SaveConfig();
}