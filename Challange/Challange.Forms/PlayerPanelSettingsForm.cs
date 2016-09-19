using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Views;
using System;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class PlayerPanelSettingsForm :
                                Form, IPlayerPanelSettingsView
    {
        public PlayerPanelSettingsForm()
        {
            InitializeComponent();
            savePlayerPanelSettingsButton.Click += (sender, args)
                            => Invoke(ChangePlayerPanelSettings);
        }

        public event Action ChangePlayerPanelSettings;

        public new void Show()
        {
            ShowDialog();
        }

        public PlayerPanelSettings PlayerPanelSettings
        {
            get
            {
                return GetSettings();
            }
            set
            {
                var playerPanelSettings = value;         
            }
        }

        private PlayerPanelSettings GetSettings()
        {
            var playerPanelSettings = new PlayerPanelSettings()
            {
                PlayerHeight =
                    Convert.ToInt32(playerHeightTextBox.Text),
                PlayerWidth =
                    Convert.ToInt32(playerWidthTextBox.Text),
                NumberOfPlayers =
                    Convert.ToInt32(numberOfPlayersTextBox.Text),
                AutosizeMode = autosizeCheckButton.Checked
            };
            return playerPanelSettings;
        }

        public void SetPlayerPanelSettings(PlayerPanelSettings playerPanelSetting)
        {
            SetSettingsView(playerPanelSetting);
        }

        private void SetSettingsView(PlayerPanelSettings playerPanelSetting)
        {
            playerHeightTextBox.Text =
                playerPanelSetting.PlayerHeight.ToString();
            playerWidthTextBox.Text =
                playerPanelSetting.PlayerWidth.ToString();
            numberOfPlayersTextBox.Text =
                playerPanelSetting.NumberOfPlayers.ToString();
            autosizeCheckButton.Checked =
                playerPanelSetting.AutosizeMode;
        }

        private void autosizeCheckButton_CheckedChanged(object sender, EventArgs e)
        {
            if (autosizeCheckButton.Checked)
            {
                // playerHeightLabel.Hide();
                // playerHeightTextBox.Hide();
                playerHeightTextBox.Enabled = false;
                //  playerWidthLabel.Hide();
                playerWidthTextBox.Enabled = false;
                // playerWidthTextBox.Hide();
            }
            else
            {
                //  playerHeightLabel.Show();
                //playerHeightTextBox.Show();
                playerHeightTextBox.Enabled = true;
                //  playerWidthLabel.Show();
                //playerWidthTextBox.Show();
                playerWidthTextBox.Enabled = true;
            }
        }
    }
}
