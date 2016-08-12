using System;
using Challange.Presenter.Base;
using Challange.Domain.Services.Settings.SettingTypes;
using AForge.Video.DirectShow;
using System.Drawing;

namespace Challange.Presenter.Views
{
    public interface IMainView : IView
    {
        event Action OpenPlayerPanelSettings;

        event Action StartStream;

        event Action StopStream;

        event Action MainFormClosing;

        event Action CreateChallange;

        event Action NewFrameCallback;

        Bitmap CurrentFrame { get; }

        void SubscribeNewFrameEvent(VideoCaptureDevice FinalVideo);

        void DrawPlayers(PlayerPanelSettings settings);

        void InitializeTimer();

        void ResetTimeAxis();

        void ClearPlayers();

        void AddMarkerOnTimeAxis();

        void MakeChallengeButtonInactiveOn(int seconds);

        void MakeChallengeButtonActive();

        void MakeChallengeButtonInactive();
    }
}
