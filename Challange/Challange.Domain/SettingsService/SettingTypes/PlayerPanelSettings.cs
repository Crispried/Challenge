using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.SettingsService.SettingTypes
{
    public class PlayerPanelSettings : Settings
    {
        public int NumberOfPlayers { get; set; }        

        public bool AutosizeMode { get; set; }

        public int PlayerWidth { get; set; }

        public int PlayerHeight { get; set; }
    }
}
