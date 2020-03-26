using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public abstract class PostContent
    {
        public string Id { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
