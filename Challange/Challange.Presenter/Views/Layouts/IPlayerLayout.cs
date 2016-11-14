using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Presenter.Views.Layouts
{
    public interface IPlayerLayout : ILayout
    {
        void BindForm(Form form);

        void DrawChallengePlayerForm(int numberOfPlayers, Control challengePlayerPanel);

        void InitializePlayers(Dictionary<string, Bitmap> initialData);

        // Main Form
        void BindMainFormPlayerPanel(FlowLayoutPanel panel);
    }
}
