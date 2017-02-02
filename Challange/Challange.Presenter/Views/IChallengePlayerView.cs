using Challange.Domain.Services.Message;
using Challange.Domain.Services.Zoom.Concrete;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Views
{
    public interface IChallengePlayerView : IView
    {
        event Action<string> OpenBroadcastForm;

        event Action StartAllPlayers;

        event Action StopAllPlayers;

        event Action RewindBackward;

        event Action RewindForward;

        event Action OnFormClosing;

        event Action<int> OnPlaybackSpeedChanged;

        event Action<Point, int, Point> MakeZoom;

        void DrawPlayers(int numberOfPlayers, List<string> videoNames);

        void DrawNewFrame(Bitmap frame, string videoName);

        void ShowMessage(ChallengeMessage message);

        void SetZoomData(ZoomData zoomData);
    }
}
