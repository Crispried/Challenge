using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.PlayerPanelSettingsPresenter
{
    public partial class PlayerPanelSettingsPresenter
    {
        public override void Run(PlayerPanelSettings argument)
        {
            playerPanelSettings = argument;
            SetPlayerPanelSettings();
            View.Show();
        }

        /// <summary>
        /// change settings
        /// 1. Calls SaveSettings to save it into file
        /// 2. Change playerPanelSetting fields on new, because
        ///    we want convey it into Main form and change it dynamically
        ///    without necessity read file with settings.
        /// </summary>
        /// <param name="newSettings"></param>
        private void ChangePlayerPanelSettings(
                        PlayerPanelSettings newSettings)
        {
            SaveSettings(newSettings);
            playerPanelSettings.PlayerWidth =
                            newSettings.PlayerWidth;
            playerPanelSettings.PlayerHeight =
                            newSettings.PlayerHeight;
            playerPanelSettings.NumberOfPlayers =
                            newSettings.NumberOfPlayers;
            playerPanelSettings.AutosizeMode =
                            newSettings.AutosizeMode;
            View.Close();
        }
    }
}
