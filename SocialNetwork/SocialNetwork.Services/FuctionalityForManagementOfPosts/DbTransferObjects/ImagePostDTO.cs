using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects
{
    public class ImagePostDTO
    {
        public ImagePostDTO(string description, byte[] photo)
        {
            Description = description;
            Photo = photo;
        }

        public string Description { get; set; }

        public byte[] Photo { get; set; }
    }
}
