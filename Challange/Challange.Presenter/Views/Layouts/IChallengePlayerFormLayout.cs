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
    public interface IChallengePlayerFormLayout : ILayout
    {
        void DrawPlayers(int numberOfPlayers);

        Form Form { get; set; }

        FlowLayoutPanel ChallengePlayerPanel { get; set; }

        void InitializePlayers(Dictionary<string, Bitmap> initialData);
    }
}
