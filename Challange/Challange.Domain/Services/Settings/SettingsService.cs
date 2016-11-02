using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using System;

namespace Challange.Domain.Services.Settings
{
    public interface ISettingsService<T> where T : Setting
    {
        void SaveSetting(T setting);

        T GetSetting();
    }

    public class SettingsService<T> : ISettingsService<T> where T : Setting
    {
        private ISettingsParser<T> settingsParser;

        public SettingsService(ISettingsParser<T> settingsParser)
        {
            this.settingsParser = settingsParser;
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
