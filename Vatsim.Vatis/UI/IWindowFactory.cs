using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.UI.Startup;

namespace Vatsim.Vatis.UI;

public interface IWindowFactory
{
    AirportConditionsDialog CreateAirportConditionsDialog();
    NewCompositeDialog CreateNewCompositeDialog();
    NotamDefinitionsDialog CreateNotamDefinitionsDialog();
    RecordAtisDialog CreateRecordAtisDialog();
    ProfileListDialog CreateProfileListDialog();
    SettingsDialog CreateSettingsDialog();
    TextDefinitionDialog CreateTextDefinitionDialog();
    UserInputDialog CreateUserInputDialog();
    MainForm CreateMainForm();
    ProfileConfigurationForm CreateProfileConfigurationForm();
    MiniDisplayForm CreateMiniDisplayForm();
    StartupWindow CreateStartupWindow();
}