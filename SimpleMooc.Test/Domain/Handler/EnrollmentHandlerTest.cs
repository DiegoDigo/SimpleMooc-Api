using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Handlers;
using SimpleMooc.Domain.Context.Courses.Mapper;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Shared.Repositories;
using Xunit;

namespace SimpleMooc.Test.Domain.Handler
{
    public class EnrollmentHandlerTest
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<ICourseRepository> _courseRepository;
        private Mock<IEnrollmentRepository> _enrollmentRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        private void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _courseRepository = new Mock<ICourseRepository>();
            _enrollmentRepository = new Mock<IEnrollmentRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        private static Course _course()
        {
            return new("XUnit", "teste xunit", It.IsAny<string>());
        }

        private static User _user()
        {
            var user = new User("teste@gmail.com");
            user.ChangePassword("12345678");
            return user;
        }


        [Fact]
        public async void EnrollmentHandler_CriarInscricao_Success()
        {
            Setup();

            _enrollmentRepository
                .Setup(x => x.GetByUserIdAndCourseId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult<Enrollment>(null));

            _userRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_user()));

            _courseRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_course()));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new EnrollmentsMapper()));
            IMapper mapper = new Mapper(configuration);

            var command = new EnrollmentCommand(_course().Id);

            var lessonHandler = new EnrollmentHandler(
                _userRepository.Object,
                _courseRepository.Object,
                _enrollmentRepository.Object,
                _unitOfWork.Object,
                mapper
            );
            var response = await lessonHandler.Handle(command, new CancellationToken());

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal("XUnit", ((EnrollmentResponse) response.Content).Name);
            Assert.Equal(_course().Slug, ((EnrollmentResponse) response.Content).Slug);
        }
    }
}