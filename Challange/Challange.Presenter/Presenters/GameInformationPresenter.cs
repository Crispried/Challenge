using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters
{
    public class GameInformationPresenter :
                    BasePresenter<IGameInformationView>
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

        public override void Run()
        {
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
            gameInformation = InitializeGameInformation(gameInfo);
            var directioryName = FormatDirectoryName(gameInfo.FirstTeam,
                                 gameInfo.SecondTeam, gameInfo.Date);
            CreateRootDirectory(directioryName);
            var pathToFile = PathToFile(directioryName);
            SaveGameInformation(gameInfo, pathToFile);
            View.Close();
        }

        private GameInformation InitializeGameInformation(GameInformation gameInfo)
        {
            var result = new GameInformation()
            {
                FirstTeam = gameInfo.FirstTeam,
                SecondTeam = gameInfo.SecondTeam,
                Date = gameInfo.Date,
                GameStart = gameInfo.GameStart,
                Country = gameInfo.Country,
                City = gameInfo.City,
                Part = gameInfo.Part
            };
            return result;
        }

        private string FormatDirectoryName(string firstTeam,
                                            string secondTeam, string date)
        {
            return firstTeam + "_vs_" + secondTeam + "(" + date + ")";
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
