
using System;

namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class ChallengeSettings : Setting
    {
        public int NumberOfPastFPS { get; set; }

        public int NumberOfFutureFPS { get; set; }

        public override void SetSettings(Setting newSettings)
        {
            var settings = (ChallengeSettings)newSettings;
            NumberOfPastFPS = settings.NumberOfPastFPS;
            NumberOfFutureFPS = settings.NumberOfFutureFPS;
        }
    }
}
