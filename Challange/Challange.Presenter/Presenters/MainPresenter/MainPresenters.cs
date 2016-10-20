using Challange.Domain.Abstract;
using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.Replay;
using System.Windows.Forms;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter
    {
        public override void Run()
        {
            playerPanelSettings = GetPlayerPanelSettings();
            challengeSettings = GetChallengeSettings();
            if(challengeSettings == null)
            {
                ChallengeSettingsAreNull = true;
                ShowMessage(MessageType.ChallengeSettingsFileParseProblem);
            }
            else if(playerPanelSettings == null)
            {
                PlayerPanelSettingsAreNull = true;
                ShowMessage(MessageType.PlayerPanelSettingsFileParseProblem);
            }
            else
            {
                // we need to keep game information 
                gameInformation = new GameInformation();
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
                List<string>>(camerasContainer.GetCamerasNames);
        }

        /// <summary>
        /// Adds new frame into tempFpses collection
        /// which in fact contains past and future frames
        /// </summary>
        public void AddNewFrame()
        {
            string cameraName = View.CurrentFrameInfo.Item1;
            Bitmap currentFrame = View.CurrentFrameInfo.Item2;
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
        public void PassCamerasNamesToPresenter(string key, string cameraName)
        {
            camerasContainer.SetCameraName(key, cameraName);
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
             camerasContainer = InitializeDevices();
            if (IsDeviceListEmpty) // DONT FORGET BACK "!" !!!!!!!
            {
                InitializeChallengeBuffers();
                BindPlayersToCameras();
                InitializeTimeAxisTimer();
                InitializeInternalTimer();
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
            challenge = new ChallengeObject(
                gameInformation.DirectoryName, challengeTime);
            challenge.CreateDirectoryForChallenge();
            ChangeActivityOfEventForPastFrames(false);
            ChangeActivityOfEventForFutureFrames(true);
            SetUIAsChallengeRecordingOn(challengeSettings.NumberOfFutureFPS);
            AddMarkerOnTimeAxis(challenge.GetChallengeDirectoryPath);
        }

        /// <summary>
        /// Opens the folder for current game in file system
        /// </summary>
        public void OpenGameFolder()
        {
            FileService.OpenFileOrFolder(gameInformation.DirectoryName);
        }

        /// <summary>
        /// Opens Challenge Player form for choosed challenge
        /// </summary>
        public void OpenChallengePlayer(string pathToChallenge)
        {
            Controller.Run<ChallengePlayerPresenter.ChallengePlayerPresenter,
               string>(pathToChallenge);
        }

        /// <summary>
        /// Opens Challenge Player form for last challenge
        /// </summary>
        public void OpenChallengePlayerForLastChallenge()
        {
            // challenge.GetChallengeDirectoryPath contains last one challenge folder
            if(challenge != null)
            {
                OpenChallengePlayer(challenge.GetChallengeDirectoryPath);
            }
            else
            {
                ShowMessage(MessageType.HaveNotRecordedAnyChallengeYet);
                HaveNotRecordedAnyChallengeYetMessageShown = true;
            }
        }

        public void OpenBroadcastForm(string cameraFullName)
        {
            Camera cameraForBroadcasting = camerasContainer.GetCameraByKey(cameraFullName);
            Controller.Run<BroadcastPresenter.BroadcastPresenter, Camera>(cameraForBroadcasting);
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
