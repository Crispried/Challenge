using Challange.Domain.Entities;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;

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
