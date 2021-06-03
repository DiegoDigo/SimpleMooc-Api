using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Criar Perfil de usuário
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna o perfil do usuario.</returns>
        /// <response code="201">Retorna o perfil do usuario.</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        /// <response code="401">Usuário não está logado.</response>  
        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] ProfileCommand command)
        {
            command = command with
            {
                UserId = SubjectUser.GetId(User)
            };
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        /// <summary>
        /// Burca Perfil de usuário
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna o perfil do usuario.</returns>
        /// <response code="201">Retorna o perfil do usuario.</response>
        /// <response code="204">Perfil não encontrado.</response>  
        /// <response code="401">Usuário não está logado.</response>  
        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> GetByUserId()
        {
            var userId = SubjectUser.GetId(User);

            var response = await _profileService.GetByUserId(userId);
            return response.Success ? Ok(response) : StatusCode(404, response);
        }
        
        /// <summary>
        /// Atualizar Perfil de usuário
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna o perfil do usuario atualizado.</returns>
        /// <response code="201">Retorna o perfil do usuario atualizado.</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        /// <response code="401">Usuário não está logado.</response>  
        [HttpPut]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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