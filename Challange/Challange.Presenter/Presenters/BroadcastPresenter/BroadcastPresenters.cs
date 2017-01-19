using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter
    {
        public override void Run(ICamera argument)
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
