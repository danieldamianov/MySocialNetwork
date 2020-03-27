using SocialNetwork.Services.ProfileManagement;

namespace SocialNetwork.Controllers.Extensions
{
    public class ControllerAdditionalFunctionality : IControllerAdditionalFunctionality
    {
        private readonly IProfileManagementService profileManagementService;

        public ControllerAdditionalFunctionality(IProfileManagementService profileManagementService)
        {
            this.profileManagementService = profileManagementService;
        }

        public string GetProfilePicture(string userId)
        {
            string photoByteArray = this.profileManagementService.GetUserProfileLinkById(userId).ProfilePictureId;
            //TODO: ref
            string photo = string.Empty;
            if (photoByteArray != null)
            {
                //TODO: Refavtor photo = this.imageConverter.ConvertByteArrayToString(photoByteArray);
            }
            else
            {
                //TODO: Refavtor photo = this.imageConverter.ConvertByteArrayToString(System.IO.File.ReadAllBytes("wwwroot/pics/user_def_pic.png"));
            }

            return photo;
        }
    }
}
