using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Entities;
using Challange.Domain.SettingsService.SettingTypes;
using Challange.Domain.SettingsService;
using Challange.Domain.SettingsService.SettingParser;

namespace Challange.Presenter.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private PlayerPanelSettings playerPanelSettings;

        public MainPresenter(IApplicationController controller,
                             IMainView mainView) : 
                             base(controller, mainView)
        {
            View.OpenPlayerPanelSettings +=
                                    ChangePlayerPanelSettings;
        }

        public override void Run()
        {
            playerPanelSettings = GetPlayerPanelSettings();
            DrawPlayers();
            View.Show();
        }

        private void ChangePlayerPanelSettings()
        {
            Controller.Run<PlayerPanelSettingsPresenter,
                           PlayerPanelSettings>(playerPanelSettings);
            DrawPlayers();
        }

        private void DrawPlayers()
        {
            if (playerPanelSettings != null)
            {
                View.DrawPlayers(playerPanelSettings);
            }
        }

        /// <summary>
        /// read player panel settings from xml file
        /// this action occures only when we run our form
        /// </summary>
        /// <returns></returns>
        private PlayerPanelSettings GetPlayerPanelSettings()
        {
            var playerPanelSettingService =
                 new SettingsService<PlayerPanelSettings>(
                                new PlayerPanelSettingsParser());
            return playerPanelSettingService.
                        GetSetting();
        }
    }
}
