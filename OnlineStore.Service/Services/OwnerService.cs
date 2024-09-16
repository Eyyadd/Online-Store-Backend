using OnlineStore.Application.Interfaces;
using OnlineStore.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public OwnerService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IEnumerable<User> GetAllOwners()
        {
            var owners = _userManager.Users.Where(u => _userManager.IsInRoleAsync(u, Roles.BrandOwner).Result).AsNoTracking().ToList();
            return owners;
        }

        public void DemoteOwnerToCustomer(string ownerMail)
        {
            //var owner = _userManager.Users.FirstOrDefault(u=>u.Email == ownerMail);
            var owner = _userManager.FindByEmailAsync(ownerMail).Result;
            if (owner == null)
            {
                // Remove 'Owner' role and add 'Customer' role
                var removeOwnerRole = _userManager.RemoveFromRoleAsync(owner, "Owner").Result;
                if (removeOwnerRole.Succeeded)
                {
                    var addCustomerRole = _userManager.AddToRoleAsync(owner, "Customer").Result;
                    if (!addCustomerRole.Succeeded)
                    {
                        throw new Exception("Failed to assign 'Customer' role to the user.");
                    }
                }
                else
                {
                    throw new Exception("Failed to remove 'Owner' role from the user.");
                }
            }
            else
            {
                throw new ArgumentException($"User with email {ownerMail} not found.");
            }
        }

    }

}
