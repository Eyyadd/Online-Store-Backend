using OnlineStore.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ServicesInterfaces
{
    public interface ISendEmail
    {
       Task SendEmailAsync(Email email);
    }
}
