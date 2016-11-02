using System;
using System.Linq.Expressions;
using Autofac;

namespace Challange.Forms
{
    public class AutofacAdapter : Presenter.Base.IContainer
    {
        private readonly ContainerBuilder builder = new ContainerBuilder();

        private ILifetimeScope scope;

        private Autofac.IContainer container;

        public void Build()
        {
            container = builder.Build();
            scope = container.BeginLifetimeScope();
        }

        public void Register<TService>()
        {
            builder.RegisterType<TService>();
        }

        public void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory)
        {
            builder.Register(serviceFactory => factory);
        }

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            builder.RegisterType<TImplementation>().As<TService>();
        }

        public void RegisterInstance<T>(T instance) where T : class
        {
            builder.RegisterInstance(instance);
        }

        public TService Resolve<TService>()
        {
            var service = scope.Resolve<TService>();             
            return service;
        }
    }
}
