using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters
{
    public class PlayerPanelSettingsPresenter :
             BasePresenter<IPlayerPanelSettingsView, PlayerPanelSettings>
    {
        private PlayerPanelSettings playerPanelSetting;

        public PlayerPanelSettingsPresenter(
                IApplicationController controller,
                IPlayerPanelSettingsView playerPanelSettingsView) :
                base(controller, playerPanelSettingsView)
        {
            View.ChangePlayerPanelSettings += () =>
                         ChangePlayerPanelSettings(
                                    View.PlayerPanelSettings);
        }

        public override void Run(PlayerPanelSettings argument)
        {
            playerPanelSetting = argument;
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
            playerPanelSetting.PlayerWidth = 
                            newSettings.PlayerWidth;
            playerPanelSetting.PlayerHeight = 
                            newSettings.PlayerHeight;
            playerPanelSetting.NumberOfPlayers = 
                            newSettings.NumberOfPlayers;
            playerPanelSetting.AutosizeMode = 
                            newSettings.AutosizeMode;
            View.Close();
        }

        /// <summary>
        /// fill the fields with current settings in view
        /// </summary>
        private void SetPlayerPanelSettings()
        {
            View.SetPlayerPanelSettings(playerPanelSetting);
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
