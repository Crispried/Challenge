using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.PlayerPanelSettingsPresenter
{
    public partial class PlayerPanelSettingsPresenter :
             BasePresenter<IPlayerPanelSettingsView, PlayerPanelSettings>
    {
        private PlayerPanelSettings playerPanelSettings;

        public PlayerPanelSettingsPresenter(
                IApplicationController controller,
                IPlayerPanelSettingsView playerPanelSettingsView) :
                base(controller, playerPanelSettingsView)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangePlayerPanelSettings += () =>
                         ChangePlayerPanelSettings(
                                    View.PlayerPanelSettings);
        }
    }
}
