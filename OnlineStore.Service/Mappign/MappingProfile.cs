using AutoMapper;
using OnlineStore.Application.DTOs.Admin;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Service.DTOs;
using OnlineStore.Application.DTOs.Cart;
using System.Xml;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.DTOs.Discount;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.User;
using OnlineStore.Application.DTOs.Wishlist;
using OnlineStore.Application.DTOs.Review;


namespace OnlineStore.Application.Mappign
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserDTO>();
            CreateMap<Category, CategoriesDTO>().ReverseMap();
            CreateMap<IdentityRole, RolteDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Product, ProductElementDTO>()
                .ForMember(DTO => DTO.InStocke, OPT => OPT.MapFrom(src => src.ProductVariants.Where(v => v.Quantity > 0).Count() > 0))
                .ForMember(DTO => DTO.CategoryName, opt => opt.MapFrom(src => src.SubCategory.Name))
                .ForMember(DTO => DTO.ImageCover, opt => opt.MapFrom(src => src.ImageCover));

            CreateMap<CartItems, RetriveCartItemsDTO>()
                .ForMember(Dto => Dto.ProductImage, opt => opt.MapFrom(src => src.ProductVariants.Image))
                .ForMember(Dto => Dto.ProductName, opt => opt.MapFrom(src => src.ProductVariants.Product.Name))
                .ForMember(Dto => Dto.Price, opt => opt.MapFrom(src => src.ProductVariants.Product.Price))
                .ForMember(Dto => Dto.Color, opt => opt.MapFrom(src => src.ProductVariants.Color))
                .ForMember(Dto => Dto.Size, opt => opt.MapFrom(src => src.ProductVariants.Size))
                .ForMember(Dto => Dto.ProductVaiantID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(Dto => Dto.CartProductQuantity, opt => opt.MapFrom(src => src.Qunatity))
                .ForMember(Dto => Dto.CartItemId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateCartItemDTO, CartItems>()
                .ForMember(cart => cart.ProductID, opt => opt.MapFrom(dto => dto.ProductVariantId))
                .ForMember(cart => cart.Qunatity, opt => opt.MapFrom(dto => dto.CartQuantity))
                .ForMember(cart => cart.CartId, opt => opt.MapFrom(dto => dto.CartId));
                .ForMember(cart => cart.Qunatity, opt => opt.MapFrom(dto => dto.CartQuantity));
                

            CreateMap<UpdateCartItemDTO, CartItems>();

            CreateMap<CreateProductDTO, Product>()
                .ForMember(p => p.SubCategoryId, opt => opt.MapFrom(dto => dto.CategoryId));

            CreateMap<CategoriesDTO, Category>().ReverseMap();
            CreateMap<UpdatedCategoryDTO, Category>().ReverseMap();

            CreateMap<DiscountDTO, Discount>().ForMember(dest => dest.DiscountCode,
                            src => src.MapFrom(src => src.Name))
                .ForMember(dest => dest.DiscountAmount,
                            src => src.MapFrom(src => src.Percentage)).ReverseMap();



            CreateMap<AddDiscountDTO, Discount>()
                .ForMember(dest => dest.DiscountCode,
                            src => src.MapFrom(src => src.Name))
                .ForMember(dest => dest.DiscountAmount,
                            src => src.MapFrom(src => src.Percentage));



            CreateMap<Discount, AddDiscountDTO>()
                .ForMember(dest => dest.Name,
                            src => src.MapFrom(src => src.DiscountCode))
                .ForMember(dest => dest.Percentage,
                            src => src.MapFrom(src => src.DiscountAmount))
                .ForMember(dest => dest.IsActive,
                           src => src.MapFrom(src => src.EndDiscount > src.StartDiscount));

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<User, UsersDTO>().ReverseMap();
            CreateMap<CreatedWishlistDTO, Wishlist>().ReverseMap();
            //.ForMember(dest => dest.Id, src => src.MapFrom(src => src.WishlistId))
            //.ForMember(dest => dest.UserId, src => src.MapFrom(src => src.UserId));
            //CreateMap<Wishlist, CreatedWishlistDTO>()
            //    .ForMember(dest => dest.WishlistId, src => src.MapFrom(src => src.Id));
            CreateMap<AddReview, Review>().ReverseMap();
        }
    }
}
