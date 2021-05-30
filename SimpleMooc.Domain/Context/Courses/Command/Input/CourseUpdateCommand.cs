using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Command.Input
{
    public record CourseUpdateCommand
        (Guid Id, string Name, string Description, IFormFile Image) : IRequest<BaseResponse>;
}