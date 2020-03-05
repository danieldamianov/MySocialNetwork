using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Controllers.ImageConvertingFunctionality;
using SocialNetwork.Services.ProfileManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers.Extensions
{
    public class ControllerAdditionalFunctionality
    {
        private readonly ProfileManagementService profileManagementService;

        private readonly ImageConverter imageConverter;


        public ControllerAdditionalFunctionality(ProfileManagementService profileManagementService, ImageConverter imageConverter)
        {
            this.profileManagementService = profileManagementService;
            this.imageConverter = imageConverter;
        }

        public string GetProfilePicture(string userId)
        {
            byte[] photoByteArray = this.profileManagementService.GetUserProfileLinkById(userId).Photo;
            string photo = string.Empty;
            if (photoByteArray != null)
            {
                photo = this.imageConverter.ConvertByteArrayToString(photoByteArray);
            }
            else
            {
                photo = this.imageConverter.ConvertByteArrayToString(System.IO.File.ReadAllBytes("wwwroot/pics/user_def_pic.png"));
            }

            return photo;
        }
    }
}
