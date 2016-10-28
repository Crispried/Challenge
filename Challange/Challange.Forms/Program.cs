using System;
using System.Windows.Forms;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Presenter.Presenters;
using PylonC.NET;
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Presenter.Views.Layouts;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Replay;

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
           // try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var controller = new ApplicationController(new LightInjectAdapter())

                                .RegisterView<IMainView, MainForm>()
                                .RegisterLayoutToForm<IPlayerLayout, MainForm>()
                                .RegisterLayoutToImplementation<IPlayerLayout, PlayerLayout>()
                                .RegisterView<IPlayerPanelSettingsView, PlayerPanelSettingsForm>()
                                .RegisterView<IChallengeSettingsView, ChallangeSettingsForm>()
                                .RegisterView<IGameInformationView, GameInformationForm>()
                                .RegisterView<ICamerasView, CamerasForm>()
                                .RegisterView<IChallengePlayerView, ChallengePlayerForm>()
                                .RegisterView<IRewindSettingsView, RewindSettingsForm>()
                                .RegisterView<IBroadcastView, BroadcastForm>()
                                .RegisterView<IFtpSettingsView, FtpSettingsForm>()
                                .RegisterService<IFileWorker, FileWorker>()
                                .RegisterService<IFileService, FileService>()
                                .RegisterService<IPathFormatter, PathFormatter>()
                                .RegisterService<IMessageParser, MessageParser>()
                                .RegisterService<IZoomCalculator, ZoomCalculator>()
                                .RegisterInstance(new ApplicationContext());

                controller.Run<MainPresenter>();
            }
            //catch
            //{
            //    Pylon.Terminate();
            //    throw;
            //}
            Pylon.Terminate();
        }
    }
}
