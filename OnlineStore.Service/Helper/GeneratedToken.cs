using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Helper
{
    public class GeneratedToken
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }

        public GeneratedToken(string Token,DateTime ValidTo)
        {
            this.Token = Token;
            this.ValidTo = ValidTo;
        }
    }
}
