using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialNetwork.Services.FollowingManagement.DTOs;
using SocialNetwork.DatabaseModels;

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
