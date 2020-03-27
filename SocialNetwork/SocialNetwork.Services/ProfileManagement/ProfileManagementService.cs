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
        private readonly SocialNetworkDbContext socialNetworkDbContext;

        public ProfileManagementService(SocialNetworkDbContext socialNetworkDbContext)
        {
            this.socialNetworkDbContext = socialNetworkDbContext;
        }

        public async Task<string> UpdateProfilePictureOfUserById(string userId)
        {
            var user = await this.socialNetworkDbContext.Users.FindAsync(userId);
            user.ProfilePictureId = Guid.NewGuid().ToString();
            await this.socialNetworkDbContext.SaveChangesAsync();
            return user.ProfilePictureId;
        }

        public ProfileLinkDTO GetUserProfileLinkById(string userId)
        {
            var user = this.socialNetworkDbContext.Users.Find(userId);

            return new ProfileLinkDTO()
            {
                Name = user.UserName,
                ProfilePictureId = user.ProfilePictureId,
            };
        }
    }
}
