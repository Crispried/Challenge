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

        #region Zoom In
        private Image img;
        private int imgx = 0;
        private int imgy = 0;
        private float zoom = 1;
        private float minZoom = 1;
        private bool fullScreenMode = false;
        #endregion

        #region Player hover replacement
        private bool hoverIsActive = false;
        #endregion

        private ComponentResourceManager resources =
                 new ComponentResourceManager(typeof(MainForm));

        private Tuple<string, Bitmap> currentFrameInfo;

        private List<Button> fullScreenButtonsList;

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
            fullScreenButtonsList = new List<Button>();
        }

        public void Player_MouseHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            
            if(ShouldBeHovered() && HoverNotOnFirstSelectedElement(pictureBox))
            {
                SetBorderStyle(BorderStyle.Fixed3D, pictureBox);
            }
        }

        public void Player_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (ShouldBeHovered())
            {
                if(HoverNotOnFirstSelectedElement(pictureBox))
                {
                    SetBorderStyle(BorderStyle.None, pictureBox);
                }
            }
            else
            {
                DisableBorderForAllPlayers();
            }
        }
        
        private bool ShouldBeHovered()
        {
            return hoverIsActive == true;
        }

        private bool HoverNotOnFirstSelectedElement(PictureBox pictureBox)
        {
            return playerPanel.Controls.GetChildIndex(pictureBox) != playerPanel.Controls.GetChildIndex(firstSelectedPlayer);
        }

        private void DisableBorderForAllPlayers()
        {
            foreach (PictureBox player in allPlayers)
            {
                SetBorderStyle(BorderStyle.None, player);
            }
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
            img = pictureBoxToShowFullscreen.Image;
            pictureBoxToShowFullscreen.Paint += new PaintEventHandler(imageBox_Paint);
            controlIndex = GetControlIndexOfClickedPictureBox();
            RemoveClickedPictureBoxFromPlayerPanel();
            AddFullScreenPictureBoxToGeneralControls();
            HideAllButtonsOnFullScreen();
            ShowFullScreenMode();
            ManageFullScreenEvents();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Left:
                        imgx -= (int)(pictureBoxToShowFullscreen.Width * 0.1F / zoom);
                        pictureBoxToShowFullscreen.Refresh();
                        break;

                    case Keys.Right:
                        imgx += (int)(pictureBoxToShowFullscreen.Width * 0.1F / zoom);
                        pictureBoxToShowFullscreen.Refresh();
                        break;

                    case Keys.Up:
                        imgy -= (int)(pictureBoxToShowFullscreen.Height * 0.1F / zoom);
                        pictureBoxToShowFullscreen.Refresh();
                        break;

                    case Keys.Down:
                        imgy += (int)(pictureBoxToShowFullscreen.Height * 0.1F / zoom);
                        pictureBoxToShowFullscreen.Refresh();
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if(fullScreenMode == true)
            {
                float oldzoom = zoom;

                if (MouseIsScrollingUp(e.Delta))
                {
                    zoom += 0.1F;
                }
                else if (MouseIsScrollingDown(e.Delta))
                {
                    if (ZoomIsLargerThanMinimal())
                    {
                        zoom = Math.Max(zoom - 0.1F, 0.01F);
                    }
                    else
                    {
                        zoom = minZoom;
                    }
                }

                MouseEventArgs mouse = e as MouseEventArgs;
                Point mousePosNow = mouse.Location;

                int x = mousePosNow.X - pictureBoxToShowFullscreen.Location.X;
                int y = mousePosNow.Y - pictureBoxToShowFullscreen.Location.Y;

                int oldimagex = (int)(x / oldzoom);
                int oldimagey = (int)(y / oldzoom);

                int newimagex = (int)(x / zoom);
                int newimagey = (int)(y / zoom);

                imgx = newimagex - oldimagex + imgx;
                imgy = newimagey - oldimagey + imgy;

                pictureBoxToShowFullscreen.Refresh();
            }
        }

        private bool MouseIsScrollingUp(int delta)
        {
            return delta > 0;
        }

        private bool MouseIsScrollingDown(int delta)
        {
            return delta < 0;
        }

        private bool ZoomIsLargerThanMinimal()
        {
            return zoom > minZoom;
        }

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.DrawImage(img, imgx, imgy);
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
            fullScreenMode = true;
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

            // When you change camera name in textbox, we should be able to press esc and exit fullscreen mode as well
            pictureBoxToShowFullscreen.Controls.OfType<TextBox>().First().KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);
        }
        
        private void FullScreenForm_KeyPress(object sender, KeyEventArgs e)
        {
            if (EscapeKeyWasPressed(e))
            {
                fullScreenMode = false;
                AddFullScreenPictureBoxToPlayerPanelControls();
                PlaceFullScreenPictureBoxOnOldPosition();
                ShowAllButtonsAfterFullScreenExit();
                ManageExitFullScreenEvents();
            }
        }

        private void ManageExitFullScreenEvents()
        {
            pictureBoxToShowFullscreen.KeyDown -= new KeyEventHandler(FullScreenForm_KeyPress);

            // Disable the keydown event for picturebox when leaving the fullscreenmode
            pictureBoxToShowFullscreen.Controls.OfType<TextBox>().First().KeyDown -= new KeyEventHandler(FullScreenForm_KeyPress);
        }

        private bool EscapeKeyWasPressed(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Escape;
        }

        private void PlaceFullScreenPictureBoxOnOldPosition()
        {
            SetChildIndex(pictureBoxToShowFullscreen, controlIndex);

            // Now controls are not being removed after fullscreen mode exit
            pictureBoxToShowFullscreen.Dock = DockStyle.None;
        }
        #endregion

        private void PlayerPanel_Click(object sender, EventArgs e)
        {
            if(fullScreenMode == false)
            {
                hoverIsActive = !hoverIsActive;
                var clickedPlayer = sender as PictureBox;
                if (!IsReplaceMode())
                {
                    // Works a little bit slower
                    DisableFullScreenButtonsClickEvent();
                    firstSelectedPlayer = clickedPlayer;
                    SetCursor(Cursors.NoMove2D);
                    SetBorderStyle(BorderStyle.Fixed3D, clickedPlayer);
                }
                else
                {
                    // Works a little bit slower
                    EnableFullScreenButtonsClickEvent();
                    secondSelecterPlayer = clickedPlayer;
                    ReplacePlayers();
                    SetCursor(Cursors.Default);
                    SetBorderStyle(BorderStyle.None, clickedPlayer);
                }
                ToggleReplaceMode();
            }
        }

        private void DisableFullScreenButtonsClickEvent()
        {
            foreach (Button fullScreenButton in fullScreenButtonsList)
            {
                fullScreenButton.Click -= new EventHandler(ShowFullScreen_Click);
            }
        }

        private void EnableFullScreenButtonsClickEvent()
        {
            foreach (Button fullScreenButton in fullScreenButtonsList)
            {
                fullScreenButton.Click += new EventHandler(ShowFullScreen_Click);
            }
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
            SetChildIndex(firstSelectedPlayer, secondPlayerIndex);
            SetChildIndex(secondSelecterPlayer, firstPlayerIndex);
        }

        private int GetPlayerIndex(PictureBox player)
        {
            return playerPanel.Controls.GetChildIndex(player);
        }

        private void SetCursor(Cursor cursorType)
        {
            Cursor = cursorType;
        }

        private void SetBorderStyle(BorderStyle borderStyle, PictureBox clickedPlayer)
        {
            clickedPlayer.BorderStyle = borderStyle;
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

        private void SetChildIndex(PictureBox player, int index)
        {
            playerPanel.Controls.SetChildIndex(player,
                                            index);
        }

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
                player.Click += new EventHandler(PlayerPanel_Click);
                player.MouseHover += new EventHandler(Player_MouseHover);
                player.MouseLeave += new EventHandler(Player_MouseLeave);
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
            return CreatePlayer(playerWidth, playerHeight);
        }
        #endregion

        private PictureBox CreatePlayer(int playerWidth, int playerHeight)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Red;
            pictureBox.Height = playerHeight;
            pictureBox.Width = playerWidth;
            pictureBox.Image = Image.FromFile(@"C:\Images\default.jpg");
            pictureBox.Controls.Add(CreateTextBox(playerWidth));
            pictureBox.Controls.Add(CreateFullScreenButton(playerWidth, playerHeight));

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

            fullScreenButtonsList.Add(showFullscreen);
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
            Bitmap frameClone = CloneFrame(frame);
            UpdatePlayersImage(cameraName, frameClone);
            currentFrameInfo = Tuple.Create(cameraName, frameClone);
            Invoke(NewFrameCallback);
        }

        private Bitmap CloneFrame(Bitmap frame)
        {
            return frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height), frame.PixelFormat);
        }

        private void UpdatePlayersImage(string cameraName, Bitmap frameClone)
        {
            allPlayers.Where(player => GetPlayersTextBox(player).Tag.ToString() == cameraName).First().Image = frameClone;
        }

        public void BindPlayersToCameras(Queue<string> camerasNames)
        {
            this.camerasNames = new Dictionary<string, string>();
            TextBox tmpCameraNameTextBox;
            string currentCameraName;
            foreach (var player in allPlayers)
            {
                if (camerasNames.Count > 0)
                {
                    currentCameraName = camerasNames.Dequeue();
                    this.camerasNames.Add(currentCameraName, currentCameraName);
                    tmpCameraNameTextBox = GetPlayersTextBox(player);
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

        private TextBox GetPlayersTextBox(PictureBox player)
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
