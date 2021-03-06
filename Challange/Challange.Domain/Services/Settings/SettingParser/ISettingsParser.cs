﻿using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Services.Settings.SettingParser
{
    public interface ISettingsParser<T> where T : Setting
    {
        bool SaveSettings(T setting);

        T GetSettings();

        string SettingsFilePath { get; set; }
    }
}
