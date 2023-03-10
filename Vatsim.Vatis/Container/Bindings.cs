using Ninject.Extensions.Factory;
using Ninject.Modules;
using Vatsim.Vatis.Atis;
using Vatsim.Vatis.AudioForVatsim;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.UI;
using Vatsim.Vatis.Updates;

namespace Vatsim.Vatis.Container;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<VatisAppContext>().ToSelf();
        Bind<IAppConfig>().To(typeof(AppConfig)).InSingletonScope();
        Bind<IClientUpdater>().To(typeof(ClientUpdater)).InSingletonScope();
        Bind<IDownloader>().To(typeof(Downloader)).InSingletonScope();
        Bind<IAtisBuilder>().To(typeof(AtisBuilder)).InSingletonScope();
        Bind<INavaidDatabase>().To(typeof(NavaidDatabase)).InSingletonScope();
        Bind<IAudioManager>().To(typeof(AudioManager)).InSingletonScope();
        Bind<ITextToSpeechRequest>().To(typeof(TextToSpeechRequest)).InSingletonScope();
        Bind<IWindowFactory>().ToFactory();
    }
}