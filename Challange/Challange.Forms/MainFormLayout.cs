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
        private FlowLayoutPanel mainFormPlayerPanel;
        private Form form;

        public void BindMainFormPlayerPanel(FlowLayoutPanel mainFormPlayerPanel)
        {
            this.mainFormPlayerPanel = mainFormPlayerPanel;
        }

        public void BindForm(Form form)
        {
            this.form = form;
        }

        public void DrawPlayers(PlayerPanelSettings settings, int numberOfPlayers)
        {
            ClearPlayerPanelControls(mainFormPlayerPanel);
            var playerSize = GetPlayerSize(settings);
            var playerWidth = playerSize.Width;
            var playerHeight = playerSize.Height;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var player = InitializePlayer(playerWidth,
                                    playerHeight, i.ToString());
                // player.Click += new EventHandler(PlayerPanel_Click);
                // player.MouseHover += new EventHandler(Player_MouseHover);
                // player.MouseLeave += new EventHandler(Player_MouseLeave);
                DrawPlayer(player);
                AddPlayerIntoPlayerList(player);
            }
        }

        private Size GetPlayerSize(PlayerPanelSettings settings)
        {
            Size size = new Size();
            if (AutoSizeModeIsOn(settings))
            {
                size.Width = mainFormPlayerPanel.Width / autosizeWidthCoefficient;
                size.Height = mainFormPlayerPanel.Height / autosizeHeightCoefficient;
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
    }
}
