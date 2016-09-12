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
        ///  Initializes game information
        /// </summary>
        /// <param name="gameInfo"></param>
        private void InitializeGameInformation(GameInformation gameInfo)
        {
            gameInformation.FirstTeam = gameInfo.FirstTeam;
            gameInformation.SecondTeam = gameInfo.SecondTeam;
            gameInformation.Date = gameInfo.Date;
            gameInformation.GameStart = gameInfo.GameStart;
            gameInformation.Country = gameInfo.Country;
            gameInformation.City = gameInfo.City;
            gameInformation.Part = gameInfo.Part;
        }

        /// <summary>
        /// Creates directory for current game
        /// </summary>
        /// <param name="name"></param>
        private void CreateRootDirectory(string name)
        {
            FileService.CreateDirectory(name);
        }

        /// <summary>
        /// Gets path to file with game information
        /// </summary>
        /// <param name="directioryName"></param>
        /// <returns></returns>
        private string PathToFile(string directioryName)
        {
            return directioryName + @"\Game_Information.xml";
        }

        /// <summary>
        /// Creates xml file with game information
        /// </summary>
        /// <param name="gameInfo"></param>
        /// <param name="path"></param>
        private void SaveGameInformation(GameInformation gameInfo, string path)
        {
            FileWorker.SerializeXml(gameInfo, path);
        }
    }
}
