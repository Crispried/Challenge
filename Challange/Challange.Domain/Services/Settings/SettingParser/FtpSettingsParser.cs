using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class FtpSettingsParser : ISettingsParser<FtpSettings>
    {
        private string settingsFilePath = @"Settings\ftp.xml";
        private IFileWorker fileWorker;

        public FtpSettingsParser(IFileWorker fileWorker)
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

        public bool SaveSettings(FtpSettings settings)
        {
            return fileWorker.SerializeXml(settings, SettingsFilePath) ? true : false;
        }

        public FtpSettings GetSettings()
        {
            FtpSettings settings = fileWorker.
                DeserializeXml<FtpSettings>(SettingsFilePath);
            return settings;
        }
    }
}
