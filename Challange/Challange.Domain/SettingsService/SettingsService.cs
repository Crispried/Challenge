using Challange.Domain.SettingsService.SettingParser;
using Challange.Domain.SettingsService.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.SettingsService
{
    public class SettingsService<T> where T : Settings
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
