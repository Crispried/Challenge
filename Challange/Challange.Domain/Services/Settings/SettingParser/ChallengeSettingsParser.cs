using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public class ChallengeSettingsParser : ISettingsParser<ChallengeSettings>
    {
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
            return FileWorker.SerializeXml(settings, SettingsFilePath) ? true : false;
        }

        public ChallengeSettings GetSettings()
        {
            try
            {
                ChallengeSettings settings = FileWorker.
                DeserializeXml<ChallengeSettings>(SettingsFilePath);
                return settings;
            }
            catch
            {
                return null;
            }
        }
    }
}
