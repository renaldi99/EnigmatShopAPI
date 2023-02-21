using AutoMapper;
using EnigmatShopAPI.Dto;
using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // <source / opt, destination / dest>
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
        }
    }
}
