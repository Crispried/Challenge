using System;
using System.Collections.Generic;

namespace Challange.Domain.Entities
{
    public class GameInformation
    {
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
                return FormatDirectoryName();
            }
        }

        private string FormatDirectoryName()
        {
            return FirstTeam + "_vs_" + SecondTeam + "(" + Date + ")";
        }
    }
}
