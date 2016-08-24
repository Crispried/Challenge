using System;
using System.Windows.Forms;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Presenter.Presenters;
using PylonC.NET;

namespace Challange.Forms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Pylon.Initialize();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var controller = new ApplicationController(new LightInjectAdapter())
                                .RegisterView<IMainView, MainForm>()
                                .RegisterView<IPlayerPanelSettingsView, PlayerPanelSettingsForm>()
                                .RegisterView<IChallengeSettingsView, ChallangeSettingsForm>()
                                .RegisterView<IGameInformationView, GameInformationForm>()
                                .RegisterView<ICamerasView, CamerasForm>()
                                .RegisterInstance(new ApplicationContext());

                controller.Run<MainPresenter>();
            }
            catch
            {
                Pylon.Terminate();
                throw;
            }
            Pylon.Terminate();
        }
    }
}
