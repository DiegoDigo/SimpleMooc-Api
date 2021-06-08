using System;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Util;

namespace SimpleMooc.Domain.Context.Courses.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public int Stars { get; private set; }
        public string UrlImage { get; private set; }

        public Course()
        {
        }

        public Course(string name, string description, string urlImage, int stars = 0)
        {
            Name = name;
            Description = description;
            Slug = StringUtil.GenerateSlug(name);
            Stars = stars;
            UrlImage = urlImage;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void ChangeUrlImage(string url)
        {
            UrlImage = url;
        }

        public void ChangeStars(int numStart)
        {
            Stars = numStart;
        }
    }
}