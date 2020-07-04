using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyStore.Models;

namespace VidlyStore.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }
       
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ReleasedDate { get; set; }

        [Required]
        [Display(Name = "No in Stock")]
        [Range(1, 20)]
        public byte? NoinStock { get; set; }



        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
                
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            NoinStock = movie.NoinStock;
            GenreId = movie.GenreId;
            ReleasedDate = movie.ReleasedDate;
        }
    }
}