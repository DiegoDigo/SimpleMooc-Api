using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMooc.Shared.Services
{
    public interface IUploadService
    {
        Task<string> UploadImageProfile(Guid userId, Stream image);
        Task<string> UploadImageCourse(string slug, Stream image);
        Task<string> UploadMaterial(string name, Stream video, CancellationToken cancellationToken);
    }
}