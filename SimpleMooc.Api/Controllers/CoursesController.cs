using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Services;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICourseService _courseService;

        public CoursesController(IMediator mediator, ICourseService courseService)
        {
            _mediator = mediator;
            _courseService = courseService;
        }                                                           

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> GetAll()
        {
            var response = await _courseService.GetAll();
            return (response.Content as List<CourseResponse>)!.Any() ? Ok(response) : NoContent();
        }
    }
}