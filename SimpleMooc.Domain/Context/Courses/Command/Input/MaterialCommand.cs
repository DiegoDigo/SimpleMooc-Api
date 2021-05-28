using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Command.Input
{
    public record MaterialCommand(
        string Name,
        IFormFile Material,
        Guid LessonId,
        Guid CourseId
    ) : IRequest<BaseResponse>;
}