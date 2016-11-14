using Challange.Domain.Entities;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.FtpSettingsPresenter
{
    public partial class FtpSettingsPresenter : BasePresenter<IFtpSettingsView, FtpSettings>
    {
        private FtpSettings ftpSettings;
        private IMessageParser messageParser;
        private ISettingsContext settingsContext;

        public FtpSettingsPresenter(
                IApplicationController controller,
                IFtpSettingsView ftpView,
                IMessageParser messageParser,
                ISettingsContext settingsContext) :
                base(controller, ftpView)
        {
            this.messageParser = messageParser;
            this.settingsContext = settingsContext;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangeFtpSettings += ChangeFtpSettings;
            View.TestFtpConnection += TestFtpConnection;
        }
    }
}
