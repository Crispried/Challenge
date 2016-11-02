using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class ChallengePlayerForm : Form, IChallengePlayerView
    {
        private List<PictureBox> allPlayers;

        private List<Button> fullScreenButtonsList;

        private bool fullScreenMode = false;

        #region Full screen button
        private string pathToFullScreenImage = "../../Images/fullscreen.png";
        private int fullScreenButtonWidth = 20;
        private int fullScreenButtonHeight = 20;
        #endregion

        #region Broadcast button
        private string pathToBroadcastImage = "../../Images/broadcast.png";
        private int marginBetweenButtons = 3;
        #endregion

        public ChallengePlayerForm()
        {
            InitializeComponent();
            rewindOnSetting.Click += (sender, args)
                            => Invoke(OpenRewindSettings);
            allPlayers = new List<PictureBox>();
            fullScreenButtonsList = new List<Button>();
        }

        public event Action OpenRewindSettings;

        public event Action<string> OpenBroadcastForm;

        public void DrawPlayers(int numberOfPlayers)
        {
            ClearPlayerPanelControls();
            var playerWidth = 640;
            var playerHeight = 480;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var player = InitializePlayer(playerWidth,
                                    playerHeight, i.ToString());
                DrawPlayer(player);
                AddPlayerIntoPlayerList(player);
            }
        }

        private void DrawPlayer(PictureBox player)
        {
            challengePlayerPanel.Controls.Add(player);
        }

        private void AddPlayerIntoPlayerList(PictureBox player)
        {
            allPlayers.Add(player);
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

        private int controlIndex;
        private PictureBox pictureBoxToShowFullscreen;
        private void ShowFullScreen_Click(object sender, EventArgs e)
        {
            pictureBoxToShowFullscreen = (PictureBox)((Button)sender).Parent;
            pictureBoxToShowFullscreen.Paint += new PaintEventHandler(imageBox_Paint);
            controlIndex = GetControlIndexOfClickedPictureBox();
            RemoveClickedPictureBoxFromPlayerPanel();
            AddFullScreenPictureBoxToGeneralControls();
            HideAllButtonsOnFullScreen();
            ShowFullScreenMode();
            ManageFullScreenEvents();
        }

        private void AddFullScreenPictureBoxToGeneralControls()
        {
            Controls.Add(pictureBoxToShowFullscreen);
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

        private void PlaceFullScreenPictureBoxOnOldPosition()
        {
            SetChildIndex(pictureBoxToShowFullscreen, controlIndex);

            // Now controls are not being removed after fullscreen mode exit
            pictureBoxToShowFullscreen.Dock = DockStyle.None;
        }

        private void SetChildIndex(PictureBox player, int index)
        {
            challengePlayerPanel.Controls.SetChildIndex(player,
                                            index);
        }

        private void AddFullScreenPictureBoxToPlayerPanelControls()
        {
            challengePlayerPanel.Controls.Add(pictureBoxToShowFullscreen);
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

        private bool EscapeKeyWasPressed(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Escape;
        }

        private int GetControlIndexOfClickedPictureBox()
        {
            return challengePlayerPanel.Controls.GetChildIndex(pictureBoxToShowFullscreen);
        }

        private ZoomData zoomData;

        public void RedrawZoomedImage(ZoomData zoomData)
        {
            this.zoomData = zoomData;
            pictureBoxToShowFullscreen.Refresh();
        }
        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            if (zoomData != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.ScaleTransform(zoomData.Zoom, zoomData.Zoom);
                e.Graphics.DrawImage(pictureBoxToShowFullscreen.Image, zoomData.GetImgX, zoomData.GetImgY);
            }
        }

        private void RemoveClickedPictureBoxFromPlayerPanel()
        {
            challengePlayerPanel.Controls.Remove(pictureBoxToShowFullscreen);
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

        private void MaximizeWindowState()
        {
            WindowState = FormWindowState.Maximized;
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
                            => Invoke(OpenBroadcastForm,
                                (sender as Button).
                                Parent.Controls.OfType<TextBox>().
                                FirstOrDefault().Tag);
            return broadcastButton;
        }

        private Point CalculatePlayerButtonPosition(int playerWidth, int playerHeight)
        {
            return new Point(playerWidth - fullScreenButtonWidth, playerHeight - fullScreenButtonHeight);
        }

        private void ClearPlayerPanelControls()
        {
            challengePlayerPanel.Controls.Clear();
        }

        private TextBox GetPlayersTextBox(PictureBox player)
        {
            return player.Controls.Cast<TextBox>().FirstOrDefault();
        }

        void IChallengePlayerView.InitializePlayers(Dictionary<string, Bitmap> initialData)
        {
            TextBox tmpTextBox;
            List<PictureBox> allPlayers = challengePlayerPanel.Controls.OfType<PictureBox>().ToList();
            for (int i = 0; i < allPlayers.Count; i++)
            {
                tmpTextBox = GetPlayersTextBox(allPlayers[i]);
                tmpTextBox.Text = initialData.ElementAt(i).Key;
                allPlayers[i].Image = initialData.ElementAt(i).Value;
            }
        }

        public void ShowMessage(ChallengeMessage message)
        {
            MessageBox.Show(message.Text, message.Caption,
                message.MessageButtons, message.MessageIcon);
        }
    }
}
