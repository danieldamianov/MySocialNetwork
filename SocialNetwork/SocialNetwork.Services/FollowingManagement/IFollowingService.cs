using System.Collections.Generic;
using SocialNetwork.Services.FollowingManagement.DTOs;

namespace SocialNetwork.Services.FollowingManagement
{
    public interface IFollowingService
    {
        List<string> GetUsersIdsWhichGivenUserFollows(string userId);

        List<UserWithFollowersAndFollowingDTO> GetUserByFirstLetters(string firstLetters);

        UserWithFollowersAndFollowingDTO GetUserById(string id);

        bool AddFollowingRelationShip(string followerId, string followedId);
    }
}
