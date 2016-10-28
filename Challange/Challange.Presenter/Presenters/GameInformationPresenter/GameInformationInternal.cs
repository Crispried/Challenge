using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.GameInformationPresenter
{
    public partial class GameInformationPresenter
    {
        /// <summary>
        /// Creates directory for current game
        /// </summary>
        /// <param name="name"></param>
        private void CreateRootDirectory(string name)
        {
            fileService.CreateDirectory(name);
        }

        /// <summary>
        /// Gets path to file with game information
        /// </summary>
        /// <param name="directioryName"></param>
        /// <returns></returns>
        private string FormatPathToFile(string directioryName)
        {
            string fileName = @"\Game_Information.xml";
            return pathFormatter.FormatPathToGameInformationFile(directioryName, fileName);
        }

        /// <summary>
        /// Creates xml file with game information
        /// </summary>
        /// <param name="gameInfo"></param>
        /// <param name="path"></param>
        private void SaveGameInformation(GameInformation gameInfo, string path)
        {
            fileWorker.SerializeXml(gameInfo, path);
        }

        private string FormatDirectoryName()
        {
            return gameInformation.FirstTeam +
                "_vs_" + gameInformation.SecondTeam + "(" + gameInformation.Date + ")";
        }
    }
}
