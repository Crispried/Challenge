using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Services.Settings.SettingTypes;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Entities;
using System.IO;
using Challange.Domain.Services.FileSystem;
using System.Diagnostics.CodeAnalysis;

namespace Challange.UnitTests
{
    class TestCase
    {
        protected const string settingsPath = @"Settings\rewind.xml";
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

        protected List<Device> InitializeCamerasInfo()
        {
            List<Device> camerasInfo = new List<Device>();
            Device item1 = new Device();
            item1.FullName = "FullName1";
            item1.Name = "Name1";
            item1.Index = 1;
            Device item2 = new Device();
            item2.FullName = "FullName2";
            item2.Name = "Name2";
            item2.Index = 1;
            camerasInfo.Add(item1);
            camerasInfo.Add(item2);
            return camerasInfo;
        }
    }
}
