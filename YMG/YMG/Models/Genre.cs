using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YMG.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required, RegularExpression(@"^[a-zA-Z\s\-]*$", ErrorMessage = "The genre name should only contain letters, spaces or the '-' character."), MinLength(3, ErrorMessage = "Genre name must be at least 3 characters"), MaxLength(25, ErrorMessage ="Genre name must be less than 26 characters")]
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> MoviesList { get; set; }
    }
}