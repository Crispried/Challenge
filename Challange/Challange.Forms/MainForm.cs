using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Challange.Presenter.Views;
using Challange.Domain.SettingsService.SettingTypes;

namespace Challange.Forms
{
    public partial class MainForm : Form, IMainView
    {
        private readonly ApplicationContext context;
        private int numberOfPlayers;
        private const int autosizeWidthCoefficient = 5;
        private const int autosizeHeightCoefficient = 3;
        private Timer timer;

        public MainForm(ApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
            playerPanelSettings.Click += (sender, args) =>
                            Invoke(OpenPlayerPanelSettings);
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            challangeTimeAxis.UpdateTimeAxis();
            elapsedTimeFromStart.Text = challangeTimeAxis.ElapsedTimeFromStart;
        }

        private void addChallange_Click(object sender, EventArgs e)
        {
            challangeTimeAxis.CreateMarker();
        }

        public new void Show()
        {
            context.MainForm = this;
            Application.Run(context);
        }

        public void DrawPlayers(PlayerPanelSettings setting)
        {
            playerPanel.Controls.Clear();
            numberOfPlayers = setting.NumberOfPlayers;
            int playerWidth = setting.PlayerWidth;
            int playerHeight = setting.PlayerHeight;
            if (setting.AutosizeMode)
            {
                playerWidth = playerPanel.Width / autosizeWidthCoefficient;
                playerHeight = playerPanel.Height / autosizeHeightCoefficient;
            }
            Panel newPanel;
            MediaPlayer player = null;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                newPanel = new Panel();
                newPanel.BackColor = Color.LightPink;
                newPanel.Height = playerHeight;
                newPanel.Width = playerWidth;
                newPanel.Controls.Add(new Label() { Text = i.ToString() });
                newPanel.Click += new EventHandler(PlayerPanel_Click);
                newPanel.Tag = "playerPanel";
                if (i % 2 == 0)
                {
                    player = new MediaPlayer();
                    player.LoadFile(@"C:\Users\Crispried\Downloads\1.avi", newPanel, playerWidth, playerHeight);
                }
                else
                {
                    player = new MediaPlayer();
                    player.LoadFile(@"C:\Users\Crispried\Downloads\2.avi", newPanel, playerWidth, playerHeight);
                }
                playerPanel.Controls.Add(newPanel);
            }
            
        }

        private Panel draggedPanel;
        bool bordered = false;
        int firstPanelOnChangeIndex, secondPanelOnChangeIndex;
        Panel tmp;

        private void PlayerPanel_Click(object sender, EventArgs e)
        {
            var clickedPanel = sender as Panel;
            if (!bordered)
            {
                tmp = clickedPanel;
                firstPanelOnChangeIndex = playerPanel.Controls.GetChildIndex(clickedPanel);
                Cursor = Cursors.NoMove2D;
                draggedPanel = sender as Panel;
                var allPlayers = playerPanel.Controls.OfType<Panel>();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    allPlayers.ElementAt(i).BorderStyle = BorderStyle.Fixed3D;
                }
            }
            else
            {
                secondPanelOnChangeIndex = playerPanel.Controls.GetChildIndex(clickedPanel);
                playerPanel.Controls.SetChildIndex(tmp, secondPanelOnChangeIndex);
                playerPanel.Controls.SetChildIndex(clickedPanel, firstPanelOnChangeIndex);
                Cursor = Cursors.Default;
                draggedPanel = sender as Panel;
                var allPlayers = playerPanel.Controls.OfType<Panel>();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    allPlayers.ElementAt(i).BorderStyle = BorderStyle.None;
                }
            }
            bordered = !bordered;
        }

        public event Action OpenPlayerPanelSettings;
    }
}
