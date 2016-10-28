using Challange.Domain.Services.Message;
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
                            => Invoke(ChangePlayerPanelSettings, GetSettings());
        }

        public event Action<PlayerPanelSettings> ChangePlayerPanelSettings;

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

        public bool ValidateForm()
        {
            if (FormFieldsAreValid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FormFieldsAreValid()
        {
            return (!string.IsNullOrWhiteSpace(playerHeightTextBox.Text)) &&
                   (!string.IsNullOrWhiteSpace(playerWidthTextBox.Text));
        }

        public void ShowMessage(ChallengeMessage message)
        {
            string caption = message.Caption;
            string text = message.Text;
            MessageBox.Show(text, caption,
                message.MessageButtons, message.MessageIcon);
        }

        private void playerWidthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void playerHeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }
    }
}
