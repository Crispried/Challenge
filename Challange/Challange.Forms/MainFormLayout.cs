using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Views.Layouts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    class MainFormLayout : FormLayout, IMainFormLayout
    {
        private FlowLayoutPanel playerPanel;

        private const int autosizeWidthCoefficient = 5;
        private const int autosizeHeightCoefficient = 3;

        #region Full screen button
        private string pathToFullScreenImage = "../../Images/fullscreen.png";
        private int fullScreenButtonWidth = 20;
        private int fullScreenButtonHeight = 20;
        private int controlIndex;
        private bool fullScreenMode = false;
        #endregion

        #region Broadcast button
        private string pathToBroadcastImage = "../../Images/broadcast.png";
        private int marginBetweenButtons = 3;
        #endregion

        #region Player hover replacement
        private bool hoverIsActive = false;
        #endregion

        #region replace players fields
        bool replaceMode = false;
        PictureBox firstSelectedPlayer, secondSelecterPlayer;
        #endregion

        private List<PictureBox> allPlayers;

        private Dictionary<string, string> camerasNames;

        // Not Sure What Is It And How To Initialize It?
        private PictureBox pictureBoxToShowFullscreen;

        private Form form;

        private List<Button> fullScreenButtonsList;

        public MainFormLayout()
        {
            fullScreenButtonsList = new List<Button>();
            allPlayers = new List<PictureBox>();
        }

        public FlowLayoutPanel PlayerPanel
        {
            get
            {
                return playerPanel;
            }
            set
            {
                playerPanel = value;
            }
        }

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

        public void DrawPlayers(PlayerPanelSettings settings, int numberOfPlayers)
        {
            ClearControls(playerPanel);
            var playerSize = GetPlayerSize(settings);
            var playerWidth = playerSize.Width;
            var playerHeight = playerSize.Height;
            for (int i = 0; i < 5; i++)
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

        private PictureBox InitializePlayer(int playerWidth, int playerHeight,
                               string playerName)
        {
            return CreatePlayer(playerWidth, playerHeight);
        }

        private PictureBox CreatePlayer(int playerWidth, int playerHeight)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Red;
            pictureBox.Height = playerHeight;
            pictureBox.Width = playerWidth;
            pictureBox.Controls.Add(CreateTextBox(playerWidth));
            pictureBox.Controls.Add(CreateFullScreenButton(playerWidth, playerHeight));
            pictureBox.Controls.Add(CreateBroadcastButton(playerWidth, playerHeight));

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
                Location = CalculatePlayerButtonPosition(playerWidth, playerHeight),
                BackgroundImage = Image.FromFile(pathToFullScreenImage),
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat
            };
            showFullscreen.FlatAppearance.BorderSize = 0;
            showFullscreen.Click += new EventHandler(ShowFullScreen_Click);

            fullScreenButtonsList.Add(showFullscreen);
            return showFullscreen;
        }

        private Button CreateBroadcastButton(int playerWidth, int playerHeight)
        {
            Button broadcastButton = new Button
            {
                Width = fullScreenButtonWidth,
                Height = fullScreenButtonHeight,
                Location = CalculatePlayerButtonPosition(playerWidth - (fullScreenButtonWidth + marginBetweenButtons), playerHeight),
                BackgroundImage = Image.FromFile(pathToBroadcastImage),
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat
            };
            broadcastButton.FlatAppearance.BorderSize = 0;
            broadcastButton.Click += (sender, args)
                            => form.Invoke(OpenBroadcastForm,
                                (sender as Button).
                                Parent.Controls.OfType<TextBox>().
                                FirstOrDefault().Tag);
            return broadcastButton;
        }

        public event Action<string> OpenBroadcastForm;

        private Point CalculatePlayerButtonPosition(int playerWidth, int playerHeight)
        {
            return new Point(playerWidth - fullScreenButtonWidth, playerHeight - fullScreenButtonHeight);
        }

        private void ShowFullScreen_Click(object sender, EventArgs e)
        {
            pictureBoxToShowFullscreen = (PictureBox)((Button)sender).Parent;
            // pictureBoxToShowFullscreen.Paint += new PaintEventHandler(imageBox_Paint);
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
            form.Controls.Add(pictureBoxToShowFullscreen);
        }

        private void HideAllButtonsOnFullScreen()
        {
            // Actually hides only "Show FullScreen" button
            foreach (Button btn in pictureBoxToShowFullscreen.Controls.OfType<Button>())
            {
                btn.Hide();
            }
        }

        private void ShowFullScreenMode()
        {
            fullScreenMode = true;
            pictureBoxToShowFullscreen.Dock = DockStyle.Fill;
            pictureBoxToShowFullscreen.BringToFront();
            pictureBoxToShowFullscreen.Select();
            MaximizeWindowState();
        }

        private void ManageFullScreenEvents()
        {
            pictureBoxToShowFullscreen.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);

            // When you change camera name in textbox, we should be able to press esc and exit fullscreen mode as well
            pictureBoxToShowFullscreen.Controls.OfType<TextBox>().First().KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);
        }

        private void MaximizeWindowState()
        {
            form.WindowState = FormWindowState.Maximized;
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

        private bool EscapeKeyWasPressed(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Escape;
        }

        private void AddFullScreenPictureBoxToPlayerPanelControls()
        {
            playerPanel.Controls.Add(pictureBoxToShowFullscreen);
        }

        private void PlaceFullScreenPictureBoxOnOldPosition()
        {
            SetChildIndex(pictureBoxToShowFullscreen, controlIndex);

            // Now controls are not being removed after fullscreen mode exit
            pictureBoxToShowFullscreen.Dock = DockStyle.None;
        }

        private void ShowAllButtonsAfterFullScreenExit()
        {
            foreach (Button btn in pictureBoxToShowFullscreen.Controls.OfType<Button>())
            {
                btn.Show();
            }
        }

        private void ManageExitFullScreenEvents()
        {
            pictureBoxToShowFullscreen.KeyDown -= new KeyEventHandler(FullScreenForm_KeyPress);

            // Disable the keydown event for picturebox when leaving the fullscreenmode
            pictureBoxToShowFullscreen.Controls.OfType<TextBox>().First().KeyDown -= new KeyEventHandler(FullScreenForm_KeyPress);
        }

        private void SetChildIndex(PictureBox player, int index)
        {
            playerPanel.Controls.SetChildIndex(player,
                                            index);
        }

        private void PlayerPanel_Click(object sender, EventArgs e)
        {
            if (fullScreenMode == false)
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

        private bool IsReplaceMode()
        {
            return replaceMode ? true : false;
        }

        private void DisableFullScreenButtonsClickEvent()
        {
            foreach (Button fullScreenButton in fullScreenButtonsList)
            {
                fullScreenButton.Click -= new EventHandler(ShowFullScreen_Click);
            }
        }

        private void SetCursor(Cursor cursorType)
        {
            form.Cursor = cursorType;
        }

        private void SetBorderStyle(BorderStyle borderStyle, PictureBox clickedPlayer)
        {
            clickedPlayer.BorderStyle = borderStyle;
        }

        private void EnableFullScreenButtonsClickEvent()
        {
            foreach (Button fullScreenButton in fullScreenButtonsList)
            {
                fullScreenButton.Click += new EventHandler(ShowFullScreen_Click);
            }
        }

        private void ReplacePlayers()
        {
            var firstPlayerIndex = GetPlayerIndex(firstSelectedPlayer);
            var secondPlayerIndex = GetPlayerIndex(secondSelecterPlayer);
            SetChildIndex(firstSelectedPlayer, secondPlayerIndex);
            SetChildIndex(secondSelecterPlayer, firstPlayerIndex);
        }

        private void ToggleReplaceMode()
        {
            replaceMode = !replaceMode;
        }

        private int GetPlayerIndex(PictureBox player)
        {
            return playerPanel.Controls.GetChildIndex(player);
        }

        public void Player_MouseHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (ShouldBeHovered() && HoverNotOnFirstSelectedElement(pictureBox))
            {
                SetBorderStyle(BorderStyle.Fixed3D, pictureBox);
            }
        }

        private bool ShouldBeHovered()
        {
            return hoverIsActive == true && fullScreenMode == false;
        }

        private bool HoverNotOnFirstSelectedElement(PictureBox pictureBox)
        {
            return playerPanel.Controls.GetChildIndex(pictureBox) != playerPanel.Controls.GetChildIndex(firstSelectedPlayer);
        }

        public void Player_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (ShouldBeHovered())
            {
                if (HoverNotOnFirstSelectedElement(pictureBox))
                {
                    SetBorderStyle(BorderStyle.None, pictureBox);
                }
            }
            else
            {
                DisableBorderForAllPlayers();
            }
        }

        private void DisableBorderForAllPlayers()
        {
            foreach (PictureBox player in allPlayers)
            {
                SetBorderStyle(BorderStyle.None, player);
            }
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

        private TextBox GetPlayersTextBox(PictureBox player)
        {
            return player.Controls.Cast<TextBox>().FirstOrDefault();
        }

        private void TmpCameraNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;
            string key = currentTextBox.Tag.ToString();
            string cameraName = currentTextBox.Text;
            camerasNames[key] = cameraName;
            form.Invoke(PassCamerasNamesToPresenterCallback, key, cameraName);
        }

        public event Action<string, string> PassCamerasNamesToPresenterCallback;

        public void UpdatePlayersImage(string cameraName, Bitmap frameClone)
        {
            allPlayers.Where(player => GetPlayersTextBox(player).Tag.ToString() == cameraName).First().Image = frameClone;
        }

        private void DrawPlayer(PictureBox player)
        {
            playerPanel.Controls.Add(player);
        }

        private void AddPlayerIntoPlayerList(PictureBox player)
        {
            allPlayers.Add(player);
        }
    }
}
