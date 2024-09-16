using AutoMapper;
using OnlineStore.Application.DTOs.Admin;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Service.DTOs;


namespace OnlineStore.Application.Mappign
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserDTO>();
            CreateMap<Category, CategoriesDTO>().ReverseMap();
            CreateMap<IdentityRole, RolteDTO>().ReverseMap();
            //CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Product, ProductElementDTO>()
                .ForMember(DTO => DTO.InStocke , OPT => OPT.MapFrom(src => src.ProductVariants.Where(v => v.Quantity > 0).Count() >0))
                .ForMember(DTO => DTO.CategoryName , opt => opt.MapFrom(src => src.SubCategory.Name))
                .ForMember(DTO => DTO.ImageCover , opt => opt.MapFrom(src => src.ProductVariants.FirstOrDefault().Image));
        }
    }
}
