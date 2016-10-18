using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter : BasePresenter<IBroadcastView>
    {
        public BroadcastPresenter(
                IApplicationController controller,
                IBroadcastView broadcastView) :
                base(controller, broadcastView)
        {
        }
    }
}
