using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SimpleMooc.Api.Controllers;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Test.Util;
using Xunit;

namespace SimpleMooc.Test.Controller
{
    public class UserControllerTest
    {
        private Mock<IMediator> _mediator;

        private void Setup()
        {
            _mediator = new Mock<IMediator>();
        }

        public static BaseResponse _BaseResponse()
        {
            var tokenResponse =
                new TokenResponse(
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9");
            return new BaseResponse(true, "User Register", tokenResponse);
        }


        [Fact]
        public async Task Create_RegistarNovoUsuario_ReturnOK()
        {
            Setup();

            _mediator
                .Setup(x => x.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_BaseResponse()));

            var command = new RegisterUserCommand("teste@gmail.com", "12345678");

            var userController = new UserController(_mediator.Object);
            var register = await userController.Register(command);

            Assert.Equal(201, ResponseUtil.GetObjectResultStatusCode(register));
            Assert.True(ResponseUtil.GetObjectResultContent(register).Success);
        }
        
        [Fact]
        public async Task Login_Usuario_ReturnOK()
        {
            Setup();

            _mediator
                .Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_BaseResponse()));

            var command = new LoginCommand("teste@gmail.com", "12345678");

            var userController = new UserController(_mediator.Object);
            var register = await userController.Login(command);

            Assert.Equal(200, ResponseUtil.GetObjectResultStatusCode(register));
            Assert.True(ResponseUtil.GetObjectResultContent(register).Success);
        }
        
        [Fact]
        public async Task Refresh_AtualizarToken_ReturnOK()
        {
            Setup();

            _mediator
                .Setup(x => x.Send(It.IsAny<RefreshTokenCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_BaseResponse()));

            var command = new RefreshTokenCommand("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9");

            var userController = new UserController(_mediator.Object);
            var register = await userController.Refresh(command);

            Assert.Equal(200, ResponseUtil.GetObjectResultStatusCode(register));
            Assert.True(ResponseUtil.GetObjectResultContent(register).Success);
        }
    }
}