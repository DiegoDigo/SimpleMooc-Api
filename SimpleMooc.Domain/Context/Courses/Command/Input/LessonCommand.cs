using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Command.Input
{
    public record LessonCommand(
        string Name,
        string Description,
        Guid CourseId,
        IFormFile Material
    ) : IRequest<BaseResponse>;
}