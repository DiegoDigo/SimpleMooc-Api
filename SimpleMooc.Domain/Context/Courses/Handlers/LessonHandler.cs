using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Entities.Enums;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Repositories;
using SimpleMooc.Shared.Services;

namespace SimpleMooc.Domain.Context.Courses.Handlers
{
    public class LessonHandler : IRequestHandler<LessonCommand, BaseResponse>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUploadService _uploadService;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public LessonHandler(ILessonRepository lessonRepository, ICourseRepository courseRepository,
            IUnitOfWork iUnitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<BaseResponse> Handle(LessonCommand command, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(command.CourseId);
            if (course is null)
            {
                return new BaseResponse(false, "Curso não encontrado.", null);
            }

            var lessons = await _lessonRepository.GetAllByCourse(course.Id);
            var numberLesson = lessons?.Count() + 1 ?? 0;

            var lesson = new Lesson(command.Name, command.Description, course);

            var file = command.Material;

            if (file is {Length: > 0})
            {
                if (!file.ContentType.StartsWith("video"))
                {
                    return new BaseResponse(true, "media nao suportada.", null);
                }

                var urlMaterial =
                    await _uploadService.UploadMaterial(command.Name, file.OpenReadStream(), cancellationToken);
                lesson.ChangeMaterial(urlMaterial);
            }

            lesson.ChangeNumberLesson(numberLesson);

            await _lessonRepository.Save(lesson);
            await _iUnitOfWork.Commit();
            var lessonMaterialResponse = _mapper.Map<Lesson, CourseLessonResponse>(lesson);
            return new BaseResponse(true, "Aula registrada com sucesso.", lessonMaterialResponse);
        }
    }
}