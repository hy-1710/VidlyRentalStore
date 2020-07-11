using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime BookingStart
        {
            get; set;
        }

        [Required]
        public DateTime BookingEnd { get; set; }
    }
}