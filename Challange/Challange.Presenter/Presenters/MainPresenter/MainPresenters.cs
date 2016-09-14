using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter
    {
        public override void Run()
        {
            playerPanelSettings = GetPlayerPanelSettings();
            challengeSettings = GetChallengeSettings();
            DrawPlayers();
            // we need to keep game information 
            InitializeGameInformation();
            Controller.Run<GameInformationPresenter.GameInformationPresenter,
                           GameInformation>(gameInformation);
            pylonCameraProvider = new PylonCameraProvider();
            View.Show();
        }

        /// <summary>
        /// Shows form which contains list of all connected devices
        /// </summary>
        public void ShowDevicesList()
        {
            InitializeDevicesList();
            Controller.Run<CamerasPresenter.CamerasPresenter,
                List<Device>>(camerasInfo);
        }

        /// <summary>
        /// Adds new frame into tempFpses collection
        /// which in fact contains past and future frames
        /// </summary>
        public void AddNewFrame()
        {
            string cameraName = View.CurrentFrameInfo.Item1;
            Bitmap currentFrame = View.CurrentFrameInfo.Item2;
            FPS tempFPS;
            tempFpses.TryGetValue(cameraName, out tempFPS);
            tempFPS.AddFrame(currentFrame);
        }

        /// <summary>
        /// Shows the form which allows to change player panel settings
        /// after that it redraws player panel with new settings
        /// </summary>
        public void ChangePlayerPanelSettings()
        {
            Controller.Run<PlayerPanelSettingsPresenter.PlayerPanelSettingsPresenter,
                           PlayerPanelSettings>(playerPanelSettings);
            DrawPlayers();
        }

        /// <summary>
        /// Shows the form which allows to change challenge settings
        /// </summary>
        public void ChangeChallengeSettings()
        {
            Controller.Run<ChallengeSettingsPresenter.ChallengeSettingsPresenter,
                            ChallengeSettings>(challengeSettings);
        }

        /// <summary>
        /// Starts up general stream process from each connected camera
        /// </summary>
        public void StartStream()
        {
            InitializeDevices();
            InitializeBuffers();
            BindPlayersToCameras();
            StartDevices();
            InitializeTempFpses();
            InitializeTimeAxisTimer();
            InitializeOneSecondTimer();
            ChangeStreamingStatus(true);
            ChangeStateOfChallengeButton(true);
        }

        /// <summary>
        /// Stops stream process
        /// </summary>
        public void StopStream()
        {
            StopCaptureDevice();
            ResetTimeAxis();
            ChangeStreamingStatus(false);
            ChangeStateOfChallengeButton(false);
        }

        /// <summary>
        /// Writes challenge videos into file system
        /// </summary>
        public void CreateChallange()
        {
            if (streaming)
            {
                var challengeTime = GetChallengeTime();
                challengeDirectoryPath = FormatChallengeDirectoryPath(challengeTime);
                CreateDirectoryForChallenge(challengeDirectoryPath);
                ChangeActivityOfEventForPastFrames(false);
                ChangeActivityOfEventForFutureFrames(true);
                ChangeStateOfChallengeButtonIn(false, challengeSettings.NumberOfFutureFPS);
                AddMarkerOnTimeAxis();
            }
        }

        /// <summary>
        /// Opens the folder for current game in file system
        /// </summary>
        public void OpenGameFolder()
        {
            FileService.OpenFileOrFolder(gameInformation.DirectoryName);
        }

        /// <summary>
        /// Stops capturing from all connected devices
        /// </summary>
        public void FormClosing()
        {
            StopCaptureDevice();
        }
    }
}
