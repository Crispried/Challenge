using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Servuces.Video.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    [ExcludeFromCodeCoverage]
    public partial class ChallengePlayerPresenter
    {
        public override void Run(Tuple<string, RewindSettings> argument)
        {
            pathToChallenge = argument.Item1;
            rewindSettings = argument.Item2;
            challengeReader = new ChallengeReader(pathToChallenge);
            challengeReader.ReadAllChallenges();
            numberOfVideos = GetNumberOfVideos(pathToChallenge);
            DrawPlayers(numberOfVideos);
            initialData = GetInitialData();
            InitializePlayers(initialData);
            View.Show();
        }

        public void OpenBroadcastForm(string cameraFullName)
        {

        }

        public void StartAllPlayers()
        {
            threads = new List<Thread>(challengeReader.Challenges.Count);
            for (int i = 0; i < threads.Capacity; i++)
            {
                int capture = i;
                threads.Add(new Thread(() => PlayVideo(challengeReader.Challenges[capture])));
            }

            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Start();
            }
        }

        public void StopAllPlayers()
        {
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Abort();
            }
        }
    }
}
