using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookVertex.Business.Services.IServices
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
