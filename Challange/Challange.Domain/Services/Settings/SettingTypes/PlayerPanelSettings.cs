
using System;
using System.Xml.Serialization;

namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class PlayerPanelSettings : Setting, ICloneable
    {     
        public bool AutosizeMode { get; set; }

        public int PlayerWidth { get; set; }

        public int PlayerHeight { get; set; }

        public override void SetSettings(Setting newSettings)
        {
            var settings = (PlayerPanelSettings)newSettings;
            AutosizeMode = settings.AutosizeMode;
            PlayerWidth = settings.PlayerWidth;
            PlayerHeight = settings.PlayerHeight;
        }

        public bool Equals(PlayerPanelSettings playerPanelSettings)
        {
            if(playerPanelSettings == null)
            {
                return false;
            }
            return (playerPanelSettings.AutosizeMode == AutosizeMode) &&
                   (playerPanelSettings.PlayerHeight == PlayerHeight) &&
                   (playerPanelSettings.PlayerWidth == PlayerWidth);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
