using Vatsim.Vatis.UI.Dialogs;

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
    VersionCheckDialog CreateVersionCheckDialog();
    MiniDisplayForm CreateMiniDisplayForm();
}