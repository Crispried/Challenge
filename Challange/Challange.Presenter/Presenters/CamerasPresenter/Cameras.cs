using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.CamerasPresenter
{
    public partial class CamerasPresenter :
                    BasePresenter<ICamerasView, ICamerasContainer>
    {
        private ICamerasContainer connectedCameras;

        public CamerasPresenter(
                    IApplicationController controller,
                    ICamerasView camerasView) :
                    base(controller, camerasView)
        {
        }
    }
}
