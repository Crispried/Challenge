using Challange.Domain.Infrastructure;
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
            if (FileWorker.SerializeXml(settings, settingsFilePath))
            {
                return true;
            }
            return false;
        }

        public RewindSettings GetSettings()
        {
            try
            {
                RewindSettings settings = FileWorker.
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
