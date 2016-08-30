using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using Challange.Domain.Entities;
using System.Timers;
using Challange.Domain.Services.Challenge;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System.Drawing;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Abstract;
using System.Linq;

namespace Challange.Presenter.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private PlayerPanelSettings playerPanelSettings;
        private ChallengeSettings challengeSettings;
        //
        private GameInformation gameInformation;
        // video streaming
        private CamerasContainer<Camera> camerasContainer;
        private PylonCameraProvider pylonCameraProvider;
        private List<Device> camerasInfo;
        private bool streaming;

        // challenge
        private Dictionary<string, List<FPS>> pastCameraRecords;
        private Dictionary<string, List<FPS>> futureCameraRecords;
        private Dictionary<string, FPS> tempFpses;
        private Timer oneSecondTimer;
        private string challengeDirectoryPath;

        public MainPresenter(IApplicationController controller,
                             IMainView mainView) : 
                             base(controller, mainView)
        {
            View.OpenPlayerPanelSettings +=
                                    ChangePlayerPanelSettings;
            View.OpenChallengeSettings +=
                                    ChangeChallengeSettings;
            View.OpenDevicesList += ShowDevicesList;
            View.StartStream += StartStream;
            View.StopStream += StopStream;
            View.OpenGameFolder += OpenGameFolder;
            View.MainFormClosing += StopCaptureDevice;
            View.CreateChallange += CreateChallange;
            View.NewFrameCallback += AddNewFrame;
            pylonCameraProvider = new PylonCameraProvider();
        }

        private void ShowDevicesList()
        {
            InitializeDevicesList();
            Controller.Run<CamerasPresenter,
                List<Device>>(camerasInfo);
        }

        /// <summary>
        /// add new frame into tempFPS Frames collection
        /// </summary>
        private void AddNewFrame()
        {
            string cameraName = View.CurrentFrameInfo.Item1;
            Bitmap currentFrame = View.CurrentFrameInfo.Item2;
            FPS tempFPS;
            tempFpses.TryGetValue(cameraName, out tempFPS);
            tempFPS.Frames.Add(currentFrame);
        }

        /// <summary>
        /// event which adds and supply concrete number of FPS object
        /// in buffer for past frames
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnOneSecondTimedEventForPastFrames(Object source, ElapsedEventArgs e)
        {
            lock (tempFpses)
            {
                if (HaveToRemovePastFps())
                {
                    RemoveFirstFpsFromPastBuffer();
                }
                AddPastFpses();
                InitializeTempFpses();
            }
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
            foreach (var pastFrames in pastCameraRecords.Values)
            {
                pastFrames.RemoveAt(0);
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
                if(pastCameraRecords.TryGetValue(tempFps.Key, out temp))
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
                tempFpses.Add(camera.Name, new FPS());
            }

        }

        private void OnOneSecondTimedEventForFutureFrames(Object source, ElapsedEventArgs e)
        {
            lock (tempFpses)
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

        public override void Run()
        {
            playerPanelSettings = GetPlayerPanelSettings();
            challengeSettings = GetChallengeSettings();
            DrawPlayers();
            // we need to keep game information 
            InitializeGameInformation();
            Controller.Run<GameInformationPresenter,
                           GameInformation>(gameInformation);
            View.Show();
        }

        private void InitializeGameInformation()
        {
            gameInformation = new GameInformation();
        }

        private void ChangePlayerPanelSettings()
        {
            Controller.Run<PlayerPanelSettingsPresenter,
                           PlayerPanelSettings>(playerPanelSettings);
            DrawPlayers();
        }

        private void ChangeChallengeSettings()
        {
            Controller.Run<ChallengeSettingsPresenter,
                            ChallengeSettings>(challengeSettings);
        }

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

        #region start stream
        /// <summary>
        /// initialize general stream process 
        /// </summary>
        private void StartStream()
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
        /// Initializes devices
        /// </summary>
        private void InitializeDevices()
        {
            InitializeDevicesList();
            camerasContainer = new CamerasContainer<Camera>();
            PylonCamera tmpCamera;
            foreach (var cameraInfo in camerasInfo)
            {
                tmpCamera = new PylonCamera(cameraInfo.Index);
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
                pastCameraRecords.Add(camera.Name, new List<FPS>());
            }
            futureCameraRecords = new Dictionary<string, List<FPS>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                futureCameraRecords.Add(camera.Name, new List<FPS>());
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
                camerasNames.Enqueue(camera.Name);
            }
            View.BindPlayersToCameras(camerasNames);
        }

        private void StartDevices()
        {
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                camera.NewFrameEvent += Camera_NewFrameEvent;
                camera.Start();
            }
        }

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

        private void ChangeStateOfChallengeButtonIn(bool isEnable, int numberOfSeconds)
        {
            View.ToggleChallengeButtonIn(isEnable, numberOfSeconds);
        }
        #endregion

        private void OpenGameFolder()
        {
            FileService.OpenFileOrFolder(gameInformation.DirectoryName);
        }

        #region stop stream
        /// <summary>
        /// Stop stream process:
        /// </summary>
        private void StopStream()
        {
            StopCaptureDevice();
            ResetTimeAxis();
            ChangeStreamingStatus(false);
            ChangeStateOfChallengeButton(false);
        }

        /// <summary>
        /// Stop streaming from devices
        /// </summary>
        private void StopCaptureDevice()
        {
            if (camerasContainer != null)
            {
                foreach (var camera in camerasContainer.GetCameras)
                {
                    camera.Stop();
                }
            }
        }

        /// <summary>
        /// Resets timer on time axis and clear it from markers
        /// </summary>
        private void ResetTimeAxis()
        {
            View.ResetTimeAxis();
        }
        #endregion

        /// <summary>
        /// set streaming state on "state"
        /// </summary>
        /// <param name="state"></param>
        private void ChangeStreamingStatus(bool state)
        {
            streaming = state;
        }

        #region create challange
        private void CreateChallange()
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

        private void AddMarkerOnTimeAxis()
        {
            View.AddMarkerOnTimeAxis();
        }

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

        private void CreateDirectoryForChallenge(string name)
        {
            FileService.CreateDirectory(name);
        }

        private void WriteChallangeAsVideo()
        {
            var videos = UnitePastAndFutureFrames();
            var pathToChallenge = FormatPathToChallenge();
            var challengeWriter = new ChallengeWriter(videos, pathToChallenge);
            challengeWriter.WriteChallenge();
            ClearPastAndFutureBuffers();
        }

        private List<Video> UnitePastAndFutureFrames()
        {
            var video = new List<Video>();
            List<FPS> tempVideoFrames;
            foreach (var pastFrames in pastCameraRecords)
            {
                foreach (var futureFrames in futureCameraRecords)
                {
                    if(pastFrames.Key == futureFrames.Key)
                    {
                        tempVideoFrames = new List<FPS>();
                        tempVideoFrames.AddRange(pastFrames.Value);
                        tempVideoFrames.AddRange(futureFrames.Value);
                        video.Add(new Video(pastFrames.Key, tempVideoFrames));
                        break;
                    }
                }
            }
            return video;
        }

        private void ClearPastAndFutureBuffers()
        {
            pastCameraRecords.Clear();
            futureCameraRecords.Clear();
        }

        private string FormatPathToChallenge()
        {
            return challengeDirectoryPath + @"\";
        }
        #endregion
    }
}
