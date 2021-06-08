using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YMG.Models;

namespace YMG.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required, RegularExpression(@"^[a-zA-Z0-9\s\,\.\:\'\-]*$", ErrorMessage = "The movie title should only contain letters, numbers, spaces or the special characters: , . : ' -"), MinLength(2, ErrorMessage = "The Movie name should be at least 2 characters long."), MaxLength(70, ErrorMessage = "The Movie name should be at most 70 characters long.")]
        public string Title { get; set; }
        [MinLength(10, ErrorMessage = "The description should be at least 10 characters long"), MaxLength(999, ErrorMessage = "The description should be less than 1000 characters")]
        public string Description { get; set; }
        [Required, RegularExpression(@"^[a-zA-Z\s\,\.\'\-]*$", ErrorMessage = "The names should only contain letters, spaces or the characters: , . ' -"), MinLength(2, ErrorMessage = "Director's name should be at least 2 characters long."), MaxLength(30, ErrorMessage = "Director's name should be at most 30 characters long.")]
        public string Director { get; set; }
        public virtual ICollection<Actor> Cast { get; set; }
        [Required, RegularExpression(@"^(188[89])|(189[\d])|(19[\d][\d])|(20[01][\d])|(202[06])$", ErrorMessage = "The year must be between 1889 and 2026")]
        public int Year { get; set; }
        [Required]
        public bool Released { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual DiscussionForum Forum { get; set; }
        public string TrailerUrl { get; set; }
        public string PosterUrl { get; set; }
        public string CopyrightMessage { get; set; }
        public virtual List<ActorRole> Characters {get; set;}
        [NotMapped]
        public List<CheckBoxViewModel> GenresList { get; set; }
        [NotMapped]
        public List<CheckBoxViewModel> BooleanList { get; set; }
        [NotMapped]
        public List<CheckBoxViewModel> ActorsList { get; set; }
    }


}