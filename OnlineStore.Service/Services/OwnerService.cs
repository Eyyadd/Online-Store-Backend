using AutoMapper;
using OnlineStore.Application.DTOs.User;
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
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ownerRepository = _unitOfWork.OwnerRepository();

        }
        public IList<UsersDTO> GetAllOwners()
        {
            var owners = _userManager.GetUsersInRoleAsync(Roles.BrandOwner).Result;
            if (owners is not null)
            {
               var OwnersMApping= _mapper.Map<IList<User>,IList<UsersDTO>>(owners);
                return OwnersMApping;
            }
            return null;
        }

       
    }

}
