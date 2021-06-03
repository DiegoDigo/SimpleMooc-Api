using System;
using System.Collections.Generic;
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
using SimpleMooc.Domain.Context.Users.Command.Output;
using SimpleMooc.Shared.Repositories;
using SimpleMooc.Shared.Services;
using Xunit;

namespace SimpleMooc.Test.Domain.Handler
{
    public class LessonHandlerTest
    {
        private Mock<ILessonRepository> _lessonRepository;
        private Mock<ICourseRepository> _courseRepository;
        private Mock<IUploadService> _uploadService;
        private Mock<IUnitOfWork> _iUnitOfWork;
        private Mock<IFormFile> _fileMock;


        private void Setup()
        {
            _lessonRepository = new Mock<ILessonRepository>();
            _iUnitOfWork = new Mock<IUnitOfWork>();
            _courseRepository = new Mock<ICourseRepository>();
            _uploadService = new Mock<IUploadService>();
            _fileMock = new Mock<IFormFile>();
        }

        private Course _course()
        {
            return new("Teste unitario xunit", "teste com xunit", It.IsAny<string>());
        }

        [Fact]
        public async void LessonHandler_CriandoAula_Success()
        {
            Setup();
            
            _courseRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_course()));

            _lessonRepository
                .Setup(x => x.GetAllByCourse(It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<IEnumerable<Lesson>>()));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new LessonMapper()));
            IMapper mapper = new Mapper(configuration);

            var command =
                new LessonCommand("Xunit", "O que é Fact", Guid.NewGuid(), _fileMock.Object);

            var lessonHandler = new LessonHandler(_lessonRepository.Object, _courseRepository.Object,
                _iUnitOfWork.Object, mapper, _uploadService.Object);
            var response = await lessonHandler.Handle(command, new CancellationToken());

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal("Xunit", ((CourseLessonResponse) response.Content).Name);
            Assert.Equal("O que é Fact", ((CourseLessonResponse) response.Content).Description);
        }
    }
}