using Appccelerate.EventBroker;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.UI;
using Vatsim.Vatis.UI.Dialogs;

namespace Vatsim.Vatis.Container;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IEventBroker>().To(typeof(EventBroker)).InSingletonScope();
        Bind<IAppConfig>().To(typeof(AppConfig)).InSingletonScope();
        Bind<IWindowFactory>().ToFactory();
        // mContainer.RegisterSingleton<IUserInterface, UserInterfaceFactory>();
        // mContainer.RegisterSingleton<IEventBroker>(() => new EventBroker());
        // mContainer.RegisterSingleton<IAppConfig, AppConfig>();
        // mContainer.RegisterSingleton<IProfileEditorConfig, ProfileEditorConfig>();
        // mContainer.RegisterSingleton<IVersionCheck, VersionCheck>();
        // mContainer.RegisterSingleton<INavaidDatabase, NavaidDatabase>();
        // mContainer.RegisterSingleton<IAudioManager, AudioManager>();
        // mContainer.RegisterSingleton<ITextToSpeechRequest, TextToSpeechRequest>();
        // mContainer.RegisterSingleton<IAtisBuilder, AtisBuilder>();
        // mContainer.RegisterSingleton<IDownloader, Downloader>();
    }
}