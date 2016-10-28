using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class RewindSettings : Setting
    {
        public int Forward { get; set; }

        public int Backward { get; set; }

        public override void SetSettings(Setting newSettings)
        {
            var settings = (RewindSettings)newSettings;
            Forward = settings.Forward;
            Backward = settings.Backward;
        }
    }
}
