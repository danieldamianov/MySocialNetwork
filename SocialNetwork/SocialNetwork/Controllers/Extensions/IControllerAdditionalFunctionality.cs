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
    public interface IControllerAdditionalFunctionality
    {

        string GetProfilePicture(string userId);
    }
}
