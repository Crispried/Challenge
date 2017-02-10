using System;
using Challange.Presenter.Base;
using Challange.Domain.Services.Settings.SettingTypes;
using System.Drawing;
using System.Collections.Generic;
using Challange.Domain.Services.Message;

namespace Challange.Presenter.Views
{
    public interface IMainView : IView
    {
        event Action OpenPlayerPanelSettings;

        event Action OpenChallengeSettings;

        event Action OpenFtpSettings;

        event Action OpenRewindSettings;

        event Action OpenDevicesList;

        event Action StartStream;

        event Action StopStream;

        event Action OpenGameFolder;

        event Action MainFormClosing;

        event Action CreateChallange;

        event Action NewFrameCallback;

        event Action<string> OpenChallengePlayer; // where string is path to neccessary challenge

        event Action OpenChallengePlayerForLastChallenge; // where string is path to last challenge

        event Action<string> OpenBroadcastForm; // where string should be cameraFullName

        event Action<string, string> CameraNameChanged; // where first string is camera key, second is camera name

        string CurrentFrameCameraName { get; }

        Bitmap CurrentFrame { get; }

        string GetElapsedTime { get; }

        void DrawPlayers(PlayerPanelSettings settings, int numberOfPlayers, List<string> camerasNames);

        void InitializeTimer();

        void ResetTimeAxis();

        void AddMarkerOnTimeAxis(string pathToChallenge);

        void ToggleChallengeButton(bool state);

        void MakeChallengeButtonInvisibleOn(int seconds);

        void MakeChallengeRecordingImageVisibleOn(int seconds);

        void ToggleStartButton(bool state);

        void ToggleStopButton(bool state);

        void DrawNewFrame(Bitmap frame, string cameraName);

        void DrawChallengeRecordingImage();

        void ShowMessage(ChallengeMessage message);

        void ToggleVisibilityOfViewLastChallengeButton();
    }
}
