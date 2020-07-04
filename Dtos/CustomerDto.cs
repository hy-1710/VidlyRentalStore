using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyStore.Models;

namespace VidlyStore.Dtos
{
    public class CustomerDto
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Customer Name")]
        [StringLength(255)]
        public string Name { get; set; }


        //[Min18YearsIfAMember]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }

        public bool IsSubscribeToNewsLetter { get; set; }

       
        [Required]
        public byte MemberShipTypeId { get; set; }
        public MemberShipTypeDto memberShipTypeDto { get; set; }
    }
}