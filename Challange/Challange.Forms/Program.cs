using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Presenter.Presenters;
using Challange.Domain.Abstract;
using Challange.Domain.Concrete;
using Challange.Domain.Entities;
using Challange.Domain.SettingsService.SettingTypes;
using Challange.Domain.SettingsService;

namespace Challange.Forms
{
    static class Program
    {
        public static readonly ApplicationContext Context = new ApplicationContext();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var controller = new ApplicationController(new LightInjectAdapter())
                            .RegisterView<IMainView, MainForm>()
                            .RegisterView<IPlayerPanelSettingsView, PlayerPanelSettingss>()
                            .RegisterService<IExampleService, ExampleService>()
                            .RegisterInstance(new ApplicationContext());

            controller.Run<MainPresenter>();
        }
    }
}
