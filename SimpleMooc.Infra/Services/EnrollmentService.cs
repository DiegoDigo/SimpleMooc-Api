using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Domain.Context.Courses.Services;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Repositories;

namespace SimpleMooc.Infra.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _mUnitOfWork;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IMapper mapper, IUnitOfWork mUnitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _mUnitOfWork = mUnitOfWork;
        }

        public async Task<BaseResponse> GetAll(Guid userId)
        {
            var enrollments = await _enrollmentRepository.GetByUserId(userId);
            var courseResponses = enrollments
                .Select(enrollment => _mapper.Map<Course, CourseResponse>(enrollment.Course))
                .ToList();
            return new BaseResponse(true, "Inscrições", courseResponses);
        }

        public async Task<BaseResponse> CloseEnrollmentByCourseIdAndUserId(Guid courseId, Guid userId)
        {
            var enrollment = await _enrollmentRepository.GetByUserIdAndCourseId(userId, courseId);
            if (enrollment is null)
            {
                return new BaseResponse(false, "Inscrição nao encontrada.", null);
            }

            _enrollmentRepository.Delete(enrollment);
            await _mUnitOfWork.Commit();
            return new BaseResponse(true, "Inscrição cancelada.", null);
        }
    }
}