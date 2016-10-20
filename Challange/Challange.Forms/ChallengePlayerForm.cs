using Challange.Domain.Services.Message;
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
    public partial class ChallengePlayerForm : Form, IChallengePlayerView
    {
        public ChallengePlayerForm()
        {
            InitializeComponent();
            rewindOnSetting.Click += (sender, args)
                            => Invoke(OpenRewindSettings);
        }

        public event Action OpenRewindSettings;

        public void ShowMessage(ChallengeMessage message)
        {
            MessageBox.Show(message.Text, message.Caption,
                message.MessageButtons, message.MessageIcon);
        }
    }
}
