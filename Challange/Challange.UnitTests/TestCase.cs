using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Challenge;

namespace Challange.UnitTests
{
    class TestCase
    {
        protected PlayerPanelSettings InitializePlayerPanelSettings()
        {
            return new PlayerPanelSettings()
            {
                AutosizeMode = false,
                PlayerHeight = 480,
                PlayerWidth = 640
            };
        }

        protected ChallengeSettings InitializeChallengeSettings()
        {
            return new ChallengeSettings()
            {
                NumberOfPastFPS = 15,
                NumberOfFutureFPS = 10
            };
        }

        protected FtpSettings InitializeFtpSettings()
        {
            return new FtpSettings()
            {
                FtpAddress = "ftp://ftp.wsiz.rzeszow.pl",
                UserName = "w46999",
                Password = "35162067160"
            };
        }

        protected RewindSettings InitializeRewindSettings()
        {
            return new RewindSettings()
            {
                Backward = 10,
                Forward = 15
            };
        }

        protected GameInformation InitializeGameInformation()
        {
            return new GameInformation()
            {
                Country = "USA",
                City = "New York",
                FirstTeam = "Best squad",
                SecondTeam = "Takeover",
                Date = "26.10.2016",
                GameStart = "17:45",
                Part = "2"
            };
        }
    }
}
