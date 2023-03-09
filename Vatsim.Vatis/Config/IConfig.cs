namespace Vatsim.Vatis.Config;

public interface IConfig
{
    void LoadConfig(string path);
    void SaveConfig();
}