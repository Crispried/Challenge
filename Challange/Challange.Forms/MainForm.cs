using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using ChallangeMarker;
using Challange.Presenter.Views;
using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Message;
using Challange.Presenter.Views.Layouts;
using Challange.Forms.Widgets;

namespace Challange.Forms
{
    public partial class MainForm : Form, IMainView
    {
        private readonly ApplicationContext context;

        private PlayerPanel _playerPanel;
        
        private System.Windows.Forms.Timer timer;

        private ComponentResourceManager resources =
                 new ComponentResourceManager(typeof(MainForm));

        private string currentFrameCameraName;
        private Bitmap currentFrame;

        public MainForm(ApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
            playerPanelSettings.Click += (sender, args) =>
                            Invoke(OpenPlayerPanelSettings);
            challangeSettings.Click += (sender, args) =>
                            Invoke(OpenChallengeSettings);
            ftpSettings.Click += (sender, args) =>
                            Invoke(OpenFtpSettings);
            rewindSettings.Click += (sender, args) =>
                            Invoke(OpenRewindSettings);
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
            viewLastChallengeButton.Click += (sender, args) =>
                            Invoke(OpenChallengePlayerForLastChallenge);
            _playerPanel = new PlayerPanel(this, true, true, true, true);          
        }

        public string CurrentFrameCameraName
        {
            get
            {
                return currentFrameCameraName;
            }
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
            challengeTimeAxis.UpdateTimeAxis();
            elapsedTimeFromStart.Text = challengeTimeAxis.ElapsedTimeFromStart;
        }

        private void TimerCallback(object state)
        {
            challengeTimeAxis.UpdateTimeAxis();
            elapsedTimeFromStart.Text = challengeTimeAxis.ElapsedTimeFromStart;
        }

        #region fullScreen

        //protected override void OnMouseWheel(MouseEventArgs e)
        //{
        //    MouseEventArgs mouseEventArgs = e as MouseEventArgs;
        //    Point mouseLocation = new Point();
        //    Point pictureBoxLocation = new Point();

        //    mouseLocation.X = mouseEventArgs.Location.X;
        //    mouseLocation.Y = mouseEventArgs.Location.Y;

        //    pictureBoxLocation.X = pictureBoxToShowFullscreen.Location.X;
        //    pictureBoxLocation.Y = pictureBoxToShowFullscreen.Location.Y;

        //    Invoke(MakeZoom, pictureBoxLocation, e.Delta, mouseLocation);
        //}

        // private ZoomData zoomData;

        public void RedrawZoomedImage(ZoomData zoomData)
        {
            // this.zoomData = zoomData;
            // pictureBoxToShowFullscreen.Refresh();
        }

        //private void imageBox_Paint(object sender, PaintEventArgs e)
        //{
        //    if (zoomData != null)
        //    {
        //        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        e.Graphics.ScaleTransform(zoomData.Zoom, zoomData.Zoom);
        //        e.Graphics.DrawImage(pictureBoxToShowFullscreen.Image, zoomData.GetImgX, zoomData.GetImgY);
        //    }
        //}               
        #endregion

        public event Action OpenPlayerPanelSettings;

        public event Action OpenChallengeSettings;

        public event Action OpenFtpSettings;

        public event Action OpenRewindSettings;

        public event Action OpenDevicesList;

        public event Action StartStream;

        public event Action StopStream;

        public event Action OpenGameFolder;

        public event Action MainFormClosing;

        public event Action CreateChallange;

        public event Action NewFrameCallback;

        public event Action<string> OpenChallengePlayer;

        public event Action OpenChallengePlayerForLastChallenge;

        public event Action<Point, int, Point> MakeZoom;

        public event Action<string, string> PassCamerasNamesToPresenterCallback;

        public event Action<string> OpenBroadcastForm;
        #endregion

        public void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = 1000;
            Thread.Sleep(1000);
            timer.Start();
        }

        #region Time axis
        public void ResetTimeAxis()
        {
            timer.Stop();
            timer.Dispose();
            // challangeTimeAxis.Reset();
            elapsedTimeFromStart.ResetText();
        }

        public void AddMarkerOnTimeAxis(string pathToChallenge)
        {
            Marker marker = new Marker(pathToChallenge);
            challengeTimeAxis.AddMarkerOnTimeAxis(marker);
            marker.Click += (sender, args) => Invoke(OpenChallengePlayer, marker.pathToChallenge);
        }
        #endregion

        public new void Show()
        {
            context.MainForm = this;
            Application.Run(context);
        }

        #region DrawPlayers
        public void DrawPlayers(PlayerPanelSettings settings,
                                int numberOfPlayers, List<string> camerasNames)
        {
            Dictionary<string, Bitmap> initialData = new Dictionary<string, Bitmap>();
            foreach (var cameraName in camerasNames)
            {
                initialData.Add(cameraName, null);
            }
            _playerPanel.DrawPlayers(numberOfPlayers, settings, initialData);
        }
        #endregion

        public void ToggleChallengeButton(bool enabled)
        {
            addChallange.Enabled = enabled;
        }

        public void MakeChallengeButtonInvisibleOn(int seconds)
        {
            ChangeVisibilityOn(addChallange, seconds, false);
        }

        public void MakeChallengeRecordingImageVisibleOn(int seconds)
        {
            ChangeVisibilityOn(challengeRecordingImage, seconds, true);
        }

        public void ToggleVisibilityOfViewLastChallengeButton()
        {
            viewLastChallengeButton.Visible = !viewLastChallengeButton.Visible;
        }

        private async void ChangeVisibilityOn(Control control, int seconds, bool isVisible)
        {
            control.Visible = isVisible;
            await Task.Delay(seconds * 1000);
            control.Visible = !isVisible;
        }

        public void ToggleStartButton(bool enabled)
        {
            startStreamButton.Enabled = enabled;
        }

        public void ToggleStopButton(bool enabled)
        {
            stopStreamButton.Enabled = enabled;
        }

        private bool IsStreaming()
        {
            return timer.Enabled;
        }

        public void DrawChallengeRecordingImage()
        {
            addChallange.Visible = false;
            PictureBox recordingChallenge = new PictureBox();
            recordingChallenge.Image = Image.FromFile(@"../../Images/challenge_recording.png");
            recordingChallenge.Location = addChallange.Location;
            recordingChallenge.Size = addChallange.Size;
            recordingChallenge.BringToFront();
            recordingChallenge.SizeMode = PictureBoxSizeMode.StretchImage;
            timeAxis.Controls.Add(recordingChallenge);
        }

        public void DrawNewFrame(Bitmap frame, string cameraName)
        {
            Bitmap frameClone = CloneFrame(frame);
            currentFrameCameraName = cameraName;
            currentFrame = frameClone;
            _playerPanel.UpdatePlayerImage(cameraName, frameClone);
            Invoke(NewFrameCallback);
        }

        private Bitmap CloneFrame(Bitmap frame)
        {
            return frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height), frame.PixelFormat);
        }

        private void drawTestPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int numberOfPlayers = 5;
            var randomData = new Dictionary<string, Bitmap>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                randomData.Add(i.ToString(), null);
            }
            _playerPanel.DrawPlayers(numberOfPlayers,
                new PlayerPanelSettings() { AutosizeMode = true }, randomData);
        }

        public void ShowMessage(ChallengeMessage message)
        {
            string caption = message.Caption;
            string text = message.Text;
            MessageBox.Show(text, caption,
                message.MessageButtons, message.MessageIcon);
        }
    }
}