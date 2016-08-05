﻿using System;
using System.Windows.Forms;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Presenter.Presenters;

namespace Challange.Forms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var controller = new ApplicationController(new LightInjectAdapter())
                            .RegisterView<IMainView, MainForm>()
                            .RegisterView<IPlayerPanelSettingsView, PlayerPanelSettingsForm>()
                            .RegisterInstance(new ApplicationContext());

            controller.Run<MainPresenter>();
        }
    }
}
