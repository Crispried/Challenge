using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Base
{
    public class ApplicationController : IApplicationController
    {
        private IContainer container;

        public ApplicationController(IContainer container)
        {
            this.container = container;
            this.container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterInstance<TArgument>(TArgument instance)
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
            if (!container.IsRegistered<TPresenter>())
            {
                container.Register<TPresenter>();
            }
            var presenter = container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgument>(TArgument argument)
                where TPresenter : class, IPresenter<TArgument>
        {
            if (!container.IsRegistered<TPresenter>())
            {
                container.Register<TPresenter>();
            }
            var presenter = container.Resolve<TPresenter>();
            presenter.Run(argument);
        }
    }
}
