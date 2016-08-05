using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Infrastructure
{
    public class ChallengeStructurer
    {
        private string desktopPath;

        private string currentTimestamp;

        private string videoExtension;

        public ChallengeStructurer()
        {
            desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            currentTimestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            videoExtension = ".avi";
        }

        public List<string> CreateDirectoryStructureForChallenge(List<Camera> cameras, string firstTeamName, string secondTeamName)
        {
            List<string> videoPaths = new List<string>();

            string mainFolderPath = CreateMainDirectory(firstTeamName, secondTeamName);

            foreach (Camera camera in cameras)
            {
                string cameraFolderPath = CreateCameraDirectory(mainFolderPath, camera.Name);
                videoPaths.Add(GenerateChallengeVideoPath(cameraFolderPath));
            }

            return videoPaths;
        }

        public string CreateMainDirectory(string firstTeamName, string secondTeamName)
        {
            string folderPath = desktopPath + "\\" + firstTeamName + "-vs" + secondTeamName;
            CreateDirectory(folderPath);

            return folderPath;
        }

        public string CreateCameraDirectory(string mainFolderPath, string cameraName)
        {
            string cameraPath = mainFolderPath + "\\Camera-" + cameraName;
            CreateDirectory(cameraPath);

            return cameraPath;
        }

        public string GenerateChallengeVideoPath(string cameraPath)
        {
            return cameraPath + "\\" + currentTimestamp + videoExtension;
        }

        private void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
