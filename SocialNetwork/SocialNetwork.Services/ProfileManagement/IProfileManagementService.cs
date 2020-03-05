using SocialNetwork.Data;
using SocialNetwork.Services.ProfileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services.ProfileManagement
{
    public interface IProfileManagementService
    {
        Task<bool> UpdateProfilePictureOfUserById(string userId, byte[] photo);

        ProfileLinkDTO GetUserProfileLinkById(string userId);
    }
}
