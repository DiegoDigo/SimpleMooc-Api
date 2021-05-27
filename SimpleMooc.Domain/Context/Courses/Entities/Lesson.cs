using System;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Entities
{
    public class Lesson : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Number { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public Course Course { get; private set; }

        public Lesson(string name, string description, int number,  Course course)
        {
            Name = name;
            Description = description;
            Number = number;
            ReleaseDate = DateTime.Now.AddDays(15);
            Course = course;
        }

        public Lesson()
        {
          
        }
    }
}