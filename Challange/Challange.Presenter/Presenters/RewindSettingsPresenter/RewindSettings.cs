using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.RewindSettingsPresenter
{
    public partial class RewindSettingsPresenter : BasePresenter<IRewindSettingsView, RewindSettings>
    {
        private RewindSettings rewindSettings;

        private IMessageParser messageParser;

        private ISettingsContext settingsContext;
        public RewindSettingsPresenter(IApplicationController controller,
                              IRewindSettingsView view,
                              IMessageParser messageParser,
                              ISettingsContext settingsContext) :
                              base(controller, view)
        {
            this.messageParser = messageParser;
            this.settingsContext = settingsContext;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangeRewindSettings += ChangeRewindSettings;
        }
    }
}
