﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Home
{
    public class NewsFeedHomeIndexViewModel
    {
        public NewsFeedHomeIndexViewModel()
        {
            this.Posts = new List<PostHomeIndexViewModel>();
        }

        public List<PostHomeIndexViewModel> Posts;
    }
}
