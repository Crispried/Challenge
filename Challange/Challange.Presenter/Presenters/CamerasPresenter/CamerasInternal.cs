using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.CamerasPresenter
{
    public partial class CamerasPresenter
    {
        /// <summary>
        /// Fills cameras list with all connected cameras names
        /// </summary>
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
