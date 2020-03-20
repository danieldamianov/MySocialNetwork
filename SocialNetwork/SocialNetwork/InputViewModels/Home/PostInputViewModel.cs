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
        [Display(Name = "Photo")]
        public List<IFormFile> Files { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }
    }
}
