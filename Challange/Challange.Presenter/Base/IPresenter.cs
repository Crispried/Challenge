using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Base
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<in TArgument>
    {
        void Run(TArgument argument);
    }
}
