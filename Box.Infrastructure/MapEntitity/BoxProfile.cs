using AutoMapper;
using Box.Contracts.DTO;

namespace Box.Infrastructure.MapEntitity
{
    using Box.Domain.Models;
    /// <summary>
    /// профиль маппера для <see cref="Box"/>
    /// </summary>
    public class BoxProfile: Profile
    { 
        public BoxProfile() 
        {
            CreateMap<ShortBoxDto, Box>(MemberList.None)
                .ForMember(d => d.Name, map => map.MapFrom(c => c.Name));
            CreateMap<Box,ShortBoxDto>(MemberList.None)
                .ForMember(c => c.Name, map => map.MapFrom(c => c.Name));

            CreateMap<BoxDto, Box>(MemberList.None)
                .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
                .ForMember(d => d.Name, map => map.MapFrom(c => c.Name));
            CreateMap<Box, BoxDto>(MemberList.None)
                .ForMember(c => c.Id, map => map.MapFrom(c => c.Id))
                .ForMember(c => c.Name, map => map.MapFrom(c => c.Name));
        }
    }
}
