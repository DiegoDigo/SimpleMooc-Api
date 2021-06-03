using System;
using SimpleMooc.Domain.Context.Courses.Entities.Enums;

namespace SimpleMooc.Domain.Context.Courses.Command.Output
{
    public class EnrollmentResponse
    {
        public Guid Id { get; private set; }
        public EStatus Status { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
    }
}