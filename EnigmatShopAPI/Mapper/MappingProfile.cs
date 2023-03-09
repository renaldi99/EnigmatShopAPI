using AutoMapper;
using EnigmatShopAPI.Dto;
using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // <source, destination>
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // jika dto tidak ada attribute bisa di ignore
                .ForMember(dest => dest.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(x => x.RefreshToken));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.ProductName))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(x => x.Stock))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.Image));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.ProductName))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(x => x.Stock))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.Image));
        }
    }
}
