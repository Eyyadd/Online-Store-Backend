using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IRoleServices
    {
        IdentityRole GetRoleByName(string roleName);
        IEnumerable<IdentityRole> GetAllRoles();
        void CreateRole(string roleName);
        bool RoleExists(string roleName);
    }
}
