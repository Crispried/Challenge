using Challange.Domain.Entities;

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
            return pathFormatter.FormatPathToGameInformationFile(directioryName);
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
