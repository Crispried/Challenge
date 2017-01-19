using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.Presenter.Presenters.CamerasPresenter
{
    public partial class CamerasPresenter
    {
        public override void Run(ICamerasContainer argument)
        {
            connectedCameras = argument;
            if(connectedCameras.IsEmpty())
            {
                ShowNoConnectedCamerasLabel();
            }
            else
            {
                FillCamerasListView();
            }
            View.Show();
        }
    }
}
