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
        public RewindSettingsPresenter(IApplicationController controller,
                              IRewindSettingsView view) :
                              base(controller, view)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangeRewindSettings += ChangeRewindSettings;
        }
    }
}
