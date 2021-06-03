using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Handler;
using SimpleMooc.Domain.Context.Users.Mappers;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Shared.Repositories;
using SimpleMooc.Shared.Services;
using Xunit;
using Profile = SimpleMooc.Domain.Context.Users.Entities.Profile;

namespace SimpleMooc.Test.Domain.Handler
{
    public class ProfileHandlerTest
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IProfileRepository> _profileRepository;
        private Mock<IUploadService> _uploadService;
        private Mock<IUnitOfWork> _unitOfWork;

        private void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _profileRepository = new Mock<IProfileRepository>();
            _uploadService = new Mock<IUploadService>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        private static User _user()
        {
            var user = new User("teste@gmail.com");
            user.ChangePassword("12345678");
            return user;
        }

        private static Profile _profile()
        {
            return new("Diego", "Domingos Delmiro", _user());
        }

        [Fact]
        public async void ProfileHandler_CriandoPerfil_Success()
        {
            Setup();

            _userRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_user()));

            _uploadService
                .Setup(x => x.UploadImageProfile(It.IsAny<Guid>(), It.IsAny<Stream>()))
                .Returns(Task.FromResult(It.IsAny<string>()));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ProfileMapper()));
            IMapper mapper = new Mapper(configuration);

            var fileMock = new Mock<IFormFile>();

            var command = new ProfileCommand("Diego", "Domingos", fileMock.Object) {UserId = _user().Id};
            var profileHandler = new ProfileHandler(_profileRepository.Object, _userRepository.Object,
                _uploadService.Object, mapper, _unitOfWork.Object);
            var response = await profileHandler.Handle(command, new CancellationToken());

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal("Diego Domingos", ((ProfileResponse) response.Content).Name);
        }

        [Fact]
        public async void ProfileHandler_UpdatePerfil_Success()
        {
            Setup();

            _userRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_user()));

            _userRepository
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<User>()));

            _profileRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_profile()));

            var fileMock = new Mock<IFormFile>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ProfileMapper()));
            IMapper mapper = new Mapper(configuration);

            var command =
                new ProfileUpdateCommand(_profile().Id, "Diego", "Domingos", fileMock.Object, "teste@gmail.com")
                    {UserId = _user().Id};
            var profileHandler = new ProfileHandler(_profileRepository.Object, _userRepository.Object,
                _uploadService.Object, mapper, _unitOfWork.Object);
            var response = await profileHandler.Handle(command, new CancellationToken());

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal("Diego Domingos", ((ProfileResponse) response.Content).Name);
        }
    }
}