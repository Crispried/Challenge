using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Presenter.Presenters.RewindSettingsPresenter
{
    public partial class RewindSettingsPresenter
    {
        private void SetRewindSettings(RewindSettings rewindSettings)
        {
            View.SetRewindSettings(rewindSettings);
        }

        private void SaveSettings(RewindSettings newSettings)
        {
            var rewindSettingsParser =
                new SettingsService<RewindSettings>(
                new RewindSettingsParser(new FileWorker()));
            rewindSettingsParser.SaveSetting(newSettings);
        }
    }
}
