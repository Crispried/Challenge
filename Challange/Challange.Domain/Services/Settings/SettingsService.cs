using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings
{
    public interface ISettingsService<T> where T : Setting
    {
        void SaveSetting(T setting);

        T GetSetting();
    }

    public class SettingsService<T> where T : Setting
    {
        private ISettingsParser<T> settingsParser;

        public SettingsService(ISettingsParser<T> settingParser)
        {
            this.settingsParser = settingParser;
        }

        public void SaveSetting(T setting)
        {
            settingsParser.SaveSettings(setting);
        }

        public T GetSetting()
        {
            return settingsParser.GetSettings();
        }
    }
}
