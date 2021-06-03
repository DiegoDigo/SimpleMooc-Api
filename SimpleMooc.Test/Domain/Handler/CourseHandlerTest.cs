using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Context.Courses.Command.Output;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Handlers;
using SimpleMooc.Domain.Context.Courses.Mapper;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Shared.Repositories;
using SimpleMooc.Shared.Services;
using Xunit;

namespace SimpleMooc.Test.Domain.Handler
{
    public class CourseHandlerTest
    {
        private Mock<IUploadService> _uploadService;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ICourseRepository> _courseRepository;

        private void Setup()
        {
            _uploadService = new Mock<IUploadService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _courseRepository = new Mock<ICourseRepository>();
        }

        private static Course _course()
        {
            return new(".net core", "Curso de .net core", It.IsAny<string>());
        }

        [Fact]
        public async void CourseHandler_CriarCurso_Success()
        {
            Setup();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CourseMapper()));
            IMapper mapper = new Mapper(configuration);

            var fileMock = new Mock<IFormFile>();

            var command = new CourseCommand(".net core", "Curso de .net core", fileMock.Object);


            var courseHandler = new CourseHandler(
                _uploadService.Object,
                _unitOfWork.Object,
                _courseRepository.Object,
                mapper
            );
            var response = await courseHandler.Handle(command, new CancellationToken());

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(".net core", ((CourseResponse) response.Content).Name);
            Assert.Equal("Curso de .net core", ((CourseResponse) response.Content).Description);
        }

        [Fact]
        public async void CourseHandler_UpdateCurso_Success()
        {
            Setup();

            _courseRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_course()));


            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CourseMapper()));
            IMapper mapper = new Mapper(configuration);

            var fileMock = new Mock<IFormFile>();

            var command = new CourseUpdateCommand(_course().Id, ".net core update", "Curso de .net core update", fileMock.Object);


            var courseHandler = new CourseHandler(
                _uploadService.Object,
                _unitOfWork.Object,
                _courseRepository.Object,
                mapper
            );
            var response = await courseHandler.Handle(command, new CancellationToken());

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(".net core update", ((CourseResponse) response.Content).Name);
            Assert.Equal("Curso de .net core update", ((CourseResponse) response.Content).Description);
        }
    }
}