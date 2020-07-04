using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Models
{
    public class Rental
    {
        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Movie Movie { get; set; }
        [Required]
        public DateTime DateRated { get; set; }
        public DateTime? DateReruned { get; set; }
    }
}