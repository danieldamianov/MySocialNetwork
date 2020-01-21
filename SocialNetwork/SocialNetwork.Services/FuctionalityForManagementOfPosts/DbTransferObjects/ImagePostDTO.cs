using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Services.FuctionalityForManagementOfPosts.DbTransferObjects
{
    public class ImagePostDTO
    {
        public string Username { get; set; }
        public ImagePostDTO(string description, byte[] photo,string username)
        {
            this.Description = description;
            this.Photo = photo;
            this.Username = username;
        }

        public string Description { get; set; }

        public byte[] Photo { get; set; }
    }
}
