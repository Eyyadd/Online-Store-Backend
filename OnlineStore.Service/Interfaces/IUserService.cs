using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IUserService
    {
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
        //void AssignRoleToUser(int userId, int roleId);
         Task AssignRoleToUser(string userEmail, string roleName);

      

        
    }
}
