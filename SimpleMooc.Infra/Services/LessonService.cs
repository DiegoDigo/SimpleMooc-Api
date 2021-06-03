using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Domain.Context.Courses.Services;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Infra.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }


        public async Task<BaseResponse> GetAllLessonByCourse(Guid courseId)
        {
            var materials = await _lessonRepository.GetAllByCourse(courseId);
            var response = materials
                .Select(lesson => _mapper.Map<Lesson, CourseLessonResponse>(lesson))
                .ToList();

            return new BaseResponse(true, "Aulas", response);
        }
    }
}