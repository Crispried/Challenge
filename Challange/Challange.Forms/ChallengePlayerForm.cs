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
            startButton.Click += (sender, args)
                            => Invoke(StartAllPlayers);
            stopButton.Click += (sender, args)
                            => Invoke(StopAllPlayers);
            rewindBackward.Click += (sender, args)
                            => Invoke(RewindBackward);
            rewindForward.Click += (sender, args)
                            => Invoke(RewindForward);
            this.FormClosing += (sender, args)
                            => Invoke(OnFormClosing);
        }

        // Unnecessary
        private Form form;

        public event Action<string> OpenBroadcastForm;

        public event Action StartAllPlayers;

        public event Action StopAllPlayers;

        public event Action RewindBackward;

        public event Action RewindForward;

        public new event Action OnFormClosing;

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

        public void DrawNewFrame(Bitmap frame, string videoName)
        {
            Bitmap frameClone = CloneFrame(frame);
            UpdatePlayersImage(videoName, frameClone);
        }

        private Bitmap CloneFrame(Bitmap frame)
        {
            return frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height), frame.PixelFormat);
        }

        public void UpdatePlayersImage(string videoName, Bitmap frame)
        {
            layout.UpdatePlayersImage(videoName, frame);
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