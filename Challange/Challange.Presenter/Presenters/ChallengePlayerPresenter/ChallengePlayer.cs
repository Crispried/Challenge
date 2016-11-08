using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Servuces.Video.Concrete;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter : BasePresenter<IChallengePlayerView, Tuple<string, RewindSettings>>
    {
        private RewindSettings rewindSettings;

        private string pathToChallenge;

        private Dictionary<string, Bitmap> initialData;

        private int numberOfVideos;

        private ChallengeReader challengeReader;

        private MessageParser messageParser;

        public ChallengePlayerPresenter(IApplicationController controller,
                     IChallengePlayerView mainView) : 
                             base(controller, mainView)
        {
            messageParser = new MessageParser();
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.OpenBroadcastForm += OpenBroadcastForm; 
        }
    }
}
