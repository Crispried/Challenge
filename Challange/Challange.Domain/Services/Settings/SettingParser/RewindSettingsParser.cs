using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class RewindSettingsParser : ISettingsParser<RewindSettings>
    {
        private string settingsFilePath = @"Settings\rewind.xml";
        private IXmlWorker fileWorker;

        public RewindSettingsParser(IXmlWorker fileWorker)
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

        public bool SaveSettings(RewindSettings settings)
        {
            return fileWorker.SerializeXml(settings, settingsFilePath) ? true : false;
        }

        public RewindSettings GetSettings()
        {
            RewindSettings settings = fileWorker.
                    DeserializeXml<RewindSettings>(settingsFilePath);
            return settings;
        }
    }
}
