using System;
using System.Threading.Tasks;
using Moq;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Handler;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Domain.Context.Users.Services;
using SimpleMooc.Shared.Repositories;
using Xunit;

namespace SimpleMooc.Test.Domain.Handler
{
    public class UserHandlerTest
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<ITokenService> _tokenService;
        private Mock<IRefreshTokenRepository> _refreshTokenRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        private void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _tokenService = new Mock<ITokenService>();
            _refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        private static User _user()
        {
            var user = new User("teste@gmail.com");
            user.ChangePassword("12345678");
            return user;
        }

        private RefreshToken _refreshToken()
            => new(GenerateRefreshToken(), _user());

        private string GenerateRefreshToken()
            => Convert.ToBase64String(new byte[64]);


        [Fact]
        public async Task UserHandler_Login_User_Success()
        {
            Setup();

            _userRepository
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(_user()));

            _refreshTokenRepository
                .Setup(x => x.GetByUserId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_refreshToken()));

            _tokenService
                .Setup(x => x.GenerateToken(It.IsAny<User>()))
                .Returns(
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");


            var command = new LoginCommand("teste@gmail.com", "12345678");
            var userHandler = new UserHandler(_userRepository.Object, _tokenService.Object,
                _refreshTokenRepository.Object, _unitOfWork.Object);
            var response = await userHandler.Handle(command, new System.Threading.CancellationToken());


            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotEmpty(((TokenResponse) response.Content).Refresh);
            Assert.NotEmpty(((TokenResponse) response.Content).Token);
        }

        [Fact]
        public async Task UserHandler_Register_User_Success()
        {
            Setup();

            _userRepository
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(null));

            _tokenService
                .Setup(x => x.GenerateToken(It.IsAny<User>()))
                .Returns(
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            _tokenService.Setup(x => x.GenerateRefreshToken())
                .Returns(GenerateRefreshToken());

            var command = new RegisterUserCommand("teste@gmail.com", "12345678");
            var userHandler = new UserHandler(_userRepository.Object, _tokenService.Object,
                _refreshTokenRepository.Object, _unitOfWork.Object);
            var response = await userHandler.Handle(command, new System.Threading.CancellationToken());


            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotEmpty(((TokenResponse) response.Content).Refresh);
            Assert.NotEmpty(((TokenResponse) response.Content).Token);
        }

        [Fact]
        public async Task UserHandler_RefreshToken_User_Success()
        {
            Setup();

            _refreshTokenRepository
                .Setup(x => x.GetByToken(It.IsAny<string>()))
                .Returns(Task.FromResult(_refreshToken()));

            _tokenService.Setup(x => x.GenerateRefreshToken())
                .Returns(GenerateRefreshToken());

            _tokenService
                .Setup(x => x.GenerateToken(It.IsAny<User>()))
                .Returns(
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            var command = new RefreshTokenCommand(_refreshToken().Token);
            var userHandler = new UserHandler(_userRepository.Object, _tokenService.Object,
                _refreshTokenRepository.Object, _unitOfWork.Object);
            var response = await userHandler.Handle(command, new System.Threading.CancellationToken());


            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotEmpty(((TokenResponse) response.Content).Refresh);
            Assert.NotEmpty(((TokenResponse) response.Content).Token);
        }
    }
}