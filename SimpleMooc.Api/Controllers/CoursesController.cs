using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMooc.Api.Core;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Services;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/courses")]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> GetAll()
        {
            var response = await _courseService.GetAll();
            return (response.Content as List<CourseResponse>)!.Any() ? Ok(response) : NoContent();
        }
        
        [HttpGet("{slug}")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>>GetBySlug(string slug)
        {
            var response = await _courseService.GetBySlug(slug);
            return response.Success ? Ok(response) : NotFound();
        }
        
        [HttpPost]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] CourseCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : NoContent();
        }


    }
}