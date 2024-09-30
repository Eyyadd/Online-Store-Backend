﻿using OnlineStore.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IOwnerService
    {
        IList<UsersDTO> GetAllOwners();
    }
}
