using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter : BasePresenter<IChallengePlayerView, string>
    {
        public ChallengePlayerPresenter(IApplicationController controller,
                     IChallengePlayerView mainView) : 
                             base(controller, mainView)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.OpenRewindSettings += OpenRewindSettings;
            View.OpenBroadcastForm += OpenBroadcastForm; 
        }
    }
}
