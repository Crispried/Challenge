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
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System.Drawing;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Abstract;
using System.Linq;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private PlayerPanelSettings playerPanelSettings;
        private ChallengeSettings challengeSettings;
        //
        private GameInformation gameInformation;
        // video streaming
        private CamerasContainer camerasContainer;
        private PylonCameraProvider pylonCameraProvider;
        private List<Device> camerasInfo;

        // challenge
        private ChallengeBuffers challengeBuffers;
        private FpsContainer fpsContainer;
        private InternalChallengeTimer internalChallengeTimer;
        private ChallengeObject challenge;

        private Dictionary<string, string> camerasNames;

        public MainPresenter(IApplicationController controller,
                             IMainView mainView) : 
                             base(controller, mainView)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.OpenPlayerPanelSettings +=
                        ChangePlayerPanelSettings;
            View.OpenChallengeSettings +=
                                    ChangeChallengeSettings;
            View.OpenDevicesList += ShowDevicesList;
            View.StartStream += StartStream;
            View.StopStream += StopStream;
            View.OpenGameFolder += OpenGameFolder;
            View.MainFormClosing += FormClosing;
            View.CreateChallange += CreateChallange;
            View.NewFrameCallback += AddNewFrame;
            View.PassCamerasNamesToPresenterCallback += PassCamerasNamesToPresenter;
        }
        
        public GameInformation GameInformation
        {
            set
            {
                gameInformation = value;
            }
        }   

        public InternalChallengeTimer InternalChallengeTimer
        {
            set
            {
                internalChallengeTimer = value;
            }
        }

        public ChallengeSettings ChallengeSettings
        {
            set
            {
                challengeSettings = value;
            }
        }
    }
}
