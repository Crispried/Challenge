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
        public override void Run(Tuple<string, RewindSettings> argument)
        {
            pathToChallenge = argument.Item1;
            rewindSettings = argument.Item2;
            challengeReader = new ChallengeReader(pathToChallenge);
            numberOfVideos = GetNumberOfVideos(pathToChallenge);
            DrawPlayers(numberOfVideos);
            initialData = GetInitialData();
            InitializePlayers(initialData);
            View.Show();
        }

        public void OpenBroadcastForm(string cameraFullName)
        {

        }
    }
}
