using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YMG.Models
{
    public class UserViewModel
    {
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}