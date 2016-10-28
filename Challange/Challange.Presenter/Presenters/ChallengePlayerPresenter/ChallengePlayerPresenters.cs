using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter
    {
        public override void Run(string argument)
        {
            pathToChallenge = argument;
            challengeReader = new ChallengeReader(pathToChallenge);
            rewindSettings = GetRewindSettings();
            if (rewindSettings == null)
            {
                RewindSettingsAreNull = true;
                ShowMessage(MessageType.RewindSettingsFileParseProblem);
            }
            else
            {
                numberOfVideos = GetNumberOfVideos(pathToChallenge);
                DrawPlayers(numberOfVideos);
                initialData = GetInitialData();
                InitializePlayers(initialData);
                View.Show();
            }
        }

        public void OpenRewindSettings()
        {
            Controller.Run<RewindSettingsPresenter.RewindSettingsPresenter,
               RewindSettings>(rewindSettings);
        }

        public void OpenBroadcastForm(string cameraFullName)
        {

        }
    }
}
