using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Challange.Presenter.Views;
using Challange.Domain.SettingsService.SettingTypes;
using AForge.Video.DirectShow;
using AForge.Video;

namespace Challange.Forms
{
    public partial class MainForm : Form, IMainView
    {
        private readonly ApplicationContext context;
        private int numberOfPlayers;
        private const int autosizeWidthCoefficient = 5;
        private const int autosizeHeightCoefficient = 3;
        private Timer timer;
        private List<PictureBox> allPlayers;

        //
        private PictureBox draggedPicturebox;
        bool bordered = false;
        int firstPanelOnChangeIndex, secondPanelOnChangeIndex;
        PictureBox tmp;

        // video streaming
        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo;

        public MainForm(ApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
            playerPanelSettings.Click += (sender, args) =>
                            Invoke(OpenPlayerPanelSettings);
            FinalVideo = new VideoCaptureDevice();
            allPlayers = new List<PictureBox>();
        }

        #region events
        private void startStreamButton_Click(object sender, EventArgs e)
        {
            InitializeTimer();
            StartStream();
        }

        private void stopStreamButton_Click(object sender, EventArgs e)
        {
            StopStream();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            challangeTimeAxis.UpdateTimeAxis();
            elapsedTimeFromStart.Text = challangeTimeAxis.ElapsedTimeFromStart;
        }

        private void addChallange_Click(object sender, EventArgs e)
        {
            challangeTimeAxis.CreateMarker();
        }

        private void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            foreach (var player in allPlayers)
            {
                player.Image = video;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalVideo.IsRunning)
            {
                FinalVideo.Stop();
            }
        }

        private void PlayerPanel_Click(object sender, EventArgs e)
        {
            var clickedPictureBox = sender as PictureBox;
            if (!bordered)
            {
                tmp = clickedPictureBox;
                firstPanelOnChangeIndex = playerPanel.Controls.GetChildIndex(clickedPictureBox);
                Cursor = Cursors.NoMove2D;
                draggedPicturebox = sender as PictureBox;
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    allPlayers.ElementAt(i).BorderStyle = BorderStyle.Fixed3D;
                }
            }
            else
            {
                secondPanelOnChangeIndex = playerPanel.Controls.GetChildIndex(clickedPictureBox);
                playerPanel.Controls.SetChildIndex(tmp, secondPanelOnChangeIndex);
                playerPanel.Controls.SetChildIndex(clickedPictureBox, firstPanelOnChangeIndex);
                Cursor = Cursors.Default;
                draggedPicturebox = sender as PictureBox;
                var allPlayers = playerPanel.Controls.OfType<PictureBox>();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    allPlayers.ElementAt(i).BorderStyle = BorderStyle.None;
                }
            }
            bordered = !bordered;
        }

        public event Action OpenPlayerPanelSettings;
        #endregion

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += new EventHandler(Timer_Tick);
        }     

        private void ResetTimeAxis()
        {
            timer.Stop();
            timer.Dispose();
            challangeTimeAxis.Reset();
            elapsedTimeFromStart.ResetText();
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
            newPictureBox.Controls.Add(new Label() { Text = playerName });
            newPictureBox.Click += new EventHandler(PlayerPanel_Click);
            newPictureBox.Tag = "playerPanel";
            return newPictureBox;
        }
        #endregion


        private void StartStream()
        {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (VideoCaptureDevices.Count > 0)
            {
                FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[0].MonikerString);
                FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
                FinalVideo.Start();
            }
        }    
        
        private void StopStream()
        {
            if (FinalVideo.IsRunning)
            {
                FinalVideo.Stop();
                foreach (var player in allPlayers)
                {
                    player.Image = null;
                }
                ResetTimeAxis();
            }
        }  
    }
}
