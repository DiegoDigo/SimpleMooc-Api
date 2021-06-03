using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        /// <summary>
        /// Criar Aula
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna a aula criada.</returns>
        /// <response code="201">Retorna a aula criada</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        /// <response code="401">Usuário não está logado.</response>  
        /// <response code="403">Usuário não tem permisão.</response>  
        [HttpPost]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        [DisableRequestSizeLimit,
         RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
             ValueLengthLimit = int.MaxValue)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] LessonCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        /// <summary>
        /// Buscar aulas por id do curso
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna as aulas.</returns>
        /// <response code="200">Buscar aulas por id do curso</response>
        /// <response code="204">Não possui aulas</response>
        /// <response code="401">Usuário não está logado.</response>  
        [HttpGet("{courseId:Guid}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> GetAll(Guid courseId)
        {
            var response = await _lessonService.GetAllLessonByCourse(courseId);
            return ((List<CourseLessonResponse>) response.Content).Any() ? Ok(response) : NoContent();
        }
    }
}