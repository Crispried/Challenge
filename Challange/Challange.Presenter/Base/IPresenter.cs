
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
