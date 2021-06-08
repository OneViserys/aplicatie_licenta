using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YMG.Models.MyValidation;

namespace YMG.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        // pt nume de tip 'Martin Luther King, Jr.', "D'Artagnan", 'Helena Bonham-Carter'
        [Required, UniqueValidator, RegularExpression(@"^[a-zA-Z\s\,\.\'\-]*$", ErrorMessage ="The name should only contain letters, spaces or the characters: , . ' -"), MinLength(2, ErrorMessage = "Actor's name should be at least 2 characters long."), MaxLength(30, ErrorMessage = "Actor's name should be at most 30 characters long.")]
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public string CopyrightMessage { get; set; }
        [Required, MinLength(1, ErrorMessage = "Actor's bio should be at least 10 characters.")]
        public string Bio { get; set; }
        public virtual ICollection<Movie> Filmography { get; set; }
        [NotMapped]
        public List<CheckBoxViewModel> MoviesList { get; set; }
        public virtual List<Award> Awards { get; set; }
        public virtual List<ActorRole> Roles { get; set; }
    }
}