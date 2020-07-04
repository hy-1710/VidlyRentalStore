using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre genre { get; set; }

        [Display(Name ="Added Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Display(Name="Release Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleasedDate { get; set; }

        [Required]
        [Display(Name ="No in Stock")]
        [Range(1,20)]
        public byte NoinStock { get; set; }

        public Genre Genre { get; set; }


        [Display (Name="Genre")]
        [Required]
        public int GenreId { get; set; }

        public byte NoofAvailableStock { get; set; }

       
    }
}