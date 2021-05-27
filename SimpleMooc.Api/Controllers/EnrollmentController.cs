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
    [Route("api/v{version:ApiVersion}/enrollments")]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IMediator mediator, IEnrollmentService enrollmentService)
        {
            _mediator = mediator;
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> Enrollment([FromBody] EnrollmentCommand command)
        {
            command = command with
            {
                UserId = SubjectUser.GetId(User)
            };
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> GetEnrollment()
        {
            var userId = SubjectUser.GetId(User);

            var response = await _enrollmentService.GetAll(userId);
            return (response.Content as List<CourseResponse>)!.Any() ? Ok(response) : NoContent();
        }
    }
}