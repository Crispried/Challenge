using Challange.Domain.Services.Message;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Drawing;

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

        void DrawPlayers(int numberOfPlayers, List<string> videoNames);

        void DrawNewFrame(Bitmap frame, string videoName);

        void ShowMessage(ChallengeMessage message);
    }
}
