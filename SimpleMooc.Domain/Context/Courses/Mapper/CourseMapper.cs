using AutoMapper;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Mapper
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseResponse>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, map => map.MapFrom(src => src.Description))
                .ForMember(dst => dst.Slug, map => map.MapFrom(src => src.Slug))
                .ForMember(dst => dst.Start, map => map.MapFrom(src => src.StartDate))
                .ForMember(dst => dst.Url, map => map.MapFrom(src => src.UrlImage));
        }
    }
}