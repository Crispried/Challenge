using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Forms.Infrastructure;
using Cyotek.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Challange.Forms.Widgets
{
    public class PlayerPanel
    {
        private Form _parentForm;
        private bool _fullScreenMode = false;
        private int _autosizeWidthCoefficient = 3;
        private int _autosizeHeightCoefficient = 2;

        #region Buttons
        private const int _marginBetweenButtons = 3;
        private const int _playerButtonWidth = 20;
        private const int _playerButtonHeight = 20;
        private const string _pathToBroadcastImage = "../../Images/broadcast.png";
        private const string _pathToFullScreenImage = "../../Images/fullscreen.png";
        private const string _fullScreenButtonTag = "fullScreen";
        private const string _broadcastButtonTag = "broadcast";
        #endregion

        #region Player panel elements
        private List<PictureBox> _allPlayers;
        private FlowLayoutPanel _playerPanel;
        #endregion

        #region Player panel settings
        private bool _possibleToFullScreen;
        private bool _possibleToBroadcast;
        private bool _possibleToMovePlayers;
        private bool _possibleToRenamePlayers;
        #endregion
        private int _defaultZoom = 100;

        #region Constructors
        public PlayerPanel(Form parentForm, bool possibleToMovePlayers = false,
                           bool possibleToFullScreen = false, bool possibleToBroadcast = false,
                           bool possibleToRenamePlayers = false)
        {
            _parentForm = parentForm;
            _possibleToMovePlayers = possibleToMovePlayers;
            _possibleToFullScreen = possibleToFullScreen;
            _possibleToBroadcast = possibleToBroadcast;
            _possibleToRenamePlayers = possibleToRenamePlayers;
            CreatePlayerPanel();
        }
        #endregion

        private void CreatePlayerPanel()
        {
            _playerPanel = new FlowLayoutPanel();
            ControlsHelper.AddControl(_parentForm, _playerPanel);
            _playerPanel.BringToFront();
            ControlsHelper.SetDock(_playerPanel, DockStyle.Fill);
            _playerPanel.AutoScroll = true;
            _playerPanel.Padding = new Padding(10, 0, 10, 0);
        }
        
        public void DrawPlayers(int numberOfPlayers, PlayerPanelSettings playerPanelSettings,
                                Dictionary<string, Bitmap> initialData)
        {
            ControlsHelper.RemoveAllControls(_playerPanel);
            _allPlayers = new List<PictureBox>();
            var playerSize = GetPlayerSize(playerPanelSettings);
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var player = InitializePlayer(playerSize, initialData.ElementAt(i).Key,
                                              initialData.ElementAt(i).Value);
                _allPlayers.Add(player);
                ControlsHelper.AddControl(_playerPanel, player);
            }
        }

        public void UpdatePlayerImage(string cameraName, Bitmap frame)
        {
            if (!_fullScreenMode)
            {
                _allPlayers.Single(player => player.Tag.ToString() == cameraName).Image = frame;
            }
            else
            {
                if(_imageBoxToShowFullscreen.Tag.ToString() == cameraName)
                {
                    _imageBoxToShowFullscreen.Image = frame;
                }
            }
        }

        private void FullScreen_Paint(object sender, PaintEventArgs e)
        {
            if (_imageBoxToShowFullscreen.Zoom < _defaultZoom)
            {
                _imageBoxToShowFullscreen.Zoom = _defaultZoom;
            }
        }

        private Size GetPlayerSize(PlayerPanelSettings settings)
        {
            Size size = new Size();
            if (settings.AutosizeMode)
            {
                size.Width = _playerPanel.Width / _autosizeWidthCoefficient;
                size.Height = _playerPanel.Height / _autosizeHeightCoefficient;
            }
            else
            {
                size.Width = settings.PlayerWidth;
                size.Height = settings.PlayerHeight;
            }
            return size;
        }

        private Point _currentButtonPosition;

        private PictureBox InitializePlayer(Size playerSize, string name, Bitmap initialImage)
        {
            PictureBox player = CreatePictureBox(playerSize, name, initialImage);
            player.Controls.Add(CreateTextBox(playerSize.Width, name));
            CalculateCurrentButtonPosition(playerSize);
            if (_possibleToBroadcast)
            {
                player.Controls.Add(CreateBroadcastButton());
            }
            if (_possibleToFullScreen)
            {
                player.Controls.Add(CreateFullScreenButton());
            }
            return player;
        }

        private void CalculateCurrentButtonPosition(Size playerSize)
        {
            _currentButtonPosition = new Point(playerSize.Width,
                                   playerSize.Height - _playerButtonHeight - _marginBetweenButtons);
        }

        #region Picture box
        private PictureBox CreatePictureBox(Size playerSize, string name, Bitmap initialImage)
        {
            PictureBox pictureBox = new PictureBox()
            {
                Tag = name,
                BackColor = Color.FromArgb(56, 102, 200),
                Height = playerSize.Height,
                Width = playerSize.Width,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            // there aren't any default image, if its null user will see back color
            if (initialImage != null)
            {
                pictureBox.InitialImage = initialImage; 
            }
            if (_possibleToMovePlayers)
            {
                pictureBox.Click += new EventHandler(Player_Click);
                pictureBox.MouseHover += new EventHandler(Player_MouseHover);
                pictureBox.MouseLeave += new EventHandler(Player_MouseLeave);
            }
            return pictureBox;
        }

        private bool _hoverIsActive; // determines should be hover effect enabled or not
        private bool _replaceMode;
        private bool _areClickEventsDisabled; // determines state of click events for all butons on players
        private PictureBox _firstSelectedPlayer;
        private PictureBox _secondSelectedPlayer;
        private void Player_Click(object sender, EventArgs e)
        {
            if (!_fullScreenMode)
            {
                var clickedPlayer = sender as PictureBox;
                if (!_replaceMode)
                {
                    _firstSelectedPlayer = clickedPlayer;
                    ControlsHelper.SetCursor(_parentForm, Cursors.NoMove2D);
                    ControlsHelper.SetBorderStyle(clickedPlayer, BorderStyle.Fixed3D);
                }
                else
                {
                    _secondSelectedPlayer = clickedPlayer;
                    ReplacePlayers();
                    ControlsHelper.SetCursor(_parentForm, Cursors.Default);
                    ControlsHelper.SetBorderStyle(clickedPlayer, BorderStyle.None);
                }
                _hoverIsActive = !_hoverIsActive;
                _areClickEventsDisabled = !_areClickEventsDisabled;
                _replaceMode = !_replaceMode;
            }
        }

        private void ReplacePlayers()
        {
            var firstPlayerIndex = ControlsHelper.GetControlIndex(_playerPanel, _firstSelectedPlayer);
            var secondPlayerIndex = ControlsHelper.GetControlIndex(_playerPanel, _secondSelectedPlayer);
            ControlsHelper.SetChildIndex(_playerPanel, _firstSelectedPlayer, secondPlayerIndex);
            ControlsHelper.SetChildIndex(_playerPanel, _secondSelectedPlayer, firstPlayerIndex);
        }

        public void Player_MouseHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (ShouldBeHovered() && HoverNotOnFirstSelectedElement(pictureBox))
            {
                ControlsHelper.SetBorderStyle(pictureBox, BorderStyle.Fixed3D);
            }
        }

        public void Player_MouseLeave(object sender, EventArgs e)
        {
            var pictureBox = (PictureBox)sender;
            if (ShouldBeHovered())
            {
                if (HoverNotOnFirstSelectedElement(pictureBox))
                {
                    ControlsHelper.SetBorderStyle(pictureBox, BorderStyle.None);
                }
            }
            else
            {
                DisableBorderForAllPlayers();
            }
        }

        private void DisableBorderForAllPlayers()
        {
            foreach (PictureBox player in _allPlayers)
            {
                ControlsHelper.SetBorderStyle(player, BorderStyle.None);
            }
        }

        private bool ShouldBeHovered()
        {
            return _hoverIsActive == true && _fullScreenMode == false;
        }

        private bool HoverNotOnFirstSelectedElement(PictureBox pictureBox)
        {
            return ControlsHelper.GetControlIndex(_playerPanel, pictureBox) !=
                   ControlsHelper.GetControlIndex(_playerPanel, _firstSelectedPlayer);
        }
        #endregion

        #region Text box
        private TextBox CreateTextBox(int playerWidth, string text)
        {
            var textBox = new TextBox()
            {
                Text = text,
                BackColor = Color.FromArgb(56, 102, 200),
                Width = playerWidth,
                MaxLength = 30
            };
            textBox.ReadOnly = _possibleToRenamePlayers ? false : true;
            textBox.TextChanged += new EventHandler(_textBoxTextChangedEventHandler);
            return textBox;
        }

        private EventHandler _textBoxTextChangedEventHandler;
        public void SetTextChangedEventHandler(EventHandler handler)
        {
            _textBoxTextChangedEventHandler = handler;
        }
        #endregion

        #region Picture box buttons
        private ImageBox _imageBoxToShowFullscreen;
        private PictureBox _pictureBoxToShowFullscreen;

        private Button CreateFullScreenButton()
        {
            Button showFullscreen = new Button
            {
                Width = _playerButtonWidth,
                Height = _playerButtonHeight,
                Location = CalculatePlayerButtonPosition(ref _currentButtonPosition),
                BackgroundImage = Image.FromFile(_pathToFullScreenImage),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Name = _fullScreenButtonTag
            };
            showFullscreen.FlatAppearance.BorderSize = 0;
            showFullscreen.Click += new EventHandler(ShowFullScreen_Click);
            return showFullscreen;
        }

        private void ShowFullScreen_Click(object sender, EventArgs e)
        {
            if (!_areClickEventsDisabled)
            {
                _pictureBoxToShowFullscreen = (PictureBox)((Button)sender).Parent;
                _imageBoxToShowFullscreen = new ImageBox()
                {
                    Tag = _pictureBoxToShowFullscreen.Tag,
                    BackColor = Color.FromArgb(56, 102, 200),
                    SelectionMode = ImageBoxSelectionMode.Zoom,
                    GridDisplayMode = ImageBoxGridDisplayMode.Image,
                    Image = new Bitmap(_pictureBoxToShowFullscreen.Image)
                };
                //_imageBoxToShowFullscreen.ZoomToFit();
                ControlsHelper.AddControl(_parentForm, _imageBoxToShowFullscreen);
                ShowFullScreenMode();
                ManageFullScreenEvents();
            }
        }

        private void ShowFullScreenMode()
        {
            _fullScreenMode = true;
            ControlsHelper.SetDock(_imageBoxToShowFullscreen, DockStyle.Fill);
            _imageBoxToShowFullscreen.BringToFront();
            _imageBoxToShowFullscreen.Select();
            ControlsHelper.SetWindowState(_parentForm, FormWindowState.Maximized);
            CalculateCurrentButtonPosition(_imageBoxToShowFullscreen.Size);
            var broadcastButton = CreateBroadcastButton();
            _imageBoxToShowFullscreen.Controls.Add(broadcastButton);
        }

        private void FullScreenForm_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _fullScreenMode = false;
                _imageBoxToShowFullscreen.Image?.Dispose();
                _imageBoxToShowFullscreen.Hide();
                ManageFullScreenEvents();
            }
        }

        private void ManageFullScreenEvents()
        {
            if (_fullScreenMode)
            {
                _parentForm.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);
                _imageBoxToShowFullscreen.Paint += new PaintEventHandler(FullScreen_Paint);
            }
            else
            {
                _imageBoxToShowFullscreen.Paint -= new PaintEventHandler(FullScreen_Paint);
                _parentForm.KeyDown -= new KeyEventHandler(FullScreenForm_KeyPress);
            }
        }

        private Button CreateBroadcastButton()
        {
            Button broadcastButton = new Button
            {
                Width = _playerButtonWidth,
                Height = _playerButtonHeight,
                Location = CalculatePlayerButtonPosition(ref _currentButtonPosition), 
                BackgroundImage = Image.FromFile(_pathToBroadcastImage),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Name = _broadcastButtonTag
            };
            broadcastButton.FlatAppearance.BorderSize = 0;
            broadcastButton.Click += new EventHandler(_broadcastButtonClickEventHandler);
            return broadcastButton;
        }

        private EventHandler _broadcastButtonClickEventHandler;
        public void SetBroadcastButtonClickEventHandler(EventHandler handler)
        {
            _broadcastButtonClickEventHandler = handler;
        }

        private Point CalculatePlayerButtonPosition(ref Point position)
        {
            position.X = position.X - _playerButtonWidth - _marginBetweenButtons;
            return position;
        }
        #endregion
    }
}
