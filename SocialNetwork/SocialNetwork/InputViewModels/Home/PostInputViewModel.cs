using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.InputViewModels.Home
{
    public class PostInputViewModel
    {
        [Required]
        [Display(Name = "Photos")]
        public List<IFormFile> Photos { get; set; }

        [Required]
        [Display(Name = "Videos")]
        public List<IFormFile> Videos { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }
    }
}
