using System;

namespace SimpleMooc.Domain.Context.Courses.Command.Output
{
    public class LessonMaterialResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}