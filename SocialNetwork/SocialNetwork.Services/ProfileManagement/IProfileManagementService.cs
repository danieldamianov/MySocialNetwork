using System.Threading.Tasks;

using SocialNetwork.Services.ProfileManagement.DTOs;

namespace SocialNetwork.Services.ProfileManagement
{
    public interface IProfileManagementService
    {
        Task<string> UpdateProfilePictureOfUserById(string userId);

        ProfileLinkDTO GetUserProfileLinkById(string userId);
    }
}
