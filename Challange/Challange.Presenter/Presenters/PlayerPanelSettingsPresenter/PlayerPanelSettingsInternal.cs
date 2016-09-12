using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
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
        /// <summary>
        /// fill the fields with current settings in view
        /// </summary>
        private void SetPlayerPanelSettings()
        {
            View.SetPlayerPanelSettings(playerPanelSettings);
        }

        /// <summary>
        /// save new settings into file with settings
        /// </summary>
        /// <param name="newSettings"></param>
        private void SaveSettings(PlayerPanelSettings newSettings)
        {
            var playerPanelSettingsService =
                    new SettingsService<PlayerPanelSettings>(
                    new PlayerPanelSettingsParser());
            playerPanelSettingsService.SaveSetting(newSettings);
        }
    }
}
