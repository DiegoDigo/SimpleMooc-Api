using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Repositories;
using SimpleMooc.Shared.Services;

namespace SimpleMooc.Domain.Context.Courses.Handlers
{
    public class CourseHandler : IRequestHandler<CourseCommand, BaseResponse>
    {
        private readonly IUploadService _uploadService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;

        public CourseHandler(IUploadService uploadService, IUnitOfWork unitOfWork, ICourseRepository courseRepository)
        {
            _uploadService = uploadService;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public async Task<BaseResponse> Handle(CourseCommand command, CancellationToken cancellationToken)
        {
            var course = new Course(command.Name, command.Description);
            var image = command.Image;

            if (image is {Length: > 0})
            {
                course.ChangeUrlImage(await _uploadService.UploadImageCourse(course.Slug, image.OpenReadStream()));
            }

            await _courseRepository.Save(course);
            await _unitOfWork.Commit();
            return new BaseResponse(true, "Course Register success", course);
        }
    }
}