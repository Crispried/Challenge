using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Video.Concrete;
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
        ManualResetEvent shutdownEvent = new ManualResetEvent(false);

        private ManualResetEvent pauseEvent = new ManualResetEvent(true);

        public override void Run(Tuple<string, RewindSettings> argument)
        {
            pathToChallenge = argument.Item1;
            rewindSettings = argument.Item2;
            challengeReader = new ChallengeReader(pathToChallenge);
            challengeReader.ReadAllChallenges();
            numberOfVideos = GetNumberOfVideos(pathToChallenge);
            indexesOfFramesToPlay = new List<int>(numberOfVideos);
            for (int i = 0; i < indexesOfFramesToPlay.Capacity; i++)
            {
                indexesOfFramesToPlay.Add(0);
            }
            initialData = GetInitialData();
            DrawPlayers(numberOfVideos, initialData);
            threads = new List<Thread>(challengeReader.Challenges.Count);
            CreateThreads();
            View.Show();
        }

        public void OpenBroadcastForm(string cameraFullName)
        {

        }

        private void CreateThreads()
        {
            for (int i = 0; i < threads.Capacity; i++)
            {
                int capture = i;
                Thread thread = new Thread(() => PlayVideo(challengeReader.Challenges[capture]));
                threads.Add(thread);
            }
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Start();
            }
        }

        public void StartAllPlayers()
        {
            pauseEvent.Set();
        }

        public void StopAllPlayers()
        {
            pauseEvent.Reset();
        }

        public void RewindBackward()
        {
            for (int i = 0; i < challengeReader.Challenges.Count; i++)
            {
                if(challengeReader.Challenges[i].FrameIndex - rewindSettings.Backward < 0)
                {
                    challengeReader.Challenges[i].FrameIndex = 0;
                }
                else
                {
                    challengeReader.Challenges[i].FrameIndex -= rewindSettings.Backward;
                }          
                View.DrawNewFrame(challengeReader.Challenges[i].Frames[challengeReader.Challenges[i].FrameIndex], challengeReader.Challenges[i].Name);
            }
        }

        public void RewindForward()
        {
            for (int i = 0; i < challengeReader.Challenges.Count; i++)
            {
                if (challengeReader.Challenges[i].FrameIndex + rewindSettings.Forward >= challengeReader.Challenges[i].Frames.Count)
                {
                    challengeReader.Challenges[i].FrameIndex = challengeReader.Challenges[i].Frames.Count - 1;
                }
                else
                {
                    challengeReader.Challenges[i].FrameIndex += rewindSettings.Forward;
                }
                View.DrawNewFrame(challengeReader.Challenges[i].Frames[challengeReader.Challenges[i].FrameIndex], challengeReader.Challenges[i].Name);
            }
        }

        public void OnFormClosing()
        {
            // Signal the shutdown event
            shutdownEvent.Set();

            // Make sure to resume any paused threads
            pauseEvent.Set();
            for (int i = 0; i < threads.Count; i++)
            {
                // Wait for the thread to exit
                threads[i].Join();
            }
        }
    }
}
