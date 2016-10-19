using Challange.Domain.Entities;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter : BasePresenter<IBroadcastView, Camera>
    {
        private Camera camera;
        public BroadcastPresenter(
                IApplicationController controller,
                IBroadcastView broadcastView) :
                base(controller, broadcastView)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.BroadcastShowCallback += BroadcastShowCallback;
        }
    }
}
