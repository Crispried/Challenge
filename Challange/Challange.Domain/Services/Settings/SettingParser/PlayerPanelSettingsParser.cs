using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class PlayerPanelSettingsParser : ISettingsParser<PlayerPanelSettings>
    {
        public bool SaveSettings(PlayerPanelSettings settings,
                                string settingsFilePath)
        {
            if (FileWorker.SerializeXml(settings, settingsFilePath))
            {
                return true;
            }
            return false;
        }

        public PlayerPanelSettings GetSettings(string settingsFilePath)
        {
            PlayerPanelSettings settings = FileWorker.
                        DeserializeXml<PlayerPanelSettings>(settingsFilePath);
            return settings;
        }
    }
}
