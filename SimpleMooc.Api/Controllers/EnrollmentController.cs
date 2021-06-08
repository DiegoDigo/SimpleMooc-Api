using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Inscrever-se no curso
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Restorna a inscrição.</returns>
        /// <response code="201">Restorna a inscrição.</response>
        /// <response code="406">Retorna os possíveis erros de validação</response>  
        /// <response code="401">Usuário não está logado.</response>  
        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> Enrollment([FromBody] EnrollmentCommand command)
        {
            command = command with
            {
                UserId = SubjectUser.GetId(User)
            };
            var response = await _mediator.Send(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }

        /// <summary>
        /// Pegar cursos que o usuario se inscreveu
        /// </summary>
        /// <returns>Restorna a inscrição.</returns>
        /// <response code="200">Restorna a inscrição.</response>
        /// <response code="204">Não tem cursos inscrito .</response>
        /// <response code="401">Usuário não está logado.</response>  
        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> GetEnrollment()
        {
            var userId = SubjectUser.GetId(User);

            var response = await _enrollmentService.GetAll(userId);
            return (response.Content as List<CourseResponse>)!.Any() ? Ok(response) : NoContent();
        }
        
        /// <summary>
        /// Sair do courso
        /// </summary>
        /// <returns>Retorna ok.</returns>
        /// <response code="200">Restorna q saiu do curso.</response>
        /// <response code="204">Não tem cursos inscrito .</response>
        /// <response code="401">Usuário não está logado.</response>  
        [HttpPut("{courseId:Guid}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CloseEnrollment(Guid courseId)
        {
            var userId = SubjectUser.GetId(User);
            var response = await _enrollmentService.CloseEnrollmentByCourseIdAndUserId(courseId, userId);
            return response.Success  ? Ok(response) : StatusCode(404, response);
        }
    }
}