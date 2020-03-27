using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services.ProfileManagement;

namespace SocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileManagementService profileManagementService;

        private readonly IWebHostEnvironment env;

        public ProfileController(
            IProfileManagementService profileManagementService,
            IWebHostEnvironment env)
        {
            this.profileManagementService = profileManagementService;
            this.env = env;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(List<IFormFile> files)
        {
            if (files[0].ContentType == "")
            {
                return this.Redirect("/");
            }

            using (var stream = new MemoryStream())
            {
                await files[0].CopyToAsync(stream);

                stream.Seek(0, SeekOrigin.Begin);

                var imageId = await this.profileManagementService.UpdateProfilePictureOfUserById(
                    this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                await this.SavePhotoToLocalSystemAsync(imageId, stream.ToArray());
            }

            return this.Redirect("/");
        }

        private async Task SavePhotoToLocalSystemAsync(string fileId, byte[] photoContent)
        {
            var directory = this.env.WebRootPath;
            await System.IO.File.WriteAllBytesAsync(directory + @"/postsData/" + $"{fileId}.jpg", photoContent);
        }
    }
}
