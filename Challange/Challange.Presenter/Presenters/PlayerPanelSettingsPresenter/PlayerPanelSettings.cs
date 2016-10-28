using Challange.Domain.Infrastructure;
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

        private IFileWorker fileWorker;

        private SettingsService<PlayerPanelSettings> settingsService;

        public PlayerPanelSettingsPresenter(
                IApplicationController controller,
                IPlayerPanelSettingsView playerPanelSettingsView,
                IMessageParser messageParser,
                IFileWorker fileWorker) :
                base(controller, playerPanelSettingsView)
        {
            this.messageParser = messageParser;
            this.fileWorker = fileWorker;
            settingsService = new SettingsService<PlayerPanelSettings>(
                new PlayerPanelSettingsParser(fileWorker));
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangePlayerPanelSettings += ChangePlayerPanelSettings;
        }
    }
}
