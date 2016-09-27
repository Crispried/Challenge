using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Presenter.Presenters.CamerasPresenter
{
    public partial class CamerasPresenter :
                    BasePresenter<ICamerasView, List<string>>
    {
        private List<string> connectedCameras;

        public CamerasPresenter(
                    IApplicationController controller,
                    ICamerasView camerasView) :
                    base(controller, camerasView)
        {
        }
    }
}
