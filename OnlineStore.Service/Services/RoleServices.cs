using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    internal class RoleServices: IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            return _roleManager.FindByNameAsync(roleName).Result;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public void CreateRole(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result)
            {
                var role = new IdentityRole(roleName);
                _roleManager.CreateAsync(role).Wait();
            }
        }

        public bool RoleExists(string roleName)
        {
            return _roleManager.RoleExistsAsync(roleName).Result;
        }

    }
}

