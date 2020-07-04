using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Customer Name")]
        [StringLength(255)]
        public string Name { get; set; }

        
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }
            
        public bool IsSubscribeToNewsLetter { get; set; }

        public MemberShipType MemberShipType { get; set; }

        [Required]
        [Display(Name = "Membership Type")]
        public byte MemberShipTypeId { get; set; }


    }
}