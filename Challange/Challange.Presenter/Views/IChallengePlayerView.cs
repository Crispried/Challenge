using Challange.Domain.Services.Message;
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

        void DrawPlayers(int numberOfPlayers);

        void InitializePlayers(Dictionary<string, Bitmap> initialData);

        void DrawNewFrame(Bitmap frame, string videoName);

        void ShowMessage(ChallengeMessage message);
    }
}
