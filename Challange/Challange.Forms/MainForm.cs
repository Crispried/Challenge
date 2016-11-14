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

namespace Challange.Forms
{
    public partial class MainForm : Form, IMainView, IMainFormLayout
    {
        private readonly ApplicationContext context;
        
        private System.Windows.Forms.Timer timer;
        private IMainFormLayout layout;

        private ComponentResourceManager resources =
                 new ComponentResourceManager(typeof(MainForm));

        private Tuple<string, Bitmap> currentFrameInfo;

        public MainForm(ApplicationContext context, IMainFormLayout layout)
        {
            this.context = context;
            this.layout = layout;
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
            layout.PlayerPanel = playerPanel;
            layout.Form = this;
        }

        // Unnecessary 
        public event Action<string, string> PassCamerasNamesToPresenterCallback;
        public event Action<string> OpenBroadcastForm;

        public FlowLayoutPanel PlayerPanel
        {
            get
            {
                return layout.PlayerPanel;
            }
            set
            {
                layout.PlayerPanel = value;
            }
        }

        public Form Form
        {
            get
            {
                return layout.Form;
            }
            set
            {
                layout.Form = value;
            }
        }

        public void UpdatePlayersImage(string cameraName, Bitmap frameClone)
        {
            layout.UpdatePlayersImage(cameraName, frameClone);
        }
        // End of unnecessary

        public Tuple<string, Bitmap> CurrentFrameInfo
        {
            get
            {
                return currentFrameInfo;
            }
        }

        public string GetElapsedTime
        {
            get
            {
                return elapsedTimeFromStart.Text;
            }
        }

        //public Dictionary<string, string> CamerasNames
        //{
        //    get
        //    {
        //        return camerasNames;
        //    }
        //}

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
        public void DrawPlayers(PlayerPanelSettings settings, int numberOfPlayers)
        {
            layout.DrawPlayers(settings, numberOfPlayers);
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
            layout.UpdatePlayersImage(cameraName, frameClone);
            currentFrameInfo = Tuple.Create(cameraName, frameClone);
            Invoke(NewFrameCallback);
        }

        private Bitmap CloneFrame(Bitmap frame)
        {
            return frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height), frame.PixelFormat);
        }

        public void BindPlayersToCameras(Queue<string> camerasNames)
        {
            layout.BindPlayersToCameras(camerasNames);
        }

        private void drawTestPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // layout.ClearControls(playerPanel);
            layout.DrawPlayers(new PlayerPanelSettings() { AutosizeMode = true }, 5);
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