using SocialNetwork.Data;
using SocialNetwork.Services.ProfileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services.ProfileManagement
{
    public class ProfileManagementService : IProfileManagementService
    {
        public ProfileManagementService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }

        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public async Task<bool> UpdateProfilePictureOfUserById(string userId, byte[] photo)
        {
            var user = await this.socialNetworkDbContext.Users.FindAsync(userId);
            user.Photo = photo;
            await this.socialNetworkDbContext.SaveChangesAsync();
            return true;
        }

        public ProfileLinkDTO GetUserProfileLinkById(string userId)
        {
            var user = this.socialNetworkDbContext.Users.Find(userId);

            return new ProfileLinkDTO()
            {
                Name = user.UserName,
                Photo = user.Photo,
            };
        }
    }
}
