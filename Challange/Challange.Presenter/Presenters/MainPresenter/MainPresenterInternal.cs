using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Challenge;
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
        private void OnOneSecondTimedEventForPastFrames(Object source, ElapsedEventArgs e)
        {
            if (HaveToRemovePastFps())
            {
                RemoveFirstFpsFromPastBuffer();
            }
            AddPastFpses();
            InitializeTempFpses();
        }

        /// <summary>
        /// check is past frame buffer count equals
        /// to necessary number of past FPS
        /// </summary>
        /// <returns></returns>
        private bool HaveToRemovePastFps()
        {
            var pastFrames = pastCameraRecords.Values.FirstOrDefault();
            return pastFrames.Count == challengeSettings.NumberOfPastFPS;
        }

        /// <summary>
        /// for example our past buffer is only for 20 FPS objects
        /// so on 21 second we have to remove the first object from this buffer
        /// </summary>
        private void RemoveFirstFpsFromPastBuffer()
        {
            var fpsesToRemove = new Dictionary<string, FPS>();
            foreach (var pastFrames in pastCameraRecords)
            {
                fpsesToRemove.Add(pastFrames.Key, pastFrames.Value[0]);
                pastFrames.Value.RemoveAt(0);
            }
            foreach (var fpsToRemove in fpsesToRemove.Values)
            {
                foreach (var frame in fpsToRemove.Frames)
                {
                    frame.Dispose();
                }
            }
        }

        /// <summary>
        /// adds past fps objects into buffer for past frames
        /// </summary>
        private void AddPastFpses()
        {
            List<FPS> temp;
            foreach (var tempFps in tempFpses)
            {
                if (pastCameraRecords.TryGetValue(tempFps.Key, out temp))
                {
                    temp.Add(tempFps.Value);
                }
                else
                {
                    temp = new List<FPS>();
                    temp.Add(tempFps.Value);
                    pastCameraRecords.Add(tempFps.Key, temp);
                }
            }
        }

        /// <summary>
        /// this is temporary object which will keep fps objects
        /// from all cameras which we create every second
        /// </summary>
        private void InitializeTempFpses()
        {
            tempFpses = new Dictionary<string, FPS>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                tempFpses.Add(camera.FullName, new FPS());
            }

        }

        /// <summary>
        /// Process frames for future collection
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnOneSecondTimedEventForFutureFrames(Object source, ElapsedEventArgs e)
        {
            if (HaveToAddFutureFps())
            {
                AddFutureFpses();
                InitializeTempFpses();
            }
            else
            {
                ChangeActivityOfEventForFutureFrames(false);
                WriteChallangeAsVideo();
                InitializeTempFpses();
                InitializeBuffers();
                ChangeActivityOfEventForPastFrames(true);
            }
        }

        /// <summary>
        /// check is future frame buffer count equals
        /// to necessary number of future FPS
        /// </summary>
        /// <returns></returns>
        private bool HaveToAddFutureFps()
        {
            var futureFrames = futureCameraRecords.Values.First();
            return futureFrames.Count != challengeSettings.NumberOfFutureFPS;
        }

        /// <summary>
        /// adds future fps objects into buffer for future frames
        /// </summary>
        private void AddFutureFpses()
        {
            List<FPS> temp;
            foreach (var tempFps in tempFpses)
            {
                if (futureCameraRecords.TryGetValue(tempFps.Key, out temp))
                {
                    temp.Add(tempFps.Value);
                }
                else
                {
                    temp = new List<FPS>();
                    temp.Add(tempFps.Value);
                    futureCameraRecords.Add(tempFps.Key, temp);
                }
            }
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
                oneSecondTimer.Elapsed += new ElapsedEventHandler(OnOneSecondTimedEventForFutureFrames);
            }
            else
            {
                oneSecondTimer.Elapsed -= new ElapsedEventHandler(OnOneSecondTimedEventForFutureFrames);
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
                oneSecondTimer.Elapsed += new ElapsedEventHandler(OnOneSecondTimedEventForPastFrames);
            }
            else
            {
                oneSecondTimer.Elapsed -= new ElapsedEventHandler(OnOneSecondTimedEventForPastFrames);
            }
        }

        /// <summary>
        /// Just initialize GameInformation object
        /// </summary>
        private void InitializeGameInformation()
        {
            gameInformation = new GameInformation();
        }

        /// <summary>
        /// Draws player panel
        /// </summary>
        private void DrawPlayers()
        {
            if (playerPanelSettings != null)
            {
                View.DrawPlayers(playerPanelSettings);
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
                                new PlayerPanelSettingsParser());
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
                                new ChallengeSettingsParser());
            return challengeSettingService.
                        GetSetting();
        }
        #endregion



        /// <summary>
        /// Initializes devices
        /// </summary>
        private void InitializeDevices()
        {
            InitializeDevicesList();
            camerasContainer = new CamerasContainer<Camera>();
            PylonCamera tmpCamera;
            foreach (var cameraInfo in camerasInfo)
            {
                tmpCamera = new PylonCamera(cameraInfo.Index, cameraInfo.FullName);
                camerasContainer.AddCamera(tmpCamera);
            }
        }

        /// <summary>
        /// Initialize 2 buffers for past and future frames
        /// </summary>
        private void InitializeBuffers()
        {
            pastCameraRecords = new Dictionary<string, List<FPS>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                pastCameraRecords.Add(camera.FullName, new List<FPS>());
            }
            futureCameraRecords = new Dictionary<string, List<FPS>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                futureCameraRecords.Add(camera.FullName, new List<FPS>());
            }
        }

        /// <summary>
        /// Initializes player for each camera
        /// </summary>
        private void BindPlayersToCameras()
        {
            Queue<string> camerasNames = new Queue<string>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                camerasNames.Enqueue(camera.FullName);
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
                camera.NewFrameEvent += Camera_NewFrameEvent;
                camera.Start();
            }
        }

        /// <summary>
        /// Process new frame from device cameraName
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="cameraName"></param>
        private void Camera_NewFrameEvent(Bitmap frame, string cameraName)
        {
            View.DrawNewFrame(frame, cameraName);
        }

        /// <summary>
        /// Initializes devices list
        /// </summary>
        private void InitializeDevicesList()
        {
            //Devices = new List<VideoCaptureDevice>(capacity);
            camerasInfo = pylonCameraProvider.GetConnectedCameras();
        }

        /// <summary>
        /// Initialize timer on time axis
        /// </summary>
        private void InitializeTimeAxisTimer()
        {
            View.InitializeTimer();
        }

        /// <summary>
        /// Initialize one second timer to create FPS object every second
        /// </summary>
        private void InitializeOneSecondTimer()
        {
            oneSecondTimer = new Timer(1000);
            oneSecondTimer.AutoReset = true;
            oneSecondTimer.Elapsed += new ElapsedEventHandler(OnOneSecondTimedEventForPastFrames);
            oneSecondTimer.Start();
        }

        /// <summary>
        /// makes challenge button enable
        /// </summary>
        private void ChangeStateOfChallengeButton(bool isEnable)
        {
            View.ToggleChallengeButton(isEnable);
        }

        /// <summary>
        /// Changes the state of challenge button in pointed seconds
        /// if true - active
        /// if false - not active
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="numberOfSeconds"></param>
        private void ChangeStateOfChallengeButtonIn(bool isEnable, int numberOfSeconds)
        {
            View.ToggleChallengeButtonIn(isEnable, numberOfSeconds);
        }      

        /// <summary>
        /// Resets timer on time axis and clear it from markers
        /// </summary>
        private void ResetTimeAxis()
        {
            View.ResetTimeAxis();
        }

        /// <summary>
        /// set streaming state on "state"
        /// </summary>
        /// <param name="state"></param>
        private void ChangeStreamingStatus(bool state)
        {
            streaming = state;
        }

        /// <summary>
        /// Draws marker on time axis
        /// </summary>
        private void AddMarkerOnTimeAxis()
        {
            View.AddMarkerOnTimeAxis();
        }

        /// <summary>
        /// Gets elapsed time from start of streaming
        /// </summary>
        /// <returns></returns>
        private string GetChallengeTime()
        {
            return View.GetElapsedTime;
        }

        /// <summary>
        /// Replace ':' on '_'
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private string FormatChallengeDirectoryPath(string folderName)
        {
            return gameInformation.DirectoryName + @"\" +
                        FileService.FilterFolderName(folderName);
        }

        /// <summary>
        /// Creates directory in file system for current challenge
        /// </summary>
        /// <param name="name"></param>
        private void CreateDirectoryForChallenge(string name)
        {
            FileService.CreateDirectory(name);
        }

        /// <summary>
        /// Writes challenge videos in file system
        /// </summary>
        private void WriteChallangeAsVideo()
        {
            var videos = UnitePastAndFutureFrames();
            var pathToChallenge = FormatPathToChallenge();
            var challengeWriter = new ChallengeWriter(videos, pathToChallenge);
            challengeWriter.WriteChallenge();
            ClearPastAndFutureBuffers();
        }

        /// <summary>
        /// Unites past and future frames collection in one
        /// </summary>
        /// <returns></returns>
        private List<Video> UnitePastAndFutureFrames()
        {
            var videos = new List<Video>();
            List<FPS> tempVideoFrames;
            string currentVideoName;
            foreach (var pastFrames in pastCameraRecords)
            {
                foreach (var futureFrames in futureCameraRecords)
                {
                    if (pastFrames.Key == futureFrames.Key)
                    {
                        tempVideoFrames = new List<FPS>();
                        tempVideoFrames.AddRange(pastFrames.Value);
                        tempVideoFrames.AddRange(futureFrames.Value);
                        View.CamerasNames.TryGetValue(
                                pastFrames.Key, out currentVideoName);
                        videos.Add(new Video(currentVideoName, tempVideoFrames));
                        break;
                    }
                }
            }
            return videos;
        }

        /// <summary>
        /// Clears buffers for past and future frames
        /// </summary>
        private void ClearPastAndFutureBuffers()
        {
            pastCameraRecords.Clear();
            futureCameraRecords.Clear();
        }

        /// <summary>
        /// Formats path to challenge
        /// </summary>
        /// <returns></returns>
        private string FormatPathToChallenge()
        {
            return challengeDirectoryPath + @"\";
        }

        /// <summary>
        /// Stops capturing of devices
        /// </summary>
        public void StopCaptureDevice()
        {
            if (camerasContainer != null)
            {
                foreach (var camera in camerasContainer.GetCameras)
                {
                    camera.Stop();
                }
            }
        }

        #region Show error messages
        private void ShowSettingsFileParseProblemError()
        {
            View.ShowSettingsFileParseProblemError();
        }
        #endregion
    }
}
