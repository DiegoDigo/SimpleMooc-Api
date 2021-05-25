using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Register([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        [HttpPost("login")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }

        [HttpPost("token/refresh")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Refresh([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }
    }
}