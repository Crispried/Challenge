
using Challange.Presenter.Views.Layouts;

namespace Challange.Presenter.Base
{
    public interface IApplicationController
    {
        IApplicationController RegisterView<TView, TImplementation>()
                where TImplementation : class, TView
                where TView : IView;

        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        IApplicationController RegisterService<TService, TImplementation>()
                where TImplementation : class, TService;

        IApplicationController RegisterLayoutToForm<TLayout, TImplementation>()
                where TLayout : ILayout
                where TImplementation : class, TLayout;

        IApplicationController RegisterLayoutToImplementation<TLayout, TImplementation>()
                where TLayout : ILayout
                where TImplementation : class, TLayout;

        void Run<TPresenter>()
                where TPresenter : class, IPresenter;

        void Run<TPresenter, TArgument>(TArgument argument)
                where TPresenter : class, IPresenter<TArgument>;
    }
}
