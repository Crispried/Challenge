using System;
using System.Collections.Generic;

namespace Challange.Domain.Entities
{
    public class GameInformation
    {
        public string FirstTeam { get; set; }

        public string SecondTeam { get; set; }

        public int FirstTeamScore { get; set; }

        public int SecondTeamScore { get; set; }

        public string WinnerTeam { get; set; }

        public DateTime GameStart { get; set; }

        public DateTime GameEnd { get; set; }

        public List<ChallengeCase> Challenges { get; set; }
    }
}
