using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Driving License")]
        [StringLength(255)]
        public string DrivingLicense { get; set; }
        [Required]
        [Display(Name = "Mobile No")]
        [StringLength(10)]
        
        [Phone]
        [RegularExpression("/d{10}$/", ErrorMessage = "Please Enter Valid Phone No")]
        public string MobileNo { get; set; }

    }
}