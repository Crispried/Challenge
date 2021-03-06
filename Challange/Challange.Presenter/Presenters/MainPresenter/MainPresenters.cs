﻿using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using System.Drawing;
using System;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Challenge;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter
    {
        public override void Run()
        {
            ReadAllSettings();
            CheckAllSettingsOnNull();
            if (!_nullSettingsContainer.IsEmpty())
            {
                ShowMessage(MessageType.SettingsFilesParseProblem);
            }
            else
            {
                Controller.Run<GameInformationPresenter.GameInformationPresenter,
                               GameInformation>(_gameInformation);
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
                ICamerasContainer>(_camerasProvider.CamerasContainer);
        }

        /// <summary>
        /// Adds new frame into tempFpses collection
        /// which in fact contains past and future frames
        /// </summary>
        public void AddNewFrame()
        {
            string cameraName = View.CurrentFrameCameraName;
            Bitmap currentFrame = View.CurrentFrame;
            IFps tempFPS = _fpsContainer.GetFpsByKey(cameraName);
            tempFPS.AddFrame(currentFrame);
        }

        /// <summary>
        /// Shows the form which allows to change player panel settings
        /// after that it redraws player panel with new settings
        /// </summary>
        public void ChangePlayerPanelSettings()
        {
            var oldPlayerPanelSettings =
                (PlayerPanelSettings)_settingsContext.PlayerPanelSetting.Clone();
            Controller.Run<PlayerPanelSettingsPresenter.PlayerPanelSettingsPresenter,
                           PlayerPanelSettings>(_settingsContext.PlayerPanelSetting);
            // we want redraw players only if there were any changes
            // in player panel settings
            if (!_settingsContext.PlayerPanelSetting.Equals(oldPlayerPanelSettings))
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
                            ChallengeSettings>(_settingsContext.ChallengeSetting);
        }

        /// <summary>
        /// Shows the form which allows to change ftp connection settings
        /// </summary>
        public void ChangeFtpSettings()
        {
            Controller.Run<FtpSettingsPresenter.FtpSettingsPresenter,
                            FtpSettings>(_settingsContext.FtpSetting);
        }

        /// <summary>
        /// Shows the form which allows to change rewind settings
        /// </summary>
        public void ChangeRewindSettings()
        {
            Controller.Run<RewindSettingsPresenter.RewindSettingsPresenter,
                            RewindSettings>(_settingsContext.RewindSetting);
        }

        /// <summary>
        /// Starts up general stream process from each connected camera
        /// </summary>
        public void StartStream()
        {
            if (!_camerasProvider.CamerasContainer.IsEmpty()) // DONT FORGET BACK "!" !!!!!!!
            {
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
        /// Changes name of camera
        /// </summary>
        /// <param name="cameraFullName"></param>
        /// <param name="newCameraName"></param>
        public void CameraNameChanged(string cameraFullName, string newCameraName)
        {
            _camerasProvider.CamerasContainer.SetCameraName(cameraFullName, newCameraName);
        }

        /// <summary>
        /// Writes challenge videos into file system
        /// </summary>
        public void CreateChallange()
        {
            var challengeTime = GetChallengeTime();
            _challenge.Initialize(_gameInformation.DirectoryName, challengeTime);
            SetChallengeDirectoryPath();
            CreateDirectoryForChallenge();
            ChangeActivityOfEventForPastFrames(false);
            ChangeActivityOfEventForFutureFrames(true);
            SetUIAsChallengeRecordingOn(_settingsContext.ChallengeSetting.NumberOfFutureFPS);
            AddMarkerOnTimeAxis(_challenge.PathToChallengeDirectory);
        }

        /// <summary>
        /// Opens the folder for current game in file system
        /// </summary>
        public void OpenGameFolder()
        {
            _fileService.OpenFileOrFolder(_gameInformation.DirectoryName);
        }

        /// <summary>
        /// Opens Challenge Player form for choosed challenge
        /// </summary>
        public void OpenChallengePlayer(string pathToChallenge)
        {
            // remove these 2 strings
            string stringForTest = @"Team1_vs_Team2(21.10.2016)\00_00_10\";
            pathToChallenge = stringForTest;
            //
            Controller.Run<ChallengePlayerPresenter.ChallengePlayerPresenter,
               Tuple<string, RewindSettings>>
               (Tuple.Create(pathToChallenge, _settingsContext.RewindSetting)); // pathToChallenge instead of stringForTest
        }

        /// <summary>
        /// Opens Challenge Player form for last challenge
        /// </summary>
        public void OpenChallengePlayerForLastChallenge()
        {
            if(_challenge != null)
            {
                OpenChallengePlayer(_challenge.PathToChallengeDirectory);
            }
            else
            {
                ShowMessage(MessageType.HaveNotRecordedAnyChallengeYet);
            }
        }

        public void OpenBroadcastForm(string cameraFullName)
        {
            ICamera cameraForBroadcasting = _camerasProvider.CamerasContainer.GetCameraByKey(cameraFullName);
            Controller.Run<BroadcastPresenter.BroadcastPresenter,
                Tuple<object, BroadcastPresenter.BroadcastType>>(
                Tuple.Create((object)cameraForBroadcasting,
                BroadcastPresenter.BroadcastType.Stream));
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
