using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings
{
    public class SettingsService<T> where T : Setting
    {
        private const string settingsFilePath = "settings.xml";
        private ISettingsParser<T> settingsParser;

        public SettingsService(ISettingsParser<T> settingParser)
        {
            this.settingsParser = settingParser;
        }

        public void SaveSetting(T setting)
        {
            settingsParser.SaveSettings(setting, settingsFilePath);
        }

        public T GetSetting()
        {
            return settingsParser.GetSettings(settingsFilePath);
        }
    }
}
