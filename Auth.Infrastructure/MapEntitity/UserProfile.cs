using Auth.Contracts.DTO;
using Auth.Domain.Models;
using AutoMapper;

namespace Auth.Infrastructure.MapEntitity
{
    /// <summary>
    /// Профиль маппера для <see cref="User"/>
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, User>(MemberList.None)
                .ForMember(d => d.UserName, map => map.MapFrom(s => s.UserName))
                .ForMember(d => d.Login, map => map.MapFrom(s => s.Login))
                .ForMember(d => d.PasswordHash, map => map.MapFrom(s => s.Password))
                .ForMember(d => d.CreatedDate, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<User, ShortUserDto>()
                .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
                .ForMember(d => d.UserName, map => map.MapFrom(s => s.UserName));
        }
    }
}
   
    


