using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Forms.Widgets;
using Challange.Presenter.Views;
using Challange.Presenter.Views.Layouts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class ChallengePlayerForm : Form, IChallengePlayerView
    {
        private PlayerPanel _playerPanel;

        private ZoomData _zoomData;

        public ChallengePlayerForm()
        {
            InitializeComponent();
            startButton.Click += (sender, args)
                            => Invoke(StartAllPlayers);
            stopButton.Click += (sender, args)
                            => Invoke(StopAllPlayers);
            rewindBackward.Click += (sender, args)
                            => Invoke(RewindBackward);
            rewindForward.Click += (sender, args)
                            => Invoke(RewindForward);
            FormClosing += (sender, args)
                            => Invoke(OnFormClosing);
            _playerPanel = new PlayerPanel(this, _zoomData, false, true, true, false);
        }

        public event Action<string> OpenBroadcastForm;

        public event Action StartAllPlayers;

        public event Action StopAllPlayers;

        public event Action RewindBackward;

        public event Action RewindForward;

        public new event Action OnFormClosing;
        
        public int PlaybackSpeed
        {
            get
            {
                return speedBar.Value;
            }
        }

        public void DrawPlayers(int numberOfPlayers, Dictionary<string, Bitmap> initialData)
        {
            _playerPanel.DrawPlayers(numberOfPlayers,
                new PlayerPanelSettings() { AutosizeMode = true }, initialData);
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
            _playerPanel.UpdatePlayerImage(videoName, frame);
        }

        //public void RedrawZoomedImage(ZoomData zoomData)
        //{
        //    this.zoomData = zoomData;
        //    pictureBoxToShowFullscreen.Refresh();
        //}

        public void ShowMessage(ChallengeMessage message)
        {
            MessageBox.Show(message.Text, message.Caption,
                message.MessageButtons, message.MessageIcon);
        }
    }
}