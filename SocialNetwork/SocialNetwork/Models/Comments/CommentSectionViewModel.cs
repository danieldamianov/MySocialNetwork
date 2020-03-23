using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Comments
{
    public class CommentSectionViewModel
    {
        public CommentSectionViewModel(string htmlGeneratedForComments, int commentsCount)
        {
            this.HtmlGeneratedForComments = htmlGeneratedForComments;
            this.CommentsCount = commentsCount;
        }

        public string HtmlGeneratedForComments { get; set; }

        public int CommentsCount { get; set; }
    }
}
