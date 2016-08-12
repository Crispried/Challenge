using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using Challange.Domain.Entities;
using System.Timers;
using System.Threading.Tasks;
using AForge.Video.FFMPEG;
using Challange.Domain.Services.Challange;

namespace Challange.Presenter.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private PlayerPanelSettings playerPanelSettings;
        // video streaming
        private FilterInfoCollection VideoCaptureDevices;
        private List<VideoCaptureDevice> Devices;
        private bool streaming;

        private int numberOfChallangeFPS = 11;
        private List<FPS> pastFrames;
        private List<FPS> futureFrames;
        private List<FPS> video;
        private FPS tempFPS;
        private Timer oneSecondTimer;
        private ChallengeWriter challengeWriter;

        public MainPresenter(IApplicationController controller,
                             IMainView mainView) : 
                             base(controller, mainView)
        {
            View.OpenPlayerPanelSettings +=
                                    ChangePlayerPanelSettings;
            View.StartStream += StartStream;
            View.StopStream += StopStream;
            View.MainFormClosing += StopCaptureDevice;
            View.CreateChallange += CreateChallange;
            View.NewFrameCallback += AddNewFrame;
            Devices = new List<VideoCaptureDevice>();
        }

        private void AddNewFrame()
        {
            tempFPS.Frames.Add(View.CurrentFrame);
        }

        private void OnOneSecondTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (pastFrames.Count > numberOfChallangeFPS)
            {
                pastFrames.RemoveAt(0);
            }
            pastFrames.Add(tempFPS);
            tempFPS = new FPS();
        }

        private void OnOneSecondTimedSecondEvent(Object source, ElapsedEventArgs e)
        {
            if (futureFrames.Count < numberOfChallangeFPS)
            {
                futureFrames.Add(tempFPS);
                tempFPS = new FPS();
            }
            else
            {
                WriteChallangeAsVideo();
                oneSecondTimer.Elapsed -= new ElapsedEventHandler(OnOneSecondTimedSecondEvent);
                oneSecondTimer.Elapsed += new ElapsedEventHandler(OnOneSecondTimedEvent);
            }

        }

        public override void Run()
        {
            playerPanelSettings = GetPlayerPanelSettings();
            DrawPlayers();
            Controller.Run<GameInformationPresenter>();
            View.Show();
        }

        private void ChangePlayerPanelSettings()
        {
            Controller.Run<PlayerPanelSettingsPresenter,
                           PlayerPanelSettings>(playerPanelSettings);
            DrawPlayers();
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
        #endregion

        #region start stream
        /// <summary>
        /// initialize general stream process where we have to
        /// 1. Initialize 2 buffers for past and future frames
        /// 2. Subscribe them onto frames change event
        /// 3. Initialize devices
        /// 4. Initialize frame change time on time axis
        /// 5. Initialize one second timer to create FPS object every second
        /// 6. Set streaming state as true
        /// 7. Activate challenge button
        /// </summary>
        private void StartStream()
        {
            InitializeBuffers();
            InitializeDevices();
            StartDevices();
            InitializeFrameChangeTimer();
            InitializeOneSecondTimer();
            ChangeStreamingStatus(true);
            View.MakeChallengeButtonActive();
        }

        private void InitializeBuffers()
        {
            pastFrames = new List<FPS>();
            futureFrames = new List<FPS>();
        }

        private void InitializeOneSecondTimer()
        {
            tempFPS = new FPS();
            oneSecondTimer = new Timer(1000);
            oneSecondTimer.AutoReset = true;
            oneSecondTimer.Elapsed += new ElapsedEventHandler(OnOneSecondTimedEvent);
            oneSecondTimer.Start();
        }

        private void InitializeDevices()
        {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        private void StartDevices()
        {
            int numberOfDevices = VideoCaptureDevices.Count;
            if (numberOfDevices > 0)
            {
                Devices = new List<VideoCaptureDevice>(numberOfDevices);
                for (int i = 0; i < VideoCaptureDevices.Count; i++)
                {
                    Devices.Add(
                        new VideoCaptureDevice(VideoCaptureDevices[i].MonikerString));
                    View.SubscribeNewFrameEvent(Devices[i]);
                    Devices[i].Start();
                }
            }
        }

        private void InitializeFrameChangeTimer()
        {
            View.InitializeTimer();
        }
        #endregion

        #region stop stream
        /// <summary>
        /// Stop stream process:
        /// 1. Stop streaming from devices
        /// 2. Reset time axis
        /// 3. Clear players (removes last frame from it)
        /// </summary>
        private void StopStream()
        {
            StopCaptureDevice();
            View.ResetTimeAxis();
            View.ClearPlayers();
            ChangeStreamingStatus(false);
            View.MakeChallengeButtonInactive();
        }

        /// <summary>
        /// Stop streaming from devices
        /// </summary>
        private void StopCaptureDevice()
        {
            foreach (var device in Devices)
            {
                if (device.IsRunning)
                {
                    device.Stop();
                }
            }
        }
        #endregion

        private void ChangeStreamingStatus(bool status)
        {
            streaming = status;
        }

        #region create challange
        private void CreateChallange()
        {
            if (streaming)
            {

                ChangeOneSecondElapsedEvent();
                View.MakeChallengeButtonInactiveOn(numberOfChallangeFPS);
                View.AddMarkerOnTimeAxis();
            }
        }

        private void ChangeOneSecondElapsedEvent()
        {
            oneSecondTimer.Elapsed -= new ElapsedEventHandler(OnOneSecondTimedEvent);
            oneSecondTimer.Elapsed += new ElapsedEventHandler(OnOneSecondTimedSecondEvent);
        }

        private void WriteChallangeAsVideo()
        {
            pastFrames.AddRange(futureFrames); // unite past and future frames lists
            challengeWriter = new ChallengeWriter(pastFrames);
            challengeWriter.WriteChallenge();
            pastFrames.Clear();
            futureFrames.Clear();
        }
        #endregion
    }
}
