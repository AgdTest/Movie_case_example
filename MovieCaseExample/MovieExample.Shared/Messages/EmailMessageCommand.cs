using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieExample.Shared.Messages
{
    public class EmailMessageCommand
    {
        public EmailMessageCommand()
        {

        }

        public string SenderEmail { get; set; }

        public string ReceiverEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
