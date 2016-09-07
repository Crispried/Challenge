using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters
{
    public class GameInformationPresenter :
                    BasePresenter<IGameInformationView, GameInformation>
    {
        private GameInformation gameInformation;

        public GameInformationPresenter(
        IApplicationController controller,
        IGameInformationView gameInformationView) :
                base(controller, gameInformationView)
        {
            View.SetGameInformation += () =>
                         PrepareApplication(
                                    View.GameInformation);
        }

        public override void Run(GameInformation argument)
        {
            gameInformation = argument;
            View.Show();
        }

        /// <summary>
        /// 1. Initialize game information
        /// 2. Create folder structure
        /// 3. Create xml file with game information
        /// </summary>
        /// <param name="newSettings"></param>
        private void PrepareApplication(
                        GameInformation gameInfo)
        {
            InitializeGameInformation(gameInfo);
            var directoryName = gameInformation.DirectoryName;
            CreateRootDirectory(directoryName);
            var pathToFile = PathToFile(directoryName);
            SaveGameInformation(gameInformation, pathToFile);
            View.Close();
        }

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

        private string PathToFile(string directioryName)
        {
            return directioryName + @"\Game_Information.xml";
        }

        private void CreateRootDirectory(string name)
        {
            FileService.CreateDirectory(name);
        }

        private void SaveGameInformation(GameInformation gameInfo, string path)
        {
            FileWorker.SerializeXml(gameInfo, path);
        }
    }
}
