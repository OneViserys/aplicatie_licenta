using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YMG.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required, MinLength(5, ErrorMessage = "Title should have at least 5 characters."), MaxLength(15, ErrorMessage = "Title should have up to 15 characters.")]
        public string Title { get; set; }
        [Required, RegularExpression(@"^10|[1-9]$", ErrorMessage = "Rating should be an integer value between 1 and 10.")]
        public string Rating { get; set; }
        [Required, MinLength(10, ErrorMessage = "The review should have at least 10 characters."), MaxLength(300, ErrorMessage = "The review should have up to 300 characters")]
        public string Text { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        [Display(Name ="Does the review include spoilers?")]
        public bool SpoilersAhead { get; set; }
        public int NumberOfReports { get; set; }
        public virtual ICollection<ReportReview> Reports { get; set; }
    }
}