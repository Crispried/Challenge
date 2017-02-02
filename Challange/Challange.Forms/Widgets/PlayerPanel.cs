using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Zoom.Concrete;
using Challange.Forms.Infrastructure;
using Cyotek.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

        private Bitmap _tempImage;
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
                    _tempImage = frame;
                    if (_zoom != 0)
                    {
                        //var bitmap = new Bitmap(frame.Width, frame.Height, PixelFormat.Format24bppRgb);
                        //var graph = Graphics.FromImage(bitmap);
                        //graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        //graph.ScaleTransform(_zoom, _zoom);
                        //graph.DrawImage(frame, _imgX, _imgY);
                        //graph.Dispose();

                        //_pictureBoxToShowFullscreen.Image = bitmap;
                        //_tempImage = frame;
                        //_imageBoxToShowFullscreen.Refresh();
             
                    }
                    else
                    {
                       // _pictureBoxToShowFullscreen.Image = frame;
                    }
                }
            }
        }

        private void FullScreen_Paint(object sender, PaintEventArgs e)
        {
            //if(_tempImage != null)
            //{
            //    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    e.Graphics.ScaleTransform(_zoom, _zoom);
            //    _tempImage.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
            //    e.Graphics.DrawImage(_tempImage, _imgX, _imgY);
            //}
            if (_imageBoxToShowFullscreen.Zoom < 18)
            {
                _imageBoxToShowFullscreen.Zoom = 18;
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
                SizeMode = PictureBoxSizeMode.Zoom
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
            return textBox;
        }

        private EventHandler _textBoxTextChangedEventHandler;
        public void SubscribeTextBoxesToTextChangedEvent(EventHandler handler)
        {
            if (_possibleToRenamePlayers)
            {
                _textBoxTextChangedEventHandler = handler;
                TextBox tmpTextBox;
                foreach (var player in _allPlayers)
                {
                    tmpTextBox = ControlsHelper.GetFirstControl<TextBox>(player);
                    tmpTextBox.TextChanged += handler;
                }
            }
        }

        public void UnsubscribeTextBoxesFromTextChangedEvent()
        {
            if (_possibleToRenamePlayers)
            {
                TextBox tmpTextBox;
                foreach (var player in _allPlayers)
                {
                    tmpTextBox = ControlsHelper.GetFirstControl<TextBox>(player);
                    tmpTextBox.TextChanged -= _textBoxTextChangedEventHandler;
                }
            }
        }
        #endregion

        #region Picture box buttons
        private ImageBox _imageBoxToShowFullscreen;
        private PictureBox _pictureBoxToShowFullscreen;
        private int _fullScreenPlayerIndex;
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
                    SelectionMode = ImageBoxSelectionMode.Zoom
                };
                _imageBoxToShowFullscreen.ZoomToFit();
                _fullScreenPlayerIndex = ControlsHelper.GetControlIndex(_playerPanel, _pictureBoxToShowFullscreen);
           //     ControlsHelper.RemoveControl(_playerPanel, _imageBoxToShowFullscreen);
                ControlsHelper.AddControl(_parentForm, _imageBoxToShowFullscreen);
           //     ChangeFullScreenButtonVisibility(false);
                ShowFullScreenMode();
                ManageFullScreenEvents(true);
            }
        }

        public event Action<Point, int, Point> OnMouseWheelCallback; // arguments are neccessary for zooming

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            Point mouseLocation = new Point();
            Point pictureBoxLocation = new Point();
            mouseLocation.X = mouseEventArgs.Location.X;
            mouseLocation.Y = mouseEventArgs.Location.Y;
            pictureBoxLocation.X = _imageBoxToShowFullscreen.Location.X;
            pictureBoxLocation.Y = _imageBoxToShowFullscreen.Location.Y;
            _parentForm.Invoke(OnMouseWheelCallback, pictureBoxLocation, e.Delta, mouseLocation);
        }

        private void ShowFullScreenMode()
        {
            _fullScreenMode = true;
            ControlsHelper.SetDock(_imageBoxToShowFullscreen, DockStyle.Fill);
            _imageBoxToShowFullscreen.BringToFront();
            _imageBoxToShowFullscreen.Select();
            ControlsHelper.SetWindowState(_parentForm, FormWindowState.Maximized);
           // _pictureBoxToShowFullscreen.ImageLocation = new Point((_parentForm.Width / 2 - _pictureBoxToShowFullscreen.Width / 2), (_parentForm.Height / 2 - _pictureBoxToShowFullscreen.Height / 2));
            var broadcastButton = ControlsHelper.GetFirstControlWithName<Button>(
                                    _pictureBoxToShowFullscreen, "broadcast");
            CalculateCurrentButtonPosition(_imageBoxToShowFullscreen.Size);
            broadcastButton.Location = CalculatePlayerButtonPosition(ref _currentButtonPosition);
        }

        private void FullScreenForm_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _fullScreenMode = false;
                // ControlsHelper.AddControl(_playerPanel, _imageBoxToShowFullscreen);
                //   ControlsHelper.SetChildIndex(_playerPanel, _imageBoxToShowFullscreen,
                //                             _fullScreenPlayerIndex);
                //  ControlsHelper.SetDock(_imageBoxToShowFullscreen, DockStyle.None);
                _imageBoxToShowFullscreen.Dispose();
               // ChangeFullScreenButtonVisibility(true);
               // var broadcastButton = ControlsHelper.GetFirstControlWithName<Button>(
                          //           _imageBoxToShowFullscreen, "broadcast");
              //  CalculateCurrentButtonPosition(_imageBoxToShowFullscreen.Size);
               // broadcastButton.Location = CalculatePlayerButtonPosition(ref _currentButtonPosition);
                ManageFullScreenEvents(false);
            }
        }

        private void ManageFullScreenEvents(bool enabled)
        {
            if (enabled)
            {
                _parentForm.KeyDown += new KeyEventHandler(FullScreenForm_KeyPress);
                _imageBoxToShowFullscreen.Paint += new PaintEventHandler(FullScreen_Paint);
               // _imageBoxToShowFullscreen.MouseWheel += new MouseEventHandler(OnMouseWheel);
            }
            else
            {
                _imageBoxToShowFullscreen.Paint -= new PaintEventHandler(FullScreen_Paint);
                _parentForm.KeyDown -= new KeyEventHandler(FullScreenForm_KeyPress);
              //  _imageBoxToShowFullscreen.MouseWheel -= new MouseEventHandler(OnMouseWheel);
            }
        }

        private void ChangeFullScreenButtonVisibility(bool visible)
        {
            Button fullScreenButton = ControlsHelper.GetFirstControlWithName<Button>(
                                        _imageBoxToShowFullscreen, _fullScreenButtonTag);
            if (visible)
            {
                ControlsHelper.ShowControl(fullScreenButton);
            }
            else
            {
                ControlsHelper.HideControl(fullScreenButton);
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
            return broadcastButton;
        }

        private EventHandler _broadcastButtonClickEventHandler;
        public void SubscribeBroadcastButtonToClickEvent(EventHandler handler)
        {
            _broadcastButtonClickEventHandler = handler;
            Button tmpButton;
            foreach (var player in _allPlayers)
            {
                tmpButton = ControlsHelper.GetFirstControlWithName<Button>(player, _broadcastButtonTag);
                tmpButton.Click += handler;
            }
        }

        public void UnsubscribeBroadcastButtonFromClickEvent()
        {
            Button tmpButton;
            foreach (var player in _allPlayers)
            {
                tmpButton = ControlsHelper.GetFirstControlWithName<Button>(player, _broadcastButtonTag);
                tmpButton.Click -= _broadcastButtonClickEventHandler;
            }
        }

        private Point CalculatePlayerButtonPosition(ref Point position)
        {
            position.X = position.X - _playerButtonWidth - _marginBetweenButtons;
            return position;
        }
        #endregion

        #region zoom
        private float _zoom = 1;
        private int _imgX;
        private int _imgY;

        public void SetZoomData(float zoom, int imgX, int imgY)
        {
            _zoom = zoom;
            _imgX = imgX;
            _imgY = imgY;
        }
        #endregion
    }
}
