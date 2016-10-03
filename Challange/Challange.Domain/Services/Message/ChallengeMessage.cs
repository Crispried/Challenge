using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Domain.Services.Message
{
    public class ChallengeMessage
    {
        public MessageType MessageType { get; set; }

        public string Caption { get; set; }

        public string Text { get; set; }

        public MessageBoxButtons MessageButtons { get; set; }

        public MessageBoxIcon MessageIcon { get; set; }
    }
}
