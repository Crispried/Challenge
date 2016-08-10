using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Challange.Presenter.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private PlayerPanelSettings playerPanelSettings;
        // video streaming
        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo;
        private bool streaming;

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
            streaming = false;
            FinalVideo = new VideoCaptureDevice();
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
        /// 1. Initialize devices
        /// 2. Subscribe them onto frames change event
        /// 3. Initialize timer
        /// </summary>
        private void StartStream()
        {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (VideoCaptureDevices.Count > 0)
            {
                FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[0].MonikerString);
                View.SubscribeNewFrameEvent(FinalVideo);                    
                FinalVideo.Start();
            }
            View.InitializeTimer();
            streaming = true;
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
            streaming = false;
        }

        /// <summary>
        /// Stop streaming from devices
        /// </summary>
        private void StopCaptureDevice()
        {
            if (FinalVideo.IsRunning)
            {
                FinalVideo.Stop();
            }
        }
        #endregion

        #region create challange
        private void CreateChallange()
        {
            if (streaming)
            {
                View.AddMarketOnTimeAxis();
            }
        }
        #endregion
    }
}
