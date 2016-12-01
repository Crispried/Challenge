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
        Form Form { get; set; }
        FlowLayoutPanel ChallengePlayerPanel { get; set; }
        void DrawPlayers(int numberOfPlayers);

        void UpdatePlayersImage(string cameraName, Bitmap frame);

        void InitializePlayers(Dictionary<string, Bitmap> initialData);
    }
}
