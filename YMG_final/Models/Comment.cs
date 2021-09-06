using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YMG.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required, MinLength(1, ErrorMessage = "The comment must have at least 5 characters."), MaxLength(500, ErrorMessage = "Please keep comments under 500 characters.")]
        public string Text { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public string CreatedAt { get; set; }
        public string EditedAt { get; set; }
        public virtual DiscussionForum Forum { get; set; }
        public int NumberOfReports { get; set; }
    }
}