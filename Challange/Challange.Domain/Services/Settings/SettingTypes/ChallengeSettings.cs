
namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class ChallengeSettings : Setting, IChallengeSettings
    {
        public int NumberOfPastFPS { get; set; }

        public int NumberOfFutureFPS { get; set; }
    }
}
