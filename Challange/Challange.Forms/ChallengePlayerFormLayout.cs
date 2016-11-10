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
        // Why do we need to make it static?
        private static Control challengePlayerPanel;

        private List<PictureBox> allPlayers;

        private const int autosizeWidthCoefficient = 5;
        private const int autosizeHeightCoefficient = 3;

        public ChallengePlayerFormLayout() : base(challengePlayerPanel)
        {
            allPlayers = new List<PictureBox>();
        }

        public void DrawChallengePlayerForm(int numberOfPlayers, Control playerPanel)
        {
            // Setting the static player panel
            challengePlayerPanel = playerPanel;

            ClearPlayerPanelControls(challengePlayerPanel);
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

        // FROM IChallengePlayerView
        public void InitializePlayers(Dictionary<string, Bitmap> initialData)
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

        private void DrawPlayer(PictureBox player)
        {
            challengePlayerPanel.Controls.Add(player);
        }

        private void AddPlayerIntoPlayerList(PictureBox player)
        {
            allPlayers.Add(player);
        }

        private TextBox GetPlayersTextBox(PictureBox player)
        {
            return player.Controls.Cast<TextBox>().FirstOrDefault();
        }

        
    }
}
