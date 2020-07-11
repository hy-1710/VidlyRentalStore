using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Dtos
{
    public class UserDto

    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Enter User Name")]
        [StringLength(255)]

        public string Name { get; set; }

        [Required(ErrorMessage = "Enter User Status")]


        public bool status { get; set; }
    }
}