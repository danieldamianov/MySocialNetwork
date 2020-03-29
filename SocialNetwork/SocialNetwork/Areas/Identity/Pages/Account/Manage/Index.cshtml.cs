using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services.ProfileManagement;

namespace SocialNetwork.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<SocialNetworkUser> _userManager;
        private readonly SignInManager<SocialNetworkUser> _signInManager;
        private readonly IProfileManagementService profileManagementService;
        private readonly IWebHostEnvironment env;

        public IndexModel(
            UserManager<SocialNetworkUser> userManager,
            SignInManager<SocialNetworkUser> signInManager,
            IProfileManagementService profileManagementService,
            IWebHostEnvironment env)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.profileManagementService = profileManagementService;
            this.env = env;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            public IFormFile ProfilePicture { get; set; }
        }

        private async Task LoadAsync(SocialNetworkUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (this.Input.ProfilePicture.ContentType == "")
            {
                return this.Page();
            }

            using (var stream = new MemoryStream())
            {
                await this.Input.ProfilePicture.CopyToAsync(stream);

                stream.Seek(0, SeekOrigin.Begin);

                var imageId = await this.profileManagementService.UpdateProfilePictureOfUserById(
                    this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                await this.SavePhotoToLocalSystemAsync(imageId, stream.ToArray());
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task SavePhotoToLocalSystemAsync(string fileId, byte[] photoContent)
        {
            var directory = this.env.WebRootPath;
            await System.IO.File.WriteAllBytesAsync(directory + @"/postsData/" + $"{fileId}.jpg", photoContent);
        }
    }
}
