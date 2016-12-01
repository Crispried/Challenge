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

        #region Player hover replacement
        private bool hoverIsActive = false;
        #endregion

        #region replace players fields
        bool replaceMode = false;
        PictureBox firstSelectedPlayer, secondSelecterPlayer;
        #endregion

        private List<PictureBox> allPlayers;
        private Dictionary<string, string> camerasNames;


        public MainFormLayout()
        {
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
                MainPanel = playerPanel;
            }
        }

        public void DrawPlayers(PlayerPanelSettings settings, int numberOfPlayers)
        {
            ClearControls(playerPanel);
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

        private void PlayerPanel_Click(object sender, EventArgs e)
        {
            if (FullScreenMode == false)
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
            foreach (Button fullScreenButton in FullScreenButtonsList)
            {
                fullScreenButton.Click -= new EventHandler(ShowFullScreen_Click);
            }
        }

        private void SetCursor(Cursor cursorType)
        {
            Form.Cursor = cursorType;
        }

        private void SetBorderStyle(BorderStyle borderStyle, PictureBox clickedPlayer)
        {
            clickedPlayer.BorderStyle = borderStyle;
        }

        private void EnableFullScreenButtonsClickEvent()
        {
            foreach (Button fullScreenButton in FullScreenButtonsList)
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
            return hoverIsActive == true && FullScreenMode == false;
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
            Form.Invoke(PassCamerasNamesToPresenterCallback, key, cameraName);
        }

        public event Action<string, string> PassCamerasNamesToPresenterCallback;

        public void UpdatePlayersImage(string cameraName, Bitmap frame)
        {
            allPlayers.Where(player => GetPlayersTextBox(player).Tag.ToString() == cameraName).First().Image = frame;
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
