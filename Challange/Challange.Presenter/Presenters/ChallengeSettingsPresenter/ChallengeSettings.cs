using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengeSettingsPresenter
{
    public partial class ChallengeSettingsPresenter :
                    BasePresenter<IChallengeSettingsView, ChallengeSettings>
    {
        private ChallengeSettings challengeSettings;
        private IMessageParser messageParser;
        private ISettingsContext settingsContext;

        public ChallengeSettingsPresenter(
                IApplicationController controller,
                IChallengeSettingsView challengeSettingsView,
                IMessageParser messageParser,
                ISettingsContext settingsContext) :
                base(controller, challengeSettingsView)
        {
            this.messageParser = messageParser;
            this.settingsContext = settingsContext;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangeChallengeSettings += ChangeChallengeSettings;
        }
    }
}
