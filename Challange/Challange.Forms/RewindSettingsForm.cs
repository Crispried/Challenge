using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class RewindSettingsForm : Form, IRewindSettingsView
    {
        public RewindSettingsForm()
        {
            InitializeComponent();
            saveRewindSettings.Click += (sender, args)
                            => Invoke(ChangeRewindSettings, GetSettings());
        }

        public new void Show()
        {
            ShowDialog();
        }

        public event Action<RewindSettings> ChangeRewindSettings;

        private RewindSettings GetSettings()
        {
            var rewindSettings = new RewindSettings()
            {
                Forward =
                    Convert.ToInt32(rewindForwardTextBox.Text),
                Backward =
                    Convert.ToInt32(rewindBackwardTextBox.Text)
            };
            return rewindSettings;
        }

        public void SetRewindSettings(RewindSettings rewindSettings)
        {
            SetRewindView(rewindSettings);
        }

        public void SetRewindView(RewindSettings rewindSettings)
        {
            rewindForwardTextBox.Text =
                rewindSettings.Forward.ToString();
            rewindBackwardTextBox.Text =
                rewindSettings.Backward.ToString();
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
            return (!string.IsNullOrWhiteSpace(rewindForwardTextBox.Text)) &&
                   (!string.IsNullOrWhiteSpace(rewindBackwardTextBox.Text));
        }

        public void ShowValidationErrorMessage()
        {
            string caption = "Form is not valid";
            string text = "Please, fill all fields";
            MessageBox.Show(text, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void rewindForwardTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void rewindBackwardTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }
    }
}
