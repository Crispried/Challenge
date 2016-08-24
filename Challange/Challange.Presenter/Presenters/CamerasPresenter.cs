using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Presenter.Presenters
{
    public class CamerasPresenter :
                    BasePresenter<ICamerasView, List<Device>>
    {
        private List<Device> connectedCameras;

        public CamerasPresenter(
                    IApplicationController controller,
                    ICamerasView camerasView) :
                    base(controller, camerasView)
        {
        }

        public override void Run(List<Device> argument)
        {
            connectedCameras = argument;
            FillCamerasListView();
            View.Show();
        }

        private void FillCamerasListView()
        {
            List<string> camerasFullNames = new List<string>();
            foreach (var connectedCamera in connectedCameras)
            {
                camerasFullNames.Add(connectedCamera.FullName);
            }
            View.FillCamerasListView(camerasFullNames);
        }
    }
}
