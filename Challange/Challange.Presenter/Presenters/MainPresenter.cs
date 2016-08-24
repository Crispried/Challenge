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

namespace Challange.Presenter.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private PlayerPanelSettings playerPanelSettings;
        private ChallengeSettings challengeSettings;
        //
        private GameInformation gameInformation;
        // Pylon 5
        private CamerasContainer<Camera> camerasContainer;
        private PylonCameraProvider pylonCameraProvider;
        private List<Device> camerasInfo;
        // video streaming
        private FilterInfoCollection VideoCaptureDevices;
        private List<VideoCaptureDevice> Devices;
        private bool streaming;

        // challenge
        // Dictionary<string, List<FPS>> where string is camera name
        private List<FPS> pastFrames;
        private List<FPS> futureFrames;
        private FPS tempFPS;
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
            Devices = new List<VideoCaptureDevice>();
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
            tempFPS.Frames.Add(View.CurrentFrame);
        }

        /// <summary>
        /// event which adds and supply concrete number of FPS object
        /// in buffer for past frames
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnOneSecondTimedEventForPastFrames(Object source, ElapsedEventArgs e)
        {
            if (HaveToRemovePastFPS())
            {
                RemoveFirstFPSFromPastBuffer();
            }
            AddPastFPS(tempFPS);
            InitializeTempFPS();
        }

        /// <summary>
        /// check is past frame buffer count equals
        /// to necessary number of past FPS
        /// </summary>
        /// <returns></returns>
        private bool HaveToRemovePastFPS()
        {
            return pastFrames.Count == challengeSettings.NumberOfPastFPS;
        }

        /// <summary>
        /// for example our past buffer is only for 20 FPS objects
        /// so on 21 second we have to remove the first object from this buffer
        /// </summary>
        private void RemoveFirstFPSFromPastBuffer()
        {
            pastFrames.RemoveAt(0);
        }

        /// <summary>
        /// adds past fps object into buffer for past frames
        /// </summary>
        /// <param name="fps"></param>
        private void AddPastFPS(FPS fps)
        {
            pastFrames.Add(fps);
        }

        /// <summary>
        /// this is temporary object which will keep fps objects
        /// which we create every second
        /// </summary>
        private void InitializeTempFPS()
        {
            tempFPS = new FPS();
        }

        private void OnOneSecondTimedEventForFutureFrames(Object source, ElapsedEventArgs e)
        {
            if (HaveToAddFutureFPS())
            {
                AddFutureFPS(tempFPS);
                InitializeTempFPS();
            }
            else
            {
                WriteChallangeAsVideo();
                ChangeActivityOfEventForFutureFrames(false);
                ChangeActivityOfEventForPastFrames(true);
            }
        }

        /// <summary>
        /// check is future frame buffer count equals
        /// to necessary number of future FPS
        /// </summary>
        /// <returns></returns>
        private bool HaveToAddFutureFPS()
        {
            return futureFrames.Count < challengeSettings.NumberOfFutureFPS;
        }

        /// <summary>
        /// adds future fps object into buffer for future frames
        /// </summary>
        /// <param name="fps"></param>
        private void AddFutureFPS(FPS fps)
        {
            futureFrames.Add(fps);
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
            InitializeBuffers();
            InitializeDevices();
            StartDevices();
            InitializeTempFPS();
            InitializeTimeAxisTimer();
            InitializeOneSecondTimer();
            ChangeStreamingStatus(true);
            ChangeStateOfChallengeButton(true);
        }

        /// <summary>
        /// Initialize 2 buffers for past and future frames
        /// </summary>
        private void InitializeBuffers()
        {
            pastFrames = new List<FPS>();
            futureFrames = new List<FPS>();
        }

        /// <summary>
        /// Initialize devices
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

        private void StartDevices()
        {
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                camera.NewFrameEvent += Camera_NewFrameEvent;
                camera.Start();
            }
        }

        private void Camera_NewFrameEvent(Bitmap frame)
        {
            View.DrawNewFrame(frame);
        }

        /// <summary>
        /// Subscribe them onto frames change event
        /// </summary>
        /*   private void StartDevices()
           {
               int numberOfDevices = GetNumberOfConnectedCameras();
               if (numberOfDevices > 0)
               {
                   InitializeDevicesList(numberOfDevices);
                   for (int i = 0; i < VideoCaptureDevices.Count; i++)
                   {
                       AddDeviceIntoDevicesList(
                           new VideoCaptureDevice(VideoCaptureDevices[i].MonikerString));
                       SubscribeDeviceOnFrameChangeEvent(Devices[i]);
                       StartDevice(Devices[i]);
                   }
               }
           }*/

        /// <summary>
        /// Returns number of connected devices
        /// </summary>
        /// <returns></returns>
        private int GetNumberOfConnectedCameras()
        {
            return VideoCaptureDevices.Count;
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
        /// Subscribes device on frame change event
        /// </summary>
        /// <param name="device"></param>
        private void SubscribeDeviceOnFrameChangeEvent(VideoCaptureDevice device)
        {
            View.SubscribeNewFrameEvent(device);
        }

        /// <summary>
        /// Starts device (turns it on)
        /// </summary>
        /// <param name="device"></param>
        private void StartDevice(VideoCaptureDevice device)
        {
            device.Start();
        }

        /// <summary>
        /// Adds device into Devices
        /// </summary>
        /// <param name="device"></param>
        private void AddDeviceIntoDevicesList(VideoCaptureDevice device)
        {
            Devices.Add(device);
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
            ClearPlayers();
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

        /// <summary>
        /// Erase last frame from every player in player panel
        /// </summary>
        private void ClearPlayers()
        {
            View.ClearPlayers();
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
            var video = UnitePastAndFutureFrames();
            var pathToChallenge = FormatFullPathToChallenge();
            var challengeWriter = new ChallengeWriter(video, pathToChallenge);
            challengeWriter.WriteChallenge();
            ClearPastAndFutureBuffers();
        }

        private List<FPS> UnitePastAndFutureFrames()
        {
            List<FPS> video = new List<FPS>();
            video.AddRange(pastFrames);
            video.AddRange(futureFrames);
            return video;
        }

        private void ClearPastAndFutureBuffers()
        {
            pastFrames.Clear();
            futureFrames.Clear();
        }

        private string FormatFullPathToChallenge()
        {
            return challengeDirectoryPath + @"\" + "testtest.mp4";
        }
        #endregion
    }
}
