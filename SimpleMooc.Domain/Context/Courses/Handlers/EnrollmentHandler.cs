using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Repositories;

namespace SimpleMooc.Domain.Context.Courses.Handlers
{
    public class EnrollmentHandler : IRequestHandler<EnrollmentCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentHandler(IUserRepository userRepository, ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(EnrollmentCommand command, CancellationToken cancellationToken)
        {
            var enrollmentFind = await _enrollmentRepository.GetByUserIdAndCourseId(command.UserId, command.CourseId);

            if (enrollmentFind is not null)
            {
                return new BaseResponse(false, "user already register to course.", null);
            }
            
            var user = await _userRepository.GetById(command.UserId);
            if (user is null)
            {
                return new BaseResponse(false, "user not found.", null);
            }

            var course = await _courseRepository.GetById(command.CourseId);
            if (course is null)
            {
                return new BaseResponse(false, "course not found.", null);
            }

            var enrollment = new Enrollment(user, course);
            await _enrollmentRepository.Save(enrollment);
            await _unitOfWork.Commit();
            var response = _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            return new BaseResponse(true, "Enrollment success.", response);
        }
    }
}