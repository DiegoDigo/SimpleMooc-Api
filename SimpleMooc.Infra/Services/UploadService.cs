using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using SimpleMooc.Shared.Config;
using SimpleMooc.Shared.Services;

namespace SimpleMooc.Infra.Services
{
    public class UploadService : IUploadService
    {
        private readonly CloudinaryConfig _cloudinaryConfig;

        public UploadService(CloudinaryConfig cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
        }

        public async Task<string> UploadImageProfile(Guid userId, Stream image)
            => await Upload(userId.ToString(), image, "avatar");

        public async Task<string> UploadImageCourse(string slug, Stream image)
            => await Upload(slug, image, "courses");

        private async Task<string> Upload(string identifier, Stream image, string path)
        {
            var imageParams = new ImageUploadParams()
            {
                File = new FileDescription(identifier, image),
                PublicId = $"simple-mooc/{path}/{identifier}",
                Overwrite = true,
                Transformation = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face")
            };
            var result = await Init().UploadAsync(imageParams);
            return result.SecureUrl.AbsoluteUri;
        }

        private Cloudinary Init()
        {
            var myAccount = new Account
            {
                ApiKey = _cloudinaryConfig.Key,
                ApiSecret = _cloudinaryConfig.Secret,
                Cloud = _cloudinaryConfig.Cloud
            };
            return new Cloudinary(myAccount);
        }
    }
}