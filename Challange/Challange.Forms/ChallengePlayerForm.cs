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
    public partial class ChallengePlayerForm : Form, IChallengePlayerView, IChallengePlayerFormLayout
    {
        private IChallengePlayerFormLayout layout;

        public ChallengePlayerForm(IChallengePlayerFormLayout layout)
        {
            InitializeComponent();
            this.layout = layout;
            layout.ChallengePlayerPanel = challengePlayerPanel;
            layout.Form = this;
        }

        // Unnecessary
        private Form form;

        public event Action<string> OpenBroadcastForm;

        public Form Form
        {
            get
            {
                return form;
            }
            set
            {
                form = value;
            }
        }

        public FlowLayoutPanel ChallengePlayerPanel
        {
            get
            {
                return challengePlayerPanel;
            }
            set
            {
                challengePlayerPanel = value;
            }
        }
        //

        public void DrawPlayers(int numberOfPlayers)
        {
            layout.DrawPlayers(numberOfPlayers);
        }

        //public void RedrawZoomedImage(ZoomData zoomData)
        //{
        //    this.zoomData = zoomData;
        //    pictureBoxToShowFullscreen.Refresh();
        //}

        // Was IChallengePlayerView.InitializePlayers
        public void InitializePlayers(Dictionary<string, Bitmap> initialData)
        {
            layout.InitializePlayers(initialData);
        }

        public void ShowMessage(ChallengeMessage message)
        {
            MessageBox.Show(message.Text, message.Caption,
                message.MessageButtons, message.MessageIcon);
        }
    }
}