using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMooc.Api.Core;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Services;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/profiles")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProfileService _profileService;

        public ProfileController(IMediator mediator, IProfileService profileService)
        {
            _mediator = mediator;
            _profileService = profileService;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] ProfileCommand command)
        {
            command = command with
            {
                UserId = SubjectUser.GetId(User)
            };
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> GetByUserId()
        {
            var userId = SubjectUser.GetId(User);

            var response = await _profileService.GetByUserId(userId);
            return response.Success ? Ok(response) : NotFound();
        }
        
        [HttpPut]
        [ApiVersion("1.0")]
        public async Task<ActionResult<BaseResponse>> Update([FromForm] ProfileUpdateCommand command)
        {
            command = command with
            {
                UserId = SubjectUser.GetId(User)
            };
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }
    }
}