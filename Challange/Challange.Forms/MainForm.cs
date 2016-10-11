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
using System.Reflection;
using System.ComponentModel;
using System.Windows;
using Challange.Domain.Services.Message;
using System.Drawing.Drawing2D;

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
        private Dictionary<string, string> camerasNames;

        #region Full screen button
        private string pathToFullScreenImage = "../../Images/fullscreen.png";
        private int fullScreenButtonWidth = 20;
        private int fullScreenButtonHeight = 20;
        #endregion

        private ComponentResourceManager resources =
                 new ComponentResourceManager(typeof(MainForm));

        private Tuple<string, Bitmap> currentFrameInfo;

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

        public Dictionary<string, string> CamerasNames
        {
            get
            {
                return camerasNames;
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

        #region fullScreen
        private int controlIndex;
        private PictureBox pictureBoxToShowFullscreen;

        private void ShowFullScreen_Click(object sender, EventArgs e)
        {
            pictureBoxToShowFullscreen = (PictureBox)((Button)sender).Parent;

            controlIndex = GetControlIndexOfClickedPictureBox();
            RemoveClickedPictureBoxFromPlayerPanel();
            AddFullScreenPictureBoxToGeneralControls();
            HideAllButtonsOnFullScreen();
            ShowFullScreenMode();
            ManageFullScreenEvents();
        }

        private int GetControlIndexOfClickedPictureBox()
        {
            return playerPanel.Controls.GetChildIndex(pictureBoxToShowFullscreen);
        }

        private void RemoveClickedPictureBoxFromPlayerPanel()
        {
            playerPanel.Controls.Remove(pictureBoxToShowFullscreen);
        }

        private void AddFullScreenPictureBoxToGeneralControls()
        {
            Controls.Add(pictureBoxToShowFullscreen);
        }

        private void AddFullScreenPictureBoxToPlayerPanelControls()
        {
            playerPanel.Controls.Add(pictureBoxToShowFullscreen);
        }

        private void ShowFullScreenMode()
        {
            pictureBoxToShowFullscreen.Dock = DockStyle.Fill;
            pictureBoxToShowFullscreen.BringToFront();
            pictureBoxToShowFullscreen.Select();
            MaximizeWindowState();
        }

        private void MaximizeWindowState()
        {
            WindowState = FormWindowState.Maximized;
        }

        private void HideAllButtonsOnFullScreen()
        {
            // Actually hides only "Show FullScreen" button
            foreach (Button btn in pictureBoxToShowFullscreen.Controls.OfType<Button>())
            {
                btn.Hide();
            }
        }

        private void ShowAllButtonsAfterFullScreenExit()
        {
            foreach (Button btn in pictureBoxToShowFullscreen.Controls.OfType<Button>())
            {
                btn.Show();
            }
        }

        private void ManageFullScreenEvents()
        {
            pictureBoxToShowFullscreen.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);

            // Zoom in/out event
            pictureBoxToShowFullscreen.MouseWheel += new MouseEventHandler(FullScreenPictureBox_MouseWheel);

            // Disable click event if fullscreen mode is entered
            pictureBoxToShowFullscreen.Click -= new EventHandler(PlayerPanel_Click);

            // When you change camera name in textbox, we should be able to press esc and exit fullscreen mode as well
            foreach (TextBox textBox in pictureBoxToShowFullscreen.Controls.OfType<TextBox>())
            {
                textBox.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);
            }
        }

        private void FullScreenForm_KeyPress(object sender, KeyEventArgs e)
        {
            if (EscapeKeyWasPressed(e))
            {
                AddFullScreenPictureBoxToPlayerPanelControls();
                PlaceFullScreenPictureBoxOnOldPosition();
                ShowAllButtonsAfterFullScreenExit();
                ManageExitFullScreenEvents();
            }
        }

        private double maxZoom = 1.5;
        private double minZoom = 0.5;
        // private double zoom = 1;
        private int realImageWidth = 0;
        private int realImageHeight = 0;
        private Image image;

        private double zoom = 1;


        private void FullScreenPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            PictureBox fullscreenPictureBox = (PictureBox)sender;
            image = fullscreenPictureBox.Image;

            if (realImageWidth == 0)
            {
                realImageWidth = fullscreenPictureBox.Image.Width;
            }

            if (realImageHeight == 0)
            {
                realImageHeight = fullscreenPictureBox.Image.Height;
            }

            if (e.Delta > 0)
            {
                zoom = zoom + 0.1;
                if (zoom < maxZoom)
                {
                    Bitmap bmp = new Bitmap(image, Convert.ToInt32(realImageWidth * zoom), Convert.ToInt32(realImageHeight * zoom));
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    fullscreenPictureBox.Image = bmp;
                    image = fullscreenPictureBox.Image;

                    // MessageBox.Show(e.X.ToString(), e.Y.ToString()); 
                }
                else
                {
                    zoom = maxZoom;
                    MessageBox.Show("No more scrolling up!");
                }
            }
            else
            {
                zoom = zoom - 0.1;
                if (zoom > minZoom)
                {
                    Bitmap bmp = new Bitmap(image, Convert.ToInt32(realImageWidth * zoom), Convert.ToInt32(realImageHeight * zoom));
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    fullscreenPictureBox.Image = bmp;
                    image = fullscreenPictureBox.Image;
                }
                else
                {
                    zoom = minZoom;
                    MessageBox.Show("No more scrolling down!");
                }
            }
        }

        private void ManageExitFullScreenEvents()
        {
            // Enable click event if user exits fullscreen mode
            pictureBoxToShowFullscreen.Click += new EventHandler(PlayerPanel_Click);

            pictureBoxToShowFullscreen.MouseWheel -= new MouseEventHandler(FullScreenPictureBox_MouseWheel);
        }

        private bool EscapeKeyWasPressed(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Escape;
        }

        private void PlaceFullScreenPictureBoxOnOldPosition()
        {
            playerPanel.Controls.SetChildIndex(pictureBoxToShowFullscreen, controlIndex);

            // Now controls are not being removed after fullscreen mode exit
            pictureBoxToShowFullscreen.Dock = DockStyle.None;
        }
        #endregion

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

        public event Action OpenPlayerPanelSettings;

        public event Action OpenChallengeSettings;

        public event Action OpenDevicesList;

        public event Action StartStream;

        public event Action StopStream;

        public event Action OpenGameFolder;

        public event Action MainFormClosing;

        public event Action CreateChallange;

        public event Action NewFrameCallback;

        public event Action<string, string> PassCamerasNamesToPresenterCallback;
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
            return replaceMode ? true : false;
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
            // challangeTimeAxis.Reset();
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
            ClearPlayerPanelControls();
            numberOfPlayers = GetNumberOfPlayers(settings);
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

        private void ClearPlayerPanelControls()
        {
            playerPanel.Controls.Clear();
        }

        private int GetNumberOfPlayers(PlayerPanelSettings settings)
        {
            return settings.NumberOfPlayers;
        }

        private Size GetPlayerSize(PlayerPanelSettings settings)
        {
            Size size = new Size();
            if (AutoSizeModeIsOn(settings))
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

        private bool AutoSizeModeIsOn(PlayerPanelSettings settings)
        {
            return settings.AutosizeMode;
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
            return CreatePictureBox(playerWidth, playerHeight);
        }
        #endregion

        private PictureBox CreatePictureBox(int playerWidth, int playerHeight)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Red;
            pictureBox.Height = playerHeight;
            pictureBox.Width = playerWidth;
            pictureBox.Image = Image.FromFile(@"C:\Images\default.jpg");
            pictureBox.Controls.Add(CreateTextBox(playerWidth));
            pictureBox.Controls.Add(CreateFullScreenButton(playerWidth, playerHeight));

            pictureBox.Click += new EventHandler(PlayerPanel_Click);

            return pictureBox;
        }

        private TextBox CreateTextBox(int playerWidth)
        {
            return new TextBox
            {
                Width = playerWidth,
                MaxLength = 30
            };
        }

        private Button CreateFullScreenButton(int playerWidth, int playerHeight)
        {
            Button showFullscreen = new Button
            {
                Width = fullScreenButtonWidth,
                Height = fullScreenButtonHeight,
                Location = CalculateFullScreenButtonPosition(playerWidth, playerHeight),
                BackgroundImage = Image.FromFile(pathToFullScreenImage),
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat
            };
            showFullscreen.FlatAppearance.BorderSize = 0;
            showFullscreen.Click += new EventHandler(ShowFullScreen_Click);
            return showFullscreen;
        }

        private Point CalculateFullScreenButtonPosition(int playerWidth, int playerHeight)
        {
            return new Point(playerWidth - fullScreenButtonWidth, playerHeight - fullScreenButtonHeight);
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

        public void DrawNewFrame(Bitmap frame, string cameraName)
        {
            Bitmap frameClone =
                frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height),
                frame.PixelFormat);
            allPlayers.Where(player =>
            GetTextBoxOfPlayer(player).Tag.ToString() == cameraName).
            First().Image = frameClone;
            currentFrameInfo = Tuple.Create(cameraName, frameClone);
            Invoke(NewFrameCallback);
        }

        public void BindPlayersToCameras(Queue<string> camerasNames)
        {
            this.camerasNames = new Dictionary<string, string>();
            TextBox tmpCameraNameTextBox;
            string currentCameraName;
            foreach (var player in allPlayers)
            {
                if(camerasNames.Count > 0)
                {
                    currentCameraName = camerasNames.Dequeue();
                    this.camerasNames.Add(currentCameraName, currentCameraName);
                    tmpCameraNameTextBox = GetTextBoxOfPlayer(player);
                    // value of our cameras names dictionary
                    tmpCameraNameTextBox.Text = currentCameraName;
                    // key of our cameras names dictionary
                    tmpCameraNameTextBox.Tag = currentCameraName; 
                    tmpCameraNameTextBox.TextChanged +=
                            TmpCameraNameTextBox_TextChanged;
                }
            }
        }

        private void TmpCameraNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;
            string key = currentTextBox.Tag.ToString();
            string cameraName = currentTextBox.Text;
            camerasNames[key] = cameraName;
            Invoke(PassCamerasNamesToPresenterCallback, key, cameraName);
        }

        private TextBox GetTextBoxOfPlayer(PictureBox player)
        {
            return player.Controls.Cast<TextBox>().FirstOrDefault();
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
