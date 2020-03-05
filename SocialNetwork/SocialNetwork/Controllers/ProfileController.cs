using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Services.ProfileManagement;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        private readonly ProfileManagementService profileManagementService;
        public ProfileController(ProfileManagementService profileManagementService)
        {
            this.profileManagementService = profileManagementService;
        }

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

                await this.profileManagementService.UpdateProfilePictureOfUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    , stream.ToArray());
            }


            return this.Redirect("/");
        }
    }
}
