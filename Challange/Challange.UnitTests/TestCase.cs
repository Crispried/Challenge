using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests
{
    class TestCase
    {
        protected void DeleteFile(string path)
        {
            FileService.DeleteFile(path);
        }

        protected bool FileExists(string path)
        {
            return FileService.FileExists(path);
        }


        protected PlayerPanelSettings InitializePlayerPanelSettings()
        {
            return new PlayerPanelSettings()
            {
                AutosizeMode = false,
                NumberOfPlayers = 5,
                PlayerHeight = 480,
                PlayerWidth = 640
            };
        }

        protected ChallengeSettings InitializeChallengeSettings()
        {
            return new ChallengeSettings()
            {
                NumberOfFutureFPS = 10,
                NumberOfPastFPS = 15
            };
        }
    }
}
