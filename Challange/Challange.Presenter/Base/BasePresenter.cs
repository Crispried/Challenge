
using System.Diagnostics.CodeAnalysis;

namespace Challange.Presenter.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class BasePresenter<TView> : IPresenter
            where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller,
                                TView view)
        {
            Controller = controller;
            View = view;
        }

        public virtual void Run()
        {
            View.Show();
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class BasePresenter<TView, TArgument> : IPresenter<TArgument>
        where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller,
                                TView view)
        {
            Controller = controller;
            View = view;
        }

        public abstract void Run(TArgument argument);
    }
}
