using System.Collections.Generic;
using Vatsim.Network;

namespace Vatsim.Vatis.Config;

public interface IAppConfig : IConfig
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
    WindowProperties ProfileListWindowProperties { get; set; }
    WindowProperties MiniDisplayWindowProperties { get; set; }
    List<Profile> Profiles { get; set; }
    Profile CurrentProfile { get; set; }
    AtisComposite CurrentComposite { get; set; }
    string MicrophoneDevice { get; set; }
    string PlaybackDevice { get; set; }
    void LoadConfig(string path);
    void SaveConfig();
}