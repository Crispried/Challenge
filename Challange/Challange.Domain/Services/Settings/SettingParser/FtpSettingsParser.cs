using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                FtpSettings settings = fileWorker.
                    DeserializeXml<FtpSettings>(SettingsFilePath);
                return settings;
            }
            catch
            {
                return null;
            }
        }
    }
}
