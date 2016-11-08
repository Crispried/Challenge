using Challange.Domain.Entities;
using Challange.Domain.Services.Event;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter : BasePresenter<IBroadcastView, ICamera>
    {
        private ICamera camera;
        private IEventSubscriber eventSubscriber;
        public BroadcastPresenter(
                IApplicationController controller,
                IBroadcastView broadcastView,
                IEventSubscriber eventSubscriber) :
                base(controller, broadcastView)
        {
            this.eventSubscriber = eventSubscriber;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.BroadcastShowCallback += BroadcastShowCallback;
        }
    }
}
