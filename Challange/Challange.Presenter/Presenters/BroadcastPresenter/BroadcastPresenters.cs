using Challange.Domain.Services.StreamProcess.Abstract;
using System;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter
    {
        public override void Run(Tuple<object, BroadcastType> argument)
        {
            objectToBroadcast = argument.Item1;
            broadcastType = argument.Item2;
            View.Show();
        }

        public void BroadcastShowCallback()
        {
            StartBroadcasting();
        }
    }
}
