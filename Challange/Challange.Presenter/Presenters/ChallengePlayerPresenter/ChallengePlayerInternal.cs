using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Video.Concrete;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter
    { 

        private void ShowMessage(MessageType type)
        {
            ChallengeMessage message = messageParser.GetMessage(type);
            View.ShowMessage(message);
        }

        private void DrawPlayers(int numberOfVideos, Dictionary<string, Bitmap> initialData)
        {
            View.DrawPlayers(numberOfVideos, initialData);
        }

        private int GetNumberOfVideos(string pathToChallenge)
        {
            DirectoryInfo di = new DirectoryInfo(pathToChallenge);
            return di.GetFiles("*.mp4", SearchOption.TopDirectoryOnly).Length;
        }

        private Dictionary<string, Bitmap> GetInitialData()
        {
            return challengeReader.GetInitialData();
        }

        private void PlayVideo(Video video)
        {
            while (true)
            {
                pauseEvent.WaitOne(Timeout.Infinite);

                if (shutdownEvent.WaitOne(0))
                    break;

                if(video.NotEnd())
                {
                    View.DrawNewFrame(video.Frames[video.FrameIndex], video.Name);
                    video.FrameIndex++;
                }
                Thread.Sleep(View.PlaybackSpeed);               
            }
        }
    }
}
