using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.RewindSettingsPresenter
{
    public partial class RewindSettingsPresenter
    {
        public override void Run(RewindSettings argument)
        {
            rewindSettings = argument;
            SetRewindSettings(rewindSettings);
            View.Show();
        }

        public void ChangeRewindSettings(RewindSettings newSettings)
        {
            SaveSettings(newSettings);
            rewindSettings.Backward = newSettings.Backward;
            rewindSettings.Forward = newSettings.Forward;
            RewindSettingsWasSaved = true;
            View.Close();
        }
    }
}
