using AutoMapper;
using OnlineStore.Application.DTOs.Admin;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Service.DTOs;
using OnlineStore.Application.DTOs.Cart;
using System.Xml;


namespace OnlineStore.Application.Mappign
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserDTO>();
            CreateMap<Category, CategoriesDTO>().ReverseMap();
            CreateMap<IdentityRole, RolteDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Product, ProductElementDTO>()
                .ForMember(DTO => DTO.InStocke , OPT => OPT.MapFrom(src => src.ProductVariants.Where(v => v.Quantity > 0).Count() >0))
                .ForMember(DTO => DTO.CategoryName , opt => opt.MapFrom(src => src.SubCategory.Name))
                .ForMember(DTO => DTO.ImageCover , opt => opt.MapFrom(src => src.ImageCover));

            CreateMap<CartItems, RetriveCartItemsDTO>()
                .ForMember(Dto => Dto.ProductImage , opt => opt.MapFrom(src => src.ProductVariants.Image))
                .ForMember(Dto => Dto.ProductName , opt => opt.MapFrom(src => src.ProductVariants.Product.Name))
                .ForMember(Dto => Dto.Price  , opt => opt.MapFrom(src => src.ProductVariants.Product.Price))
                .ForMember(Dto => Dto.Color , opt => opt.MapFrom(src => src.ProductVariants.Color))
                .ForMember(Dto => Dto.Size, opt => opt.MapFrom(src => src.ProductVariants.Size))
                .ForMember(Dto => Dto.ProductVaiantID , opt => opt.MapFrom(src => src.ProductID))
                .ForMember(Dto => Dto.CartProductQuantity , opt => opt.MapFrom(src => src.Qunatity))
                .ForMember(Dto => Dto.CartItemId , opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateCartItemDTO, CartItems>()
                .ForMember(cart => cart.ProductID, opt => opt.MapFrom(dto => dto.ProductVariantId))
                .ForMember(cart => cart.Qunatity, opt => opt.MapFrom(dto => dto.CartQuantity));
                

            CreateMap<UpdateCartItemDTO, CartItems>();

            CreateMap<CreateProductDTO, Product>()
                .ForMember(p => p.SubCategoryId , opt => opt.MapFrom(dto =>dto.CategoryId));

           



        }
    }
}
 