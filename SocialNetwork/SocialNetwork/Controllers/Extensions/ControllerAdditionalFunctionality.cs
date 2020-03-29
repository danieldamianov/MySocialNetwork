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

        public string GetProfilePictureId(string userId)
        {
            string profilePictureId = this.profileManagementService.GetUserProfileLinkById(userId).ProfilePictureId;

            return profilePictureId;
        }
    }
}
