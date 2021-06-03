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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Search(string search)
        {
            var searchCourse = await _courseRepository.SearchCourse(search);
            var courses = searchCourse
                .Select(course => _mapper.Map<Course, CourseResponse>(course))
                .OrderBy(course => course.Name)
                .ToList();
            return new BaseResponse(true, "Cursos.", courses);
        }
        
        public async Task<BaseResponse> GetAll()
        {
            var all = await _courseRepository.GetAll();
            var courses = all
                .Select(course => _mapper.Map<Course, CourseResponse>(course))
                .OrderBy(course => course.Name)
                .ToList();
            return new BaseResponse(true, "Cursos.", courses);
        }

        public async Task<BaseResponse> GetBySlug(string slug)
        {
            var searchCourse = await _courseRepository.GetBySlug(slug);
            if (searchCourse is null)
            {
                return new BaseResponse(false, "Curso não encontrado.", null);
            }
            var response = _mapper.Map<Course, CourseResponse>(searchCourse);
            return new BaseResponse(true, "Curso.", response);
        }
    }
}