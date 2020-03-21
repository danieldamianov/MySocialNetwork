﻿using SocialNetwork.Controllers.ImageConvertingFunctionality;
using SocialNetwork.Services.ProfileManagement;

namespace SocialNetwork.Controllers.Extensions
{
    public class ControllerAdditionalFunctionality : IControllerAdditionalFunctionality
    {
        private readonly IProfileManagementService profileManagementService;

        private readonly ImageConverter imageConverter;


        public ControllerAdditionalFunctionality(IProfileManagementService profileManagementService, ImageConverter imageConverter)
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
