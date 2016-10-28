using Challange.Domain.Entities;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.CamerasPresenter
{
    public partial class CamerasPresenter :
                    BasePresenter<ICamerasView, CamerasContainer>
    {
        private CamerasContainer connectedCameras;

        public CamerasPresenter(
                    IApplicationController controller,
                    ICamerasView camerasView) :
                    base(controller, camerasView)
        {
        }
    }
}
