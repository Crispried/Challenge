using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Views.Layouts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    class ChallengePlayerFormLayout : FormLayout, IChallengePlayerFormLayout
    {
        private FlowLayoutPanel challengePlayerPanel;

        private List<Button> fullScreenButtonsList;
        private List<PictureBox> allPlayers;

        public ChallengePlayerFormLayout()
        {                        
            fullScreenButtonsList = new List<Button>();
            allPlayers = new List<PictureBox>();
        }

        public void DrawPlayers(int numberOfPlayers)
        {
            ClearControls(challengePlayerPanel);
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

        public FlowLayoutPanel ChallengePlayerPanel
        {
            get
            {
                return challengePlayerPanel;
            }
            set
            {
                challengePlayerPanel = value;
                MainPanel = challengePlayerPanel;
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

        public void InitializePlayers(Dictionary<string, Bitmap> initialData)
        {
            TextBox tmpTextBox;
            List<PictureBox> allPlayers = challengePlayerPanel.Controls.OfType<PictureBox>().ToList();
            for (int i = 0; i < allPlayers.Count; i++)
            {
                tmpTextBox = GetPlayersTextBox(allPlayers[i]);
                tmpTextBox.Text = initialData.ElementAt(i).Key;
                tmpTextBox.Tag = initialData.ElementAt(i).Key;
                allPlayers[i].Image = initialData.ElementAt(i).Value;
            }
        }
        private TextBox GetPlayersTextBox(PictureBox player)
        {
            return player.Controls.Cast<TextBox>().FirstOrDefault();
        }

        public void UpdatePlayersImage(string cameraName, Bitmap frame)
        {
            allPlayers.Where(player => GetPlayersTextBox(player).Tag.ToString() == cameraName).First().Image = frame;
        }
    }
}
