using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter
    {
        /// <summary>
        /// event which adds and supply concrete number of FPS object
        /// in buffer for past frames
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void InternalTimerEventForPastFrames()
        {
            if (challengeBuffers.HaveToRemovePastFps())
            {
                challengeBuffers.RemoveFirstFpsFromPastBuffer();
            }
            challengeBuffers.AddPastFpses(fpsContainer);
            fpsContainer = new FpsContainer(camerasContainer.GetCamerasNames);
        }

        /// <summary>
        /// Process frames for future collection
        /// </summary>
        private void InternalTimerEventForFutureFrames()
        {
            if (challengeBuffers.HaveToAddFutureFps())
            {
                challengeBuffers.AddFutureFpses(fpsContainer);
                fpsContainer = new FpsContainer(camerasContainer.GetCamerasNames);
            }
            else
            {
                ChangeActivityOfEventForFutureFrames(false);
                WriteChallangeAsVideo();
                fpsContainer = new FpsContainer(camerasContainer.GetCamerasNames);
                InitializeChallengeBuffers();
                ChangeActivityOfEventForPastFrames(true);
            }
        }
        
        private void InitializeChallengeBuffers()
        {
            challengeBuffers = new ChallengeBuffers(camerasContainer,
                        challengeSettings.NumberOfPastFPS,
                        challengeSettings.NumberOfFutureFPS);
        }

        /// <summary>
        /// subscribe if isActive true
        /// unsubscribe if isActive false
        /// </summary>
        /// <param name="isActive"></param>
        private void ChangeActivityOfEventForFutureFrames(bool isActive)
        {
            if (isActive)
            {
                internalChallengeTimer.EnableTimerEvent(InternalTimerEventForFutureFrames);
                IsEventForFutureFramesActive = true;
            }
            else
            {
                internalChallengeTimer.DisableTimerEvent();
                IsEventForFutureFramesActive = false;
            }
        }

        /// <summary>
        /// subscribe if isActive true
        /// unsubscribe if isActive false
        /// </summary>
        /// <param name="isActive"></param>
        private void ChangeActivityOfEventForPastFrames(bool isActive)
        {
            if (isActive)
            {
                internalChallengeTimer.EnableTimerEvent(InternalTimerEventForPastFrames);
                IsEventForPastFramesActive = true;
            }
            else
            {
                internalChallengeTimer.DisableTimerEvent();
                IsEventForPastFramesActive = false;
            }
        }

        /// <summary>
        /// Draws player panel
        /// </summary>
        private void DrawPlayers()
        {
            if (playerPanelSettings != null)
            {
                View.DrawPlayers(playerPanelSettings, camerasContainer.CamerasNumber);
            }
        }
        #region settings
        /// <summary>
        /// read player panel settings from xml file
        /// this action occures only when we run our form
        /// </summary>
        /// <returns></returns>
        private PlayerPanelSettings GetPlayerPanelSettings()
        {
            var playerPanelSettingService =
                 new SettingsService<PlayerPanelSettings>(
                                new PlayerPanelSettingsParser(new FileWorker()));
            return playerPanelSettingService.
                        GetSetting();
        }

        /// <summary>
        /// read challenge settings from xml file
        /// this action occures only when we run our form
        /// </summary>
        /// <returns></returns>
        private ChallengeSettings GetChallengeSettings()
        {
            var challengeSettingService =
                 new SettingsService<ChallengeSettings>(
                                new ChallengeSettingsParser(new FileWorker()));
            return challengeSettingService.
                        GetSetting();
        }

        /// <summary>
        /// read ftp settings from xml file
        /// this action occures only when we run our form
        /// </summary>
        /// <returns></returns>
        private FtpSettings GetFtpSettings()
        {
            var ftpSettingsService =
                 new SettingsService<FtpSettings>(
                                new FtpSettingsParser(new FileWorker()));
            return ftpSettingsService.
                        GetSetting();
        }
        #endregion



        /// <summary>
        /// Initializes devices
        /// </summary>
        private CamerasContainer InitializeDevices()
        {
            pylonCameraProvider = new PylonCameraProvider();
            var camerasInfo = pylonCameraProvider.GetConnectedCameras();
            camerasContainer = new CamerasContainer(camerasInfo);
            IsDeviceListEmpty = camerasContainer.IsEmpty() ? true : false;
            return camerasContainer;
        }

        /// <summary>
        /// Initializes player for each camera
        /// </summary>
        private void BindPlayersToCameras()
        {
            Queue<string> camerasNames = new Queue<string>();
            foreach (string cameraName in camerasContainer.GetCamerasNames)
            {
                camerasNames.Enqueue(cameraName);
            }
            View.BindPlayersToCameras(camerasNames);
        }

        /// <summary>
        /// Starts all devices
        /// </summary>
        private void StartDevices()
        {
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                EventSubscriber.AddEventHandler(camera, "NewFrameEvent", Camera_NewFrameEvent);
                camera.Start();
            }
        }

        /// <summary>
        /// Process new frame from device cameraFullName
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="cameraName"></param>
        private void Camera_NewFrameEvent(object sender, EventArgs args)
        {
            NewFrameEventArgs newFrameEventArgs = args as NewFrameEventArgs;
            View.DrawNewFrame(newFrameEventArgs.Frame, newFrameEventArgs.CameraName);
        }

        /// <summary>
        /// Initialize timer on time axis
        /// </summary>
        private void InitializeTimeAxisTimer()
        {
            View.InitializeTimer();
        }

        /// <summary>
        /// Initialize internal timer to create FPS object every second
        /// </summary>
        private void InitializeInternalTimer()
        {
            fpsContainer = new FpsContainer(camerasContainer.GetCamerasNames);
            internalChallengeTimer = new InternalChallengeTimer(1000, true);
            internalChallengeTimer.Start();
            internalChallengeTimer.EnableTimerEvent(InternalTimerEventForPastFrames);
        }

        /// <summary>
        /// makes challenge button enable
        /// </summary>
        private void ChangeStateOfChallengeButton(bool isEnable)
        {
            View.ToggleChallengeButton(isEnable);
        }

        /// <summary>
        /// makes start button enable
        /// </summary>
        private void ChangeStateOfStartButton(bool isEnable)
        {
            View.ToggleStartButton(isEnable);
        }

        /// <summary>
        /// makes stop button enable
        /// </summary>
        private void ChangeStateOfStopButton(bool isEnable)
        {
            View.ToggleStopButton(isEnable);
        }

        /// <summary>
        /// makes challenge button invisible
        /// and makes challenge recording image visible
        /// </summary>
        /// <param name="numberOfSeconds"></param>
        private void SetUIAsChallengeRecordingOn(int numberOfSeconds)
        {
            MakeChallengeButtonInvisibleOn(numberOfSeconds);
            MakeChallengeRecordingImageVisibleOn(numberOfSeconds);
        }

        /// <summary>
        /// Makes visible challenge recording image on pointed number of seconds
        /// </summary>
        /// <param name="numberOfSeconds"></param>
        private void MakeChallengeRecordingImageVisibleOn(int numberOfSeconds)
        {
            View.MakeChallengeRecordingImageVisibleOn(numberOfSeconds);
            IsChallengeRecordingImageVisible = true;
        }

        /// <summary>
        /// Makes invisible add challenge button on pointed number of seconds
        /// </summary>
        /// <param name="isVisible"></param>
        /// <param name="numberOfSeconds"></param>
        private void MakeChallengeButtonInvisibleOn(int numberOfSeconds)
        {
            View.MakeChallengeButtonInvisibleOn(numberOfSeconds);
            IsChallengeButtonVisible = false;
        }      

        /// <summary>
        /// Resets timer on time axis and clear it from markers
        /// </summary>
        private void ResetTimeAxis()
        {
            View.ResetTimeAxis();
            WasTimeAxisResetted = true;
        }

        /// <summary>
        /// set streaming state on "state"
        /// </summary>
        /// <param name="state"></param>
        private void ChangeStreamingStatus(bool state)
        {
            IsStreamProcessOn = state;
            if (IsStreamProcessOn)
            {
                ChangeStateOfChallengeButton(true);
                ChangeStateOfStartButton(false);
                ChangeStateOfStopButton(true);
            }
            else
            {
                ChangeStateOfChallengeButton(false);
                ChangeStateOfStartButton(true);
                ChangeStateOfStopButton(false);
            }
            View.ToggleVisibilityOfViewLastChallengeButton();
        }

        /// <summary>
        /// Draws marker on time axis
        /// </summary>
        private void AddMarkerOnTimeAxis(string pathToChallenge)
        {
            View.AddMarkerOnTimeAxis(pathToChallenge);
            MarkerWasAddedOntoTimeAxis = true;
        }

        /// <summary>
        /// Gets elapsed time from start of streaming
        /// </summary>
        /// <returns></returns>
        private string GetChallengeTime()
        {
            var elapsedTime = View.GetElapsedTime;
            ElapsedTimeWasGot = true;
            return elapsedTime;
        }

        /// <summary>
        /// Writes challenge videos in file system
        /// </summary>
        private void WriteChallangeAsVideo()
        {
            SetChallengeDirectoryPath();
            var challengeWriter = new ChallengeWriter(challengeBuffers,
                                 camerasContainer, challenge.PathToChallengeDirectory);
            challengeWriter.WriteChallenge();
            challengeBuffers.ClearBuffers();
        }
        
        private void SetChallengeDirectoryPath()
        {
            var pathToRootDirectory = challenge.PathToRootDirectory;
            var challengeFolderName = challenge.ChallengeFolderName;
            challenge.PathToChallengeDirectory = pathToRootDirectory + @"\" +
                fileService.FilterFolderName(challengeFolderName) + @"\";
        }

        /// <summary>
        /// Draws challenge recording image instead of Challenge button
        /// </summary>
        private void DrawChallengeRecordingImage()
        {
            View.DrawChallengeRecordingImage();
        }

        /// <summary>
        /// Stops capturing of devices
        /// </summary>
        public void StopCaptureDevice()
        {
            if (camerasContainer != null)
            {
                camerasContainer.StopAllCameras();
            }
            IsCaptureDevicesEnable = false;
        }

        private void CreateDirectoryForChallenge()
        {
            fileService.CreateDirectory(challenge.PathToChallengeDirectory);
        }

        private void ShowMessage(MessageType type)
        {
            ChallengeMessage message = messageParser.GetMessage(type);
            View.ShowMessage(message);
        }
    }
}
