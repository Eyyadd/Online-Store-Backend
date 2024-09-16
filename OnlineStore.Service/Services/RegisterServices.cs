using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Services
{
    public static class RegisterServices
    {
        public static void AddRegisterServices(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IRoleServices, RoleServices>();
            service.AddScoped<IUserService, UserServices>();
            service.AddScoped<ICategoryServices, CategoryServices>();
            service.AddScoped<IOwnerService, OwnerService>();
            service.AddScoped<IDiscountService, DiscountService>();
        }
    }
}
