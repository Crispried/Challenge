using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Views
{
    public interface ICamerasView : IView
    {
        void FillCamerasListView(List<string> camerasNames);

        void ShowNoConnectedCamerasLabel();
    }
}
