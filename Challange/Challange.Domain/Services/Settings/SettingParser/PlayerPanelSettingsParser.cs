using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.FileSystem.Abstract;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class PlayerPanelSettingsParser : ISettingsParser<PlayerPanelSettings>
    {
        private string settingsFilePath = @"Settings\player_panel.xml";
        private IFileWorker fileWorker;

        public PlayerPanelSettingsParser(IFileWorker fileWorker)
        {
            this.fileWorker = fileWorker;
        }

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

        public bool SaveSettings(PlayerPanelSettings setting)
        {
            return fileWorker.SerializeXml(setting, settingsFilePath) ? true : false;
        }

        public PlayerPanelSettings GetSettings()
        {
            PlayerPanelSettings settings = fileWorker.
                    DeserializeXml<PlayerPanelSettings>(settingsFilePath);
            return settings;
        }
    }
}
