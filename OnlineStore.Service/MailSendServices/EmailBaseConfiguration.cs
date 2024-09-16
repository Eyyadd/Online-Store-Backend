using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.MailSendServices
{
    public class EmailBaseConfiguration
    {
        public string SenderEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string DisplayName { get; set; } = null!;
    }
}
