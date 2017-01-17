using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class ChallengeSettingsParser : ISettingsParser<ChallengeSettings>
    {
        private IFileWorker fileWorker;

        public ChallengeSettingsParser(IFileWorker fileWorker)
        {
            this.fileWorker = fileWorker;
        }

        private string settingsFilePath = @"Settings\challenge.xml";

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

        public bool SaveSettings(ChallengeSettings settings)
        {
            return fileWorker.SerializeXml(settings, SettingsFilePath) ? true : false;
        }

        public ChallengeSettings GetSettings()
        {
            ChallengeSettings settings = fileWorker.
            DeserializeXml<ChallengeSettings>(SettingsFilePath);
            return settings;
        }
    }
}
