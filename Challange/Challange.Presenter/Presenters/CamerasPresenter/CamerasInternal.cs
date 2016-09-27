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
            View.FillCamerasListView(connectedCameras);
        }
    }
}
