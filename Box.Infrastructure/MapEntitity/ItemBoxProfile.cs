using AutoMapper;
using Box.Contracts.DTO;
using Box.Domain.Models;

namespace Box.Infrastructure.MapEntitity
{
    /// <summary>
    /// профиль маппера для <see cref="ItemBox"/>
    /// </summary>
    public class ItemBoxProfile: Profile
    {
        public ItemBoxProfile()
        {
            CreateMap<ShortItemBoxDto, ItemBox>(MemberList.None)
                .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
                .ForMember(d => d.Description, map => map.MapFrom(c => c.Description));
            CreateMap<ItemBox, ShortItemBoxDto>(MemberList.None)
                .ForMember(c => c.Name, map => map.MapFrom(c => c.Name));

            CreateMap<ItemBoxDto, ItemBox>(MemberList.None)
                .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
                .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
                .ForMember(d => d.Description, map => map.MapFrom(c => c.Description));
            CreateMap<ItemBox, ItemBoxDto>(MemberList.None)
                .ForMember(c => c.Id, map => map.MapFrom(c => c.Id))
                .ForMember(c => c.Name, map => map.MapFrom(c => c.Name))
                .ForMember(c => c.Description, map => map.MapFrom(c => c.Description));
        }
    }
}
