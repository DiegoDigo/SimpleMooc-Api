using System;

namespace SimpleMooc.Domain.Context.Courses.Command.Output
{
    public class CourseLessonResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Number { get; private set; }
        public string Url { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        public CourseLessonResponse()
        {
        }

        public CourseLessonResponse(Guid id, string name, string description, int number, DateTime releaseDate, string url)
        {
            Id = id;
            Name = name;
            Description = description;
            Number = number;
            ReleaseDate = releaseDate;
            Url = url;
        }
    }
}