using System.Collections.Generic;

namespace Vatsim.Vatis.Config;

public interface IProfileEditorConfig : IConfig
{
    string AppPath { get; }
    List<AtisComposite> Composites { get; set; }
    string WorkingDirectory { get; set; }
    void LoadConfig(string path);
    void SaveConfig();
}