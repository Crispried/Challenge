using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class PlayerPanelSettingsParser : ISettingsParser<PlayerPanelSettings>
    {
        private const string settingsFilePath = @"Settings\player_panel.xml";

        public string SettingsFilePath
        {
            get
            {
                return settingsFilePath;
            }
        }

        public bool SaveSettings(PlayerPanelSettings settings)
        {
            if (FileWorker.SerializeXml(settings, settingsFilePath))
            {
                return true;
            }
            return false;
        }

        public PlayerPanelSettings GetSettings()
        {
            PlayerPanelSettings settings = FileWorker.
                        DeserializeXml<PlayerPanelSettings>(settingsFilePath);
            return settings;
        }
    }
}
