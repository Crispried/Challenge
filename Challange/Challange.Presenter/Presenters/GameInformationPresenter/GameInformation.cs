using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.GameInformationPresenter
{
    public partial class GameInformationPresenter :
                    BasePresenter<IGameInformationView, GameInformation>
    {
        private GameInformation gameInformation;

        public GameInformationPresenter(
        IApplicationController controller,
        IGameInformationView gameInformationView) :
                base(controller, gameInformationView)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.SetGameInformation += () =>
             PrepareApplication(
                        View.GameInformation);
        }
    }
}
