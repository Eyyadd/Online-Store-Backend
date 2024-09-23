using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class UserServices : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager , IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public User GetUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public void AssignRoleToUser(string userEmail, string roleName)
        {
            var user = _userManager.FindByEmailAsync(userEmail).Result;
            if (user != null)
            {
                _userManager.AddToRoleAsync(user, roleName).Wait();
            }
        }

    

      


        //public void AssignRoleToUser(int userId, int roleId)
        //{
        //    string userIdStr = userId.ToString();
        //    string roleIdStr = roleId.ToString();
        //    // Find the user by ID
        //    var user = _userManager.Users.FirstOrDefault(u => u.Id == userIdStr);
        //    if (user == null)
        //    {
        //        throw new ArgumentException($"User with ID {userId} not found.");
        //    }

        //    // Find the role by ID
        //    var role = _roleManager.Roles.FirstOrDefault(r => r.Id == roleIdStr);
        //    if (role == null)
        //    {
        //        throw new ArgumentException($"Role with ID {roleId} not found.");
        //    }

        //    // Assign the role to the user
        //    var result = _userManager.AddToRoleAsync(user, role.Name).Result;
        //    if (!result.Succeeded)
        //    {
        //        throw new Exception("Failed to assign role to user.");
        //    }
        //}
    }


}

