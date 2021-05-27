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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> GetAll(Guid userId)
        {
            var enrollments = await _enrollmentRepository.GetByUserId(userId);
            var courseResponses = enrollments
                .Select(enrollment => _mapper.Map<Course, CourseResponse>(enrollment.Course))
                .ToList();
            return new BaseResponse(true, "enrollment", courseResponses);
        }
    }
}