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
        private const string settingsFilePath = @"Settings\challenge.xml";

        public string SettingsFilePath
        {
            get
            {
                return settingsFilePath;
            }
        }

        public bool SaveSettings(ChallengeSettings settings)
        {
            if (FileWorker.SerializeXml(settings, settingsFilePath))
            {
                return true;
            }
            return false;
        }

        public ChallengeSettings GetSettings()
        {
            try
            {
                ChallengeSettings settings = FileWorker.
                DeserializeXml<ChallengeSettings>(settingsFilePath);
                return settings;
            }
            catch
            {
                return null;
            }
        }
    }
}
