
namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class PlayerPanelSettings : Setting
    {
        public int NumberOfPlayers { get; set; }        

        public bool AutosizeMode { get; set; }

        public int PlayerWidth { get; set; }

        public int PlayerHeight { get; set; }
    }
}
