using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleMooc.Api.Controllers;
using SimpleMooc.Api.Core;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Domain.Context.Users.Services;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Test.Util;
using Xunit;

namespace SimpleMooc.Test.Controller
{
    public class ProfileControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IProfileService> _profileService;
        private Mock<ClaimsPrincipal> _claims;

        private void Setup()
        {
            _mediator = new Mock<IMediator>();
            _profileService = new Mock<IProfileService>();
            _claims = new Mock<ClaimsPrincipal>();
        }

        private BaseResponse _baseResponse()
        {
            var profileResponse = new ProfileResponse(
                Guid.NewGuid(),
                "Diego Domingos Delmiro",
                "",
                "teste@gmail.com"
            );

            return new BaseResponse(true, "Criado com sucesso", profileResponse);
        }


        [Fact]
        public async Task Create_CriarPerfilUsuario_ReturnOK()
        {
            Setup();

            _mediator
                .Setup(x => x.Send(It.IsAny<ProfileCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_baseResponse()));

            var fileMock = new Mock<IFormFile>();

            var command = new ProfileCommand("Diego", "Domingos Delmiro", fileMock.Object);

            var profileController = new ProfileController(_mediator.Object, _profileService.Object);
            var register = await profileController.Create(command);

            Assert.Equal(201, ResponseUtil.GetObjectResultStatusCode(register));
            Assert.True(ResponseUtil.GetObjectResultContent(register).Success);
        }
    }
}