using Challange.Domain.Services.Message;
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
        public void ChangePlayerPanelSettings(
                        PlayerPanelSettings newSettings)
        {
            if (View.ValidateForm())
            {
                SaveSettings(newSettings);
                playerPanelSettings.SetSettings(newSettings);
                View.Close();
            }
            else
            {
                ChallengeMessage message =
                    messageParser.GetMessage(MessageType.FtpSettingsInvalid);
                View.ShowMessage(message);
            }
        }
    }
}
