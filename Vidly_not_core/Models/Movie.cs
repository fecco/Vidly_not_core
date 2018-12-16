using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly_not_core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Display(Name = "Release Date ")]
        public DateTime ReleaseDate { get; set; }
        public DateTime? DateAdded { get; set; }
        [Display(Name = "Number in stock ")]
        public int Stock { get; set; }
        //navigation property mivel ez köti össze a movie-t a genre típussal
        public MovieGenre MovieGenre { get; set; }
        //az alábbi sort automatikusan külső kulcsként fogja felismerni:
        public byte MovieGenreId { get; set; }
    }
}