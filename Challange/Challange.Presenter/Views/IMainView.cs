using System;
using Challange.Presenter.Base;
using Challange.Domain.Services.Settings.SettingTypes;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Collections.Generic;

namespace Challange.Presenter.Views
{
    public interface IMainView : IView
    {
        event Action OpenPlayerPanelSettings;

        event Action OpenChallengeSettings;

        event Action OpenDevicesList;

        event Action StartStream;

        event Action StopStream;

        event Action OpenGameFolder;

        event Action MainFormClosing;

        event Action CreateChallange;

        event Action NewFrameCallback;

        Bitmap CurrentFrame { get; }

        string GetElapsedTime { get; }

        void SubscribeNewFrameEvent(VideoCaptureDevice FinalVideo);

        void DrawPlayers(PlayerPanelSettings settings);

        void InitializeTimer();

        void ResetTimeAxis();

        void ClearPlayers();

        void AddMarkerOnTimeAxis();

        void ToggleChallengeButton(bool state);

        void ToggleChallengeButtonIn(bool state, int seconds);

        void DrawNewFrame(Bitmap frame);
    }
}
