using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Base
{
    public abstract class BasePresenter<TView> : IPresenter
            where TView : IView
    {
        protected IView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, IView view)
        {
            View = view;
            Controller = controller;
        }

        public void Run()
        {
            View.Show();
        }
    }

    public abstract class BasePresenter<TView, TArgument> : IPresenter<TArgument>
        where TView : IView
    {
        protected IView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, IView view)
        {
            View = view;
            Controller = controller;
        }

        public abstract void Run(TArgument argument);
    }
}
