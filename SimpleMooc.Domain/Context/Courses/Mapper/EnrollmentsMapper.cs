using AutoMapper;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Mapper
{
    public class EnrollmentsMapper : Profile
    {
        public EnrollmentsMapper()
        {
            CreateMap<Enrollment, EnrollmentResponse>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Slug, map => map.MapFrom(src => src.Course.Slug))
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Course.Name))
                .ForMember(dst => dst.Status, map => map.MapFrom(src => src.Status));
        }
    }
}