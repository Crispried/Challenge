using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.GameInformationPresenter
{
    public partial class GameInformationPresenter
    {
        public override void Run(GameInformation argument)
        {
            gameInformation = argument;
            View.Show();
        }

        /// <summary>
        /// Prepares application (means folder structure)
        /// </summary>
        /// <param name="newSettings"></param>
        public void PrepareApplication(
                        GameInformation gameInfo)
        {
            gameInformation.SetGameInformation(gameInfo);
            gameInformation.DirectoryName = FormatDirectoryName();
            var directoryName = gameInformation.DirectoryName;
            CreateRootDirectory(directoryName);
            var pathToFile = FormatPathToFile(directoryName);
            SaveGameInformation(gameInformation, pathToFile);
            View.Close();
        }
    }
}
