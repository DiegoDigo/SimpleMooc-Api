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
            var lessons = await _lessonRepository.GetAllByCourse(courseId);
            var response = lessons
                .Select(lesson => _mapper.Map<Lesson, CourseLessonResponse>(lesson))
                .ToList();

            return new BaseResponse(true, "Aulas", response);
        }

        public async Task<BaseResponse> GetQuantitiesLessonByCourse(Guid courseId)
        {
            var lessonsEnumerable = await _lessonRepository.GetAllByCourse(courseId);
            var lessons = lessonsEnumerable.ToList();
            var response = new QuantityLessonResponse(lessons.Count);
            return new BaseResponse(true, "Quantidade de aulas", response);
        }
    }
}