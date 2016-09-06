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

        private int controlIndex;
        private PictureBox pictureBoxToShowFullscreen;
        private bool fullScreenMode = false;

        private void ShowFullScreen_Click(object sender, EventArgs e)
        {
            pictureBoxToShowFullscreen = (PictureBox)((Button)sender).Parent;
            controlIndex = playerPanel.Controls.GetChildIndex(pictureBoxToShowFullscreen);

            playerPanel.Controls.Remove(pictureBoxToShowFullscreen);
            this.Controls.Add(pictureBoxToShowFullscreen);

            pictureBoxToShowFullscreen.Dock = DockStyle.Fill;
            pictureBoxToShowFullscreen.BringToFront();
            pictureBoxToShowFullscreen.Select();
            this.WindowState = FormWindowState.Maximized;

            fullScreenMode = true;
            pictureBoxToShowFullscreen.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);

            // Disable click event if fullscreen mode is entered
            pictureBoxToShowFullscreen.Click -= new EventHandler(PlayerPanel_Click);

            // Hide the button
            foreach(Button btn in pictureBoxToShowFullscreen.Controls.OfType<Button>())
            {
                btn.Hide();
            }

            // When you change camera name in textbox, we should be able to press esc and exit fullscreen mode also
            foreach (TextBox textBox in pictureBoxToShowFullscreen.Controls.OfType<TextBox>())
            {
                textBox.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);
            }
        }

        public void FullScreenForm_KeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                playerPanel.Controls.Add(pictureBoxToShowFullscreen);
                playerPanel.Controls.SetChildIndex(pictureBoxToShowFullscreen, controlIndex);

                // Show the button we hide in fullscreen mode
                foreach (Button btn in pictureBoxToShowFullscreen.Controls.OfType<Button>())
                {
                    btn.Show();
                }

                // Now controls are not being removed
                pictureBoxToShowFullscreen.Dock = DockStyle.None;
            }
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
                Width = playerWidth,
                MaxLength = 30
            });

            Button showFullscreen = CreateFullScreenButton(playerWidth, playerHeight);

            showFullscreen.Click += new EventHandler(ShowFullScreen_Click);
            newPictureBox.Controls.Add(showFullscreen);
            newPictureBox.Click += new EventHandler(PlayerPanel_Click);
            return newPictureBox;
        }
        #endregion

        private Button CreateFullScreenButton(int playerWidth, int playerHeight)
        {
            int buttonWidth = 20;
            int buttonHeight = 20;
            // On autoscale works good, have to fix issue when using custom width and height
            var positionPoint = new Point(playerWidth - buttonWidth, playerHeight - buttonHeight);

            Button showFullscreen = new Button
            {
                Width = buttonWidth,
                Height = buttonHeight,
                Location = positionPoint,
                BackgroundImage = Image.FromFile("../../Images/fullscreen.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat
            };
            showFullscreen.FlatAppearance.BorderSize = 0;
            return showFullscreen;
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

        public void DrawNewFrame(Bitmap frame, string cameraName)
        {
            Bitmap frameClone =
                frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height),
                frame.PixelFormat);
            allPlayers.Where(player =>
            GetTextBoxOfPlayer(player).Text == cameraName).First().Image = frameClone;
            currentFrameInfo = Tuple.Create(cameraName, frameClone);
            Invoke(NewFrameCallback);
        }

        public void BindPlayersToCameras(Queue<string> camerasNames)
        {
            TextBox tmpCameraNameTextBox;
            foreach (var player in allPlayers)
            {
                if(camerasNames.Count > 0)
                {
                    tmpCameraNameTextBox = GetTextBoxOfPlayer(player);
                    tmpCameraNameTextBox.Text = camerasNames.Dequeue();
                }
            }
        }

        private TextBox GetTextBoxOfPlayer(PictureBox player)
        {
            return player.Controls.Cast<TextBox>().FirstOrDefault();
        }
    }
}
