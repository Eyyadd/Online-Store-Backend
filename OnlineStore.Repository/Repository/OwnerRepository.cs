using Microsoft.AspNetCore.Identity;
using OnlineStore.Service.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public OwnerRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

       
        public IList<User> GetAll()
        {
            //_userManager.GetUsersInRoleAsync("Owner").Result; //
            var owners = _userManager.GetUsersInRoleAsync("Owner").Result; // = _userManager.Users.Where(u => _userManager.IsInRoleAsync(u, Roles.BrandOwner).Result).AsNoTracking().ToList();
            return owners is null ?[] : null ;

        }
    }
}
