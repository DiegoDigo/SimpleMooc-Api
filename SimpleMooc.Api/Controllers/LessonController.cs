using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Services;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/lessons")]
    [Authorize]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILessonService _lessonService;


        public LessonController(IMediator mediator, ILessonService lessonService)
        {
            _mediator = mediator;
            _lessonService = lessonService;
        }


        [HttpPost]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        [DisableRequestSizeLimit,
         RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
             ValueLengthLimit = int.MaxValue)]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] LessonCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        [HttpGet("{courseId:Guid}")]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> GetAll(Guid courseId)
        {
            var response = await _lessonService.GetAllLessonByCourse(courseId);
            return ((List<CourseLessonResponse>) response.Content).Any() ? Ok(response) : NoContent();
        }
    }
}