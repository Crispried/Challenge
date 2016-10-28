using Challange.Domain.Entities;

namespace Challange.Presenter.Presenters.CamerasPresenter
{
    public partial class CamerasPresenter
    {
        public override void Run(CamerasContainer argument)
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
