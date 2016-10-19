using Challange.Domain.Entities;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter
    {
        public override void Run(Camera argument)
        {
            camera = argument;
            View.Show();
        }

        public void BroadcastShowCallback()
        {
            StartStream();
        }
    }
}
