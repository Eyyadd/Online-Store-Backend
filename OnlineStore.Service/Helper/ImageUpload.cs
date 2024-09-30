using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OnlineStore.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace OnlineStore.Application.Helper
{
    public class ImageUpload
    {
      
        public async static Task<string> UploadImageAsync(IFormFile Image)
        {
            var ImageName = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            var ImageRealPath = Path.Combine(FolderPath, ImageName);
            using (var stream = new FileStream(ImageRealPath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }
            var ImagePathToRestor = $"https://localhost:44322/Images/{ImageName}";
            return ImagePathToRestor;
        }
    }
}
