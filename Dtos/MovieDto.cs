using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ReleasedDate { get; set; }

        [Required]
        [Range(1, 20)]
        public byte NoinStock { get; set; }
       
        [Required]
        public int GenreId { get; set; }
        public GenreDto genreDto { get; set; }
    }
}