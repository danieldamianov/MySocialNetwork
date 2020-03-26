using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.DatabaseModels
{
    public class VideoContent : PostContent
    {
        public VideoContent(string postId) 
            : base(postId)
        {
        }
    }
}
