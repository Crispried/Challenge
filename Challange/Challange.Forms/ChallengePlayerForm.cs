using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Presenter.Views;
using Challange.Presenter.Views.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class ChallengePlayerForm : Form, IChallengePlayerView, IPlayerLayout
    {
        private IPlayerLayout playerLayout;

        public ChallengePlayerForm(IPlayerLayout playerLayout)
        {
            InitializeComponent();
            this.playerLayout = playerLayout;
            BindForm(this);
        }

        public event Action<string> OpenBroadcastForm;

        // Temporary solution
        public void DrawPlayers(int numberOfPlayers, Control challengePlayerPanel)
        {

        }
        //

        public void BindForm(Form form)
        {
            playerLayout.BindForm(form);
        }

        public void DrawPlayers(int numberOfPlayers)
        {
            playerLayout.DrawPlayers(numberOfPlayers, challengePlayerPanel);
        }

        public void InitializePlayers(Dictionary<string, Bitmap> initialData)
        {
            playerLayout.InitializePlayers(initialData);
        }

        public void ShowMessage(ChallengeMessage message)
        {
            MessageBox.Show(message.Text, message.Caption,
                message.MessageButtons, message.MessageIcon);
        }

        private void MaximizeWindowState()
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
