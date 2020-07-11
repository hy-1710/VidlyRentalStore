using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter User Name")]
        [StringLength(255)]

        public string Name { get; set; }

        [Required(ErrorMessage = "Enter User Status")]

        public bool status { get; set; }
    }
}