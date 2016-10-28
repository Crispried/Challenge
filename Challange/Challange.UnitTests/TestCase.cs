using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Entities;

namespace Challange.UnitTests
{
    class TestCase
    {
        private FileService fileService;

        public TestCase()
        {
            fileService = new FileService();
        }

        protected void DeleteFile(string path)
        {
            fileService.DeleteFile(path);
        }

        protected bool FileExists(string path)
        {
            return fileService.FileExists(path);
        }


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
