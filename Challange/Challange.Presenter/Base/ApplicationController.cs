
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Presenter.Views.Layouts;
using System.Diagnostics.CodeAnalysis;

namespace Challange.Presenter.Base
{
    [ExcludeFromCodeCoverage]
    public class ApplicationController : IApplicationController
    {
        private IContainer container;

        public ApplicationController(IContainer container)
        {
            this.container = container;
            this.container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterInstance<TArgument>(TArgument instance) where TArgument : class
        {
            container.RegisterInstance(instance);
            return this;
        }

        public IApplicationController RegisterService<TService, TImplementation>()
                where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
            return this;
        }

        public IApplicationController RegisterService<TService>()
        {
            container.Register<TService>();
            return this;
        }

        public IApplicationController RegisterServiceAsSingleton<TService, TImplementation>()
        where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
            return this;
        }

        public IApplicationController RegisterLayoutToForm<TLayout, TImplementation>()
                where TLayout : ILayout
                where TImplementation : class, TLayout
        {
            container.Register<TLayout, TImplementation>();
            return this;
        }

        public IApplicationController RegisterLayoutToImplementation<TLayout, TImplementation>()
                where TLayout : ILayout
                where TImplementation : class, TLayout
        {
            container.Register<TLayout, TImplementation>();
            return this;
        }

        public IApplicationController RegisterView<TView, TImplementation>()
                where TView : IView
                where TImplementation : class, TView
        {
            container.Register<TView, TImplementation>();
            return this;
        }

        public void Run<TPresenter>()
                where TPresenter : class, IPresenter
        {
            var presenter = container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgument>(TArgument argument)
                where TPresenter : class, IPresenter<TArgument>
        {
            var presenter = container.Resolve<TPresenter>();
            presenter.Run(argument);
        }
    }
}
