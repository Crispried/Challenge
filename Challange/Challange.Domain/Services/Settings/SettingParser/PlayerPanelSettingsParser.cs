using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class PlayerPanelSettingsParser : ISettingsParser<PlayerPanelSettings>
    {
        private string settingsFilePath = @"Settings\player_panel.xml";

        public string SettingsFilePath
        {
            get
            {
                return settingsFilePath;
            }
            set
            {
                settingsFilePath = value;
            }
        }

        public bool SaveSettings(PlayerPanelSettings settings)
        {
            return FileWorker.SerializeXml(settings, settingsFilePath) ? true : false;
        }

        public PlayerPanelSettings GetSettings()
        {
            try
            {
                PlayerPanelSettings settings = FileWorker.
                        DeserializeXml<PlayerPanelSettings>(settingsFilePath);
                return settings;
            }
            catch
            {
                return null;
            }
        }
    }
}
