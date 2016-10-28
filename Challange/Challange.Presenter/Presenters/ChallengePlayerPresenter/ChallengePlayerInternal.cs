using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using AForge.Video.FFMPEG;
using System.Threading.Tasks;
using Challange.Domain.Infrastructure;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter
    {
        #region settings
        /// <summary>
        /// read rewind settings from xml file
        /// this action occures only when we run our form
        /// </summary>
        /// <returns></returns>
        private RewindSettings GetRewindSettings()
        {
            var rewindSettingService =
                 new SettingsService<RewindSettings>(
                                new RewindSettingsParser(new FileWorker()));
            return rewindSettingService.
                        GetSetting();
        }
        #endregion

        private void ShowMessage(MessageType type)
        {
            ChallengeMessage message = messageParser.GetMessage(type);
            View.ShowMessage(message);
        }

        private void DrawPlayers(int numberOfVideos)
        {
            View.DrawPlayers(numberOfVideos);
        }

        private int GetNumberOfVideos(string pathToChallenge)
        {
            DirectoryInfo di = new DirectoryInfo(pathToChallenge);
            return di.GetFiles("*.mp4", SearchOption.TopDirectoryOnly).Length;
        }

        private void InitializePlayers(Dictionary<string, Bitmap> initialData)
        {
            View.InitializePlayers(initialData);
        }

        private Dictionary<string, Bitmap> GetInitialData()
        {
            return challengeReader.GetInitialData();
        }
    }
}
