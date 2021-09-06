using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YMG.Models
{
    public class DiscussionForum
    {
        [Key]
        public int DiscussionForumId { get; set; }
        [Required]
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}