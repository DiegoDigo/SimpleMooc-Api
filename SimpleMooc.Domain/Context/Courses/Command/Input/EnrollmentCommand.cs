using System;
using MediatR;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Command.Input
{
    public record EnrollmentCommand(Guid CourseId) : IRequest<BaseResponse>
    {
        public Guid UserId { get; set; }
    }
}