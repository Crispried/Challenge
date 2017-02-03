using System;
using System.Windows.Forms;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using PylonC.NET;
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Presenter.Presenters.GameInformationPresenter;
using Challange.Presenter.Presenters.BroadcastPresenter;
using Challange.Presenter.Presenters.CamerasPresenter;
using Challange.Presenter.Presenters.ChallengePlayerPresenter;
using Challange.Presenter.Presenters.FtpSettingsPresenter;
using Challange.Presenter.Presenters.ChallengeSettingsPresenter;
using Challange.Presenter.Presenters.PlayerPanelSettingsPresenter;
using Challange.Presenter.Presenters.RewindSettingsPresenter;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.FileSystem.Concrete;
using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.Event.Concrete;
using Challange.Domain.Services.PlayVideo.Abstract;
using Challange.Domain.Services.PlayVideo.Concrete;

namespace Challange.Forms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
#if DEBUG
            /* This is a special debug setting needed only for GigE cameras.
                See 'Building Applications with pylon' in the Programmer's Guide. */
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "5000" /*ms*/);
#endif
            Pylon.Initialize();
          //  try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var autofacAdapter = new AutofacAdapter();

                var controller = new ApplicationController(autofacAdapter)
                                .RegisterView<IMainView, MainForm>()
                                .RegisterView<IPlayerPanelSettingsView, PlayerPanelSettingsForm>()
                                .RegisterView<IChallengeSettingsView, ChallangeSettingsForm>()
                                .RegisterView<IGameInformationView, GameInformationForm>()
                                .RegisterView<ICamerasView, CamerasForm>()
                                .RegisterView<IChallengePlayerView, ChallengePlayerForm>()
                                .RegisterView<IRewindSettingsView, RewindSettingsForm>()
                                .RegisterView<IBroadcastView, BroadcastForm>()
                                .RegisterView<IFtpSettingsView, FtpSettingsForm>()
                                .RegisterService<IXmlWorker, XmlWorker>()
                                .RegisterService<IFileService, FileService>()
                                .RegisterService<IPathFormatter, PathFormatter>()
                                .RegisterService<IMessageParser, MessageParser>()
                                .RegisterService<ISettingsParser<PlayerPanelSettings>, PlayerPanelSettingsParser>()
                                .RegisterService<ISettingsService<PlayerPanelSettings>, SettingsService<PlayerPanelSettings>>()
                                .RegisterService<ISettingsParser<FtpSettings>, FtpSettingsParser>()
                                .RegisterService<ISettingsService<FtpSettings>, SettingsService<FtpSettings>>()
                                .RegisterService<ISettingsParser<ChallengeSettings>, ChallengeSettingsParser>()
                                .RegisterService<ISettingsService<ChallengeSettings>, SettingsService<ChallengeSettings>>()
                                .RegisterService<ISettingsParser<RewindSettings>, RewindSettingsParser>()
                                .RegisterService<ISettingsService<RewindSettings>, SettingsService<RewindSettings>>()
                                .RegisterService<ISettingsContext, SettingsContext>()
                                .RegisterService<INullSettingsContainer, NullSettingsContainer>()
                                .RegisterService<ICamerasProvider, CamerasProvider>()
                                .RegisterService<IDevicesProvider, DevicesProvider>()
                                .RegisterService<ICamerasContainer, CamerasContainer>()
                                .RegisterService<IVideoContainer, VideoContainer>()
                                .RegisterService<IChallengeBuffers, ChallengeBuffers>()
                                .RegisterService<IFpsContainer, FpsContainer>()
                                .RegisterService<IInternalChallengeTimer, InternalChallengeTimer>()
                                .RegisterService<IChallengeObject, ChallengeObject>()
                                .RegisterService<IVideoPlayer, VideoPlayer>()
                                .RegisterService<IChallengeReader, ChallengeReader>()
                                .RegisterService<IChallengeWriter, ChallengeWriter>()
                                .RegisterServiceAsSingleton<IEventSubscriber, EventSubscriber>()
                                .RegisterService<System.Timers.Timer>()
                                .RegisterService<PlayerPanelSettings>()
                                .RegisterService<ChallengeSettings>()
                                .RegisterService<FtpSettings>()
                                .RegisterService<RewindSettings>()
                                .RegisterInstance(new ApplicationContext());


                RegisterPresenters(autofacAdapter);
                autofacAdapter.Build();
                controller.Run<MainPresenter>();
            }
            //catch
            {
            //    Pylon.Terminate();
            //    throw;
            }
            Pylon.Terminate();
        }

        static void RegisterPresenters(IContainer container)
        {
            container.Register<MainPresenter>();
            container.Register<GameInformationPresenter>();
            container.Register<BroadcastPresenter>();
            container.Register<CamerasPresenter>();
            container.Register<ChallengePlayerPresenter>();

            container.Register<FtpSettingsPresenter>();
            container.Register<ChallengeSettingsPresenter>();
            container.Register<PlayerPanelSettingsPresenter>();
            container.Register<RewindSettingsPresenter>();
        }
    }
}
