using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class RewindSettingsParser : ISettingsParser<RewindSettings>
    {
        private string settingsFilePath = @"Settings\rewind.xml";
        private IFileWorker fileWorker;

        public RewindSettingsParser(IFileWorker fileWorker)
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
            try
            {
                RewindSettings settings = fileWorker.
                        DeserializeXml<RewindSettings>(settingsFilePath);
                return settings;
            }
            catch
            {
                return null;
            }
        }
    }
}
