using Challange.Domain.SettingsService.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.SettingsService.SettingParser
{
    public interface ISettingsParser<T> where T : SettingTypes.Settings
    {
        bool SaveSettings(T settings, string settingsFilePath);

        T GetSettings(string settingsFilePath);
    }
}
