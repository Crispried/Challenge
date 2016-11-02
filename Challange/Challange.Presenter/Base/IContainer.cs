using System;
using System.Linq.Expressions;

namespace Challange.Presenter.Base
{
    public interface IContainer
    {
        void Register<TService, TImplementation>() 
                where TImplementation : TService;

        void Register<TService>();

        void RegisterInstance<T>(T instance) where T : class;

        TService Resolve<TService>();

        void Register<TService, TArgument>
                (Expression<Func<TArgument, TService>> factory);
    }
}
