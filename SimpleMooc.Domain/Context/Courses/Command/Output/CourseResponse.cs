using System;

namespace SimpleMooc.Domain.Context.Courses.Command.Output
{
    public class CourseResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public string Url { get; private set; }
        public DateTime Start { get; private set; }

        public CourseResponse()
        {
        }
    }
}