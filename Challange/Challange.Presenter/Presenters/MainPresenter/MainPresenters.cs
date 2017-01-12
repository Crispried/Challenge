using Challange.Domain.Abstract;
using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using System.Drawing;
using System;
using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter
    {
        public override void Run()
        {
            ReadAllSettings();
            CheckAllSettingsOnNull();
            if (!nullSettingsContainer.IsEmpty())
            {
                ShowMessage(MessageType.SettingsFilesParseProblem);
            }
            else
            {
                Controller.Run<GameInformationPresenter.GameInformationPresenter,
                               GameInformation>(gameInformation);
                InitializeDevices();
                DrawPlayers();
                View.Show();
            }
        }

        /// <summary>
        /// Shows form which contains list of all connected devices
        /// </summary>
        public void ShowDevicesList()
        {
            Controller.Run<CamerasPresenter.CamerasPresenter,
                ICamerasContainer>(camerasContainer);
        }

        /// <summary>
        /// Adds new frame into tempFpses collection
        /// which in fact contains past and future frames
        /// </summary>
        public void AddNewFrame()
        {
            string cameraName = View.CurrentFrameCameraName;
            Bitmap currentFrame = View.CurrentFrame;
            IFps tempFPS = fpsContainer.GetFpsByKey(cameraName);
            tempFPS.AddFrame(currentFrame);
        }

        /// <summary>
        /// Zooms in/out replayed videos in the fullscreen mode
        /// according to the mouse position
        /// </summary>
        public void MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation)
        {
            ZoomData zoomData = zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);
            View.RedrawZoomedImage(zoomData);
        }

        /// <summary>
        /// Get cameras names from view
        /// In order to pass them to ChallengeWriter
        /// </summary>
        public void SaveCamerasNames(string key, string cameraName)
        {
            camerasContainer.SetCameraName(key, cameraName);
        }

        /// <summary>
        /// Shows the form which allows to change player panel settings
        /// after that it redraws player panel with new settings
        /// </summary>
        public void ChangePlayerPanelSettings()
        {
            var oldPlayerPanelSettings =
                (PlayerPanelSettings)settingsContext.PlayerPanelSetting.Clone();
            Controller.Run<PlayerPanelSettingsPresenter.PlayerPanelSettingsPresenter,
                           PlayerPanelSettings>(settingsContext.PlayerPanelSetting);
            // we want redraw players only if there were any changes
            // in player panel settings
            if (!oldPlayerPanelSettings.Equals(settingsContext.PlayerPanelSetting))
            {
                DrawPlayers();
            }
        }

        /// <summary>
        /// Shows the form which allows to change challenge settings
        /// </summary>
        public void ChangeChallengeSettings()
        {
            Controller.Run<ChallengeSettingsPresenter.ChallengeSettingsPresenter,
                            ChallengeSettings>(settingsContext.ChallengeSetting);
        }

        /// <summary>
        /// Shows the form which allows to change ftp connection settings
        /// </summary>
        public void ChangeFtpSettings()
        {
            Controller.Run<FtpSettingsPresenter.FtpSettingsPresenter,
                            FtpSettings>(settingsContext.FtpSetting);
        }

        /// <summary>
        /// Shows the form which allows to change rewind settings
        /// </summary>
        public void ChangeRewindSettings()
        {
            Controller.Run<RewindSettingsPresenter.RewindSettingsPresenter,
                            RewindSettings>(settingsContext.RewindSetting);
        }

        /// <summary>
        /// Starts up general stream process from each connected camera
        /// </summary>
        public void StartStream()
        {
            InitializeDevices();
            if (camerasContainer.IsEmpty()) // DONT FORGET BACK "!" !!!!!!!
            {
                InitializeChallengeBuffers();
                InitializeTimeAxisTimer();
                InitializeFpsContainer();
                EnableTimerEvent(InternalTimerEventForPastFrames);
                StartDevices();
                ChangeStreamingStatus(true);
            }
            else
            {
                ShowMessage(MessageType.EmptyDeviceContainer);
            }
        }

        /// <summary>
        /// Stops stream process
        /// </summary>
        public void StopStream()
        {
            StopCaptureDevice();
            ResetTimeAxis();
            ChangeStreamingStatus(false);
        }

        /// <summary>
        /// Writes challenge videos into file system
        /// </summary>
        public void CreateChallange()
        {
            var challengeTime = GetChallengeTime();
            challenge.Initialize(gameInformation.DirectoryName, challengeTime);
            CreateDirectoryForChallenge();
            ChangeActivityOfEventForPastFrames(false);
            ChangeActivityOfEventForFutureFrames(true);
            SetUIAsChallengeRecordingOn(settingsContext.ChallengeSetting.NumberOfFutureFPS);
            AddMarkerOnTimeAxis(challenge.PathToChallengeDirectory);
        }

        /// <summary>
        /// Opens the folder for current game in file system
        /// </summary>
        public void OpenGameFolder()
        {
            fileService.OpenFileOrFolder(gameInformation.DirectoryName);
        }

        /// <summary>
        /// Opens Challenge Player form for choosed challenge
        /// </summary>
        public void OpenChallengePlayer(string pathToChallenge)
        {
            string stringForTest = @"Team1_vs_Team2(21.10.2016)\00_00_10\";
            pathToChallenge = stringForTest;
            Controller.Run<ChallengePlayerPresenter.ChallengePlayerPresenter,
               Tuple<string, RewindSettings>>
               (Tuple.Create(pathToChallenge, settingsContext.RewindSetting)); // pathToChallenge instead of stringForTest
        }

        /// <summary>
        /// Opens Challenge Player form for last challenge
        /// </summary>
        public void OpenChallengePlayerForLastChallenge()
        {
            if(challenge != null)
            {
                OpenChallengePlayer(challenge.PathToChallengeDirectory);
            }
            else
            {
                ShowMessage(MessageType.HaveNotRecordedAnyChallengeYet);
            }
        }

        /// <summary>
        /// Get cameras names from view
        /// In order to pass them to ChallengeWriter
        /// </summary>
        public void PassCamerasNamesToPresenter(string key, string cameraName)
        {
            camerasContainer.SetCameraName(key, cameraName);
        }

        public void OpenBroadcastForm(string cameraFullName)
        {
            ICamera cameraForBroadcasting = camerasContainer.GetCameraByKey(cameraFullName);
            Controller.Run<BroadcastPresenter.BroadcastPresenter, ICamera>(cameraForBroadcasting);
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
