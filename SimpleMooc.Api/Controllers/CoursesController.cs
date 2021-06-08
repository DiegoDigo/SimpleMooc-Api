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

        /// <summary>
        /// Buscar todos os cursos.
        /// </summary>
        /// <returns>Retorna todos os cursos.</returns>
        /// <response code="200">Retorna todos os cursos</response>
        /// <response code="204">Não tem cursos cadastrados.</response>
        [HttpGet]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<BaseResponse>> GetAll()
        {
            var response = await _courseService.GetAll();
            return (response.Content as List<CourseResponse>)!.Any() ? Ok(response) : NoContent();
        }
        
        /// <summary>
        /// Buscar o curso por Slug (atalho).
        /// </summary>
        /// <param name="slug">(atalho)</param>
        /// <returns>Retorna o curso por Slug (atalho).</returns>
        /// <response code="200">Retorna o curso por Slug (atalho).</response>
        /// <response code="404">Não encontrou curso para esse atalho.</response>
        [HttpGet("{slug}")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseResponse>>GetBySlug(string slug)
        {
            var response = await _courseService.GetBySlug(slug);
            return response.Success ? Ok(response) : StatusCode(404, response);
        }
        
        /// <summary>
        /// Crinado Curso
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Retorna o curso criado.</returns>
        /// <response code="201">Retorna o curso criado..</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        /// <response code="401">Usuário não está logado.</response>  
        /// <response code="403">Usuário não tem permissão.</response>  
        [HttpPost]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BaseResponse>> Create([FromForm] CourseCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }
        
        /// <summary>
        /// Atualiza o curso.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Atualiza o curso</returns>
        /// <response code="200">Retorna o curso atualziado.</response>
        /// <response code="404">Não encontrou curso para esse atalho.</response>
        /// <response code="401">Usuário não está logado.</response>  
        /// <response code="403">Usuário não tem permissão.</response>  
        [HttpPut]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BaseResponse>> Update([FromForm] CourseUpdateCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Buscar curso do descrição.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Retorna todo os cursos de acordo com a descriação</returns>
        /// <response code="200">Retorna os curso.</response>
        /// <response code="404">Não encontrou curso para esse atalho.</response>
        [HttpGet("search/{search}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Search(string search)
        {
            var response = await _courseService.Search(search);
            return response.Success ? Ok(response) : NoContent();
        }
        
        /// <summary>
        /// Deletar curso.
        /// </summary>
        /// <param name="courseId"></param>
        /// <response code="204">Curso deletado.</response>
        /// <response code="404">Não encontrou curso.</response>
        /// <response code="401">Usuário não está logado.</response>  
        /// <response code="403">Usuário não tem permissão.</response>  
        [HttpDelete("{courseId}")]
        [ApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BaseResponse>> Delete(Guid courseId)
        {
            var response = await _courseService.Delete(courseId);
            return response.Success ? StatusCode(204, response) : NoContent();
        }

    }
}