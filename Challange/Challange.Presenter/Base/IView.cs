using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Base
{
    public interface IView
    {
        void Show();

        void Close();

        void Refresh();
    }
}
