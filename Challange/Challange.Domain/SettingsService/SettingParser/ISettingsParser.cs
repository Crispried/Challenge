using Challange.Domain.SettingsService.SettingTypes;

namespace Challange.Domain.SettingsService.SettingParser
{
    public interface ISettingsParser<T> where T : Settings
    {
        bool SaveSettings(T settings, string settingsFilePath);

        T GetSettings(string settingsFilePath);
    }
}
