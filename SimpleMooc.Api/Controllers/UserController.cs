using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "email": "teste@teste.com",
        ///        "password": "123senha"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Retorna o token de acesso.</returns>
        /// <response code="201">Retorna o token de acesso.</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Register([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        /// <summary>
        /// Login do usuário.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "email": "teste@teste.com",
        ///        "password": "123senha"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <permission cref="AllowAnonymousAttribute"></permission>
        /// <returns>Retorna o token de acesso.</returns>
        /// <response code="200">Retorna o token de acesso.</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        [HttpPost("login")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /token/refresh
        ///     {
        ///        "refresh": "refresh token aqui" ,
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Retorna o token de acesso.</returns>
        /// <response code="200">Retorna o token de acesso.</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        /// <response code="401">Usuário não está logado.</response>  
        [HttpPost("token/refresh")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> Refresh([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }
    }
}