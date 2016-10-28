
namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class PlayerPanelSettings : Setting
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
    }
}
