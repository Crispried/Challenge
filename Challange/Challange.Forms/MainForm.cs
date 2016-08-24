using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings.SettingTypes;
using System.Threading.Tasks;
using System.Threading;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Challange.Forms
{
    public partial class MainForm : Form, IMainView
    {
        private readonly ApplicationContext context;
        private int numberOfPlayers;
        private const int autosizeWidthCoefficient = 5;
        private const int autosizeHeightCoefficient = 3;
        private System.Windows.Forms.Timer timer;
        private List<PictureBox> allPlayers;

        private Bitmap currentFrame;

        public MainForm(ApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
            playerPanelSettings.Click += (sender, args) =>
                            Invoke(OpenPlayerPanelSettings);
            challangeSettings.Click += (sender, args) =>
                            Invoke(OpenChallengeSettings);
            startStreamButton.Click += (sender, args) =>
                            Invoke(StartStream);
            stopStreamButton.Click += (sender, args) =>
                            Invoke(StopStream);
            openGameFolderButton.Click += (sender, args) =>
                            Invoke(OpenGameFolder);
            FormClosing += (sender, args) =>
                            Invoke(MainFormClosing);
            addChallange.Click += (sender, args) =>
                            Invoke(CreateChallange);
            openDevicesListButton.Click += (sender, args) =>
                            Invoke(OpenDevicesList);
            allPlayers = new List<PictureBox>();
        }

        public Bitmap CurrentFrame
        {
            get
            {
                return currentFrame;
            }
        }

        public string GetElapsedTime
        {
            get
            {
                return elapsedTimeFromStart.Text;
            }
        }

        #region events
        private void OnTimedEvent(Object source, EventArgs myEventArgs)
        {
            challangeTimeAxis.UpdateTimeAxis();
            elapsedTimeFromStart.Text = challangeTimeAxis.ElapsedTimeFromStart;
        }

        private void TimerCallback(object state)
        {
            challangeTimeAxis.UpdateTimeAxis();
            elapsedTimeFromStart.Text = challangeTimeAxis.ElapsedTimeFromStart;
        }

        private void PlayerPanel_Click(object sender, EventArgs e)
        {
            var clickedPlayer = sender as PictureBox;
            if (!IsReplaceMode())
            {
                firstSelectedPlayer = clickedPlayer;
                SetCursor(Cursors.NoMove2D);
                SetBorderStyle(BorderStyle.Fixed3D);
            }
            else
            {
                secondSelecterPlayer = clickedPlayer;
                ReplacePlayers();
                SetCursor(Cursors.Default);
                SetBorderStyle(BorderStyle.None);
            }
            ToggleReplaceMode();
        }

        private void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            currentFrame = video;
            foreach (var player in allPlayers)
            {
                player.Image = video;
            }
            Invoke(NewFrameCallback);
        }

        public event Action OpenPlayerPanelSettings;

        public event Action OpenChallengeSettings;

        public event Action OpenDevicesList;

        public event Action StartStream;

        public event Action StopStream;

        public event Action OpenGameFolder;

        public event Action MainFormClosing;

        public event Action CreateChallange;

        public event Action NewFrameCallback;
        #endregion

        #region replace players fields
        bool replaceMode = false;
        PictureBox firstSelectedPlayer, secondSelecterPlayer;
        private void ReplacePlayers()
        {
            var firstPlayerIndex = GetPlayerIndex(firstSelectedPlayer);
            var secondPlayerIndex = GetPlayerIndex(secondSelecterPlayer);
            playerPanel.Controls.SetChildIndex(firstSelectedPlayer,
                                            secondPlayerIndex);
            playerPanel.Controls.SetChildIndex(secondSelecterPlayer,
                                            firstPlayerIndex);
        }

        private int GetPlayerIndex(PictureBox player)
        {
            return playerPanel.Controls.GetChildIndex(player);
        }

        private void SetCursor(Cursor cursorType)
        {
            Cursor = cursorType;
        }

        private void SetBorderStyle(BorderStyle borderStyle)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                allPlayers.ElementAt(i).BorderStyle = borderStyle;
            }
        }

        private bool IsReplaceMode()
        {
            if (replaceMode)
            {
                return true;
            }
            return false;
        }

        private void ToggleReplaceMode()
        {
            replaceMode = !replaceMode;
        }
        #endregion


        public void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = 1000;
            Thread.Sleep(1000);
            timer.Start();
        }     

        public void ResetTimeAxis()
        {
            timer.Stop();
            timer.Dispose();
            challangeTimeAxis.Reset();
            elapsedTimeFromStart.ResetText();
        }

        public void SubscribeNewFrameEvent(VideoCaptureDevice finalVideo)
        {
            finalVideo.NewFrame += FinalVideo_NewFrame;
        }

        public new void Show()
        {
            context.MainForm = this;
            Application.Run(context);
        }

        #region DrawPlayers
        public void DrawPlayers(PlayerPanelSettings settings)
        {
            playerPanel.Controls.Clear();
            numberOfPlayers = settings.NumberOfPlayers;
            var playerSize = GetPlayerSize(settings);
            var playerWidth = playerSize.Width;
            var playerHeight = playerSize.Height;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var player = InitializePlayer(playerWidth,
                                    playerHeight, i.ToString());
                DrawPlayer(player);
                AddPlayerIntoPlayerList(player);
            }        
        }

        private Size GetPlayerSize(PlayerPanelSettings settings)
        {
            Size size = new Size();
            if (settings.AutosizeMode)
            {
                size.Width = playerPanel.Width / autosizeWidthCoefficient;
                size.Height = playerPanel.Height / autosizeHeightCoefficient;
            }
            else
            {
                size.Width = settings.PlayerWidth;
                size.Height = settings.PlayerHeight;
            }
            return size;
        }

        private void AddPlayerIntoPlayerList(PictureBox player)
        {
            allPlayers.Add(player);
        }

        private void DrawPlayer(PictureBox player)
        {
            playerPanel.Controls.Add(player);
        }

        private PictureBox InitializePlayer(int playerWidth, int playerHeight,
                                string playerName)
        {
            PictureBox newPictureBox;
            newPictureBox = new PictureBox();
            newPictureBox.BackColor = Color.Red;
            newPictureBox.Height = playerHeight;
            newPictureBox.Width = playerWidth;
            newPictureBox.Controls.Add(new TextBox
            {
                Text = playerName,
                Width = playerWidth,
                MaxLength = 30
            });
            newPictureBox.Click += new EventHandler(PlayerPanel_Click);
            return newPictureBox;
        }
        #endregion

        public void ClearPlayers()
        {
            foreach (var player in allPlayers)
            {
                player.Image = null;
            }
        }

        public void AddMarkerOnTimeAxis()
        {
            challangeTimeAxis.CreateMarker();
        }

        public void ToggleChallengeButton(bool enabled)
        {
            addChallange.Enabled = enabled;
        }

        public void ToggleChallengeButtonIn(bool enabled, int seconds)
        {
            ToggleChallengeButton(false);
            EnableChallangeAfter(seconds);
        }

        private async void EnableChallangeAfter(int seconds)
        {
            await Task.Delay(seconds * 1000);
            if (IsStreaming())
            {
                ToggleChallengeButton(true);
            }
        }

        private bool IsStreaming()
        {
            if (timer.Enabled)
            {
                return true;
            }
            return false;
        }

        public void DrawNewFrame(Bitmap frame)
        {
            foreach (var player in allPlayers)
            {
                player.Image = frame;
            }
            Invoke(NewFrameCallback);
        }
    }
}
