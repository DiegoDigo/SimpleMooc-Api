using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Mappers
{
    public class ProfileMapper : AutoMapper.Profile
    {
        public ProfileMapper()
        {
            CreateMap<Profile, ProfileResponse>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.FullName))
                .ForMember(dst => dst.UrlImage, map => map.MapFrom(src => src.UrlImage))
                .ForMember(dst => dst.Email, map => map.MapFrom(src => src.User.Email));
        }
    }
}