using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters
{
    public class CamerasPresenter :
                    BasePresenter<ICamerasView, List<string>>
    {
        private List<string> connectedCameras;

        public CamerasPresenter(
                    IApplicationController controller,
                    ICamerasView camerasView) :
                    base(controller, camerasView)
        {
        }

        public override void Run(List<string> argument)
        {
            connectedCameras = argument;
            View.Show();
            View.FillCamerasListView(connectedCameras);
        }
    }
}
