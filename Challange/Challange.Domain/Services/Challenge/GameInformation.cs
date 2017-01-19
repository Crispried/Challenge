namespace Challange.Domain.Services.Challenge
{
    public class GameInformation
    {
        private string directoryName;

        public string FirstTeam { get; set; }

        public string SecondTeam { get; set; }

        public string Date { get; set; }

        public string GameStart { get; set; }
        
        public string Country { get; set; }

        public string City { get; set; }

        public string Part { get; set; }

        public string DirectoryName
        {
            get
            {
                return directoryName;
            }
            set
            {
                directoryName = value;
            }
        }

        public void SetGameInformation(GameInformation newGameInformation)
        {
            FirstTeam = newGameInformation.FirstTeam;
            SecondTeam = newGameInformation.SecondTeam;
            Date = newGameInformation.Date;
            GameStart = newGameInformation.GameStart;
            Country = newGameInformation.Country;
            City = newGameInformation.City;
            Part = newGameInformation.Part;
        }
    }
}
