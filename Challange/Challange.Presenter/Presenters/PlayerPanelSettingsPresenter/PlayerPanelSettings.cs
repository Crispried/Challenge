using Challange.Domain.Services.Message;
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

        private IMessageParser messageParser;

        private ISettingsContext settingsContext;

        public PlayerPanelSettingsPresenter(
                IApplicationController controller,
                IPlayerPanelSettingsView playerPanelSettingsView,
                IMessageParser messageParser,
                ISettingsContext settingsContext) :
                base(controller, playerPanelSettingsView)
        {
            this.messageParser = messageParser;
            this.settingsContext = settingsContext;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangePlayerPanelSettings += ChangePlayerPanelSettings;
        }
    }
}
