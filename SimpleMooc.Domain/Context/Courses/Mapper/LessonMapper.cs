using AutoMapper;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Mapper
{
    public class LessonMapper : Profile
    {
        public LessonMapper()
        {
            CreateMap<Lesson, CourseLessonResponse>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dst => dst.Description, map => map.MapFrom(src => src.Description))
                .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dst => dst.Number, map => map.MapFrom(src => src.Number))
                .ForMember(dst => dst.Url, map => map.MapFrom(src => src.UrlVideos))
                .ForMember(dst => dst.ReleaseDate, map => map.MapFrom(src => src.ReleaseDate));
        }
    }
}