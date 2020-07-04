using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cus = (Customer)validationContext.ObjectInstance;

            if (cus.MemberShipTypeId == MemberShipType.Unknown || cus.MemberShipTypeId == MemberShipType.PayasYouGo)
                return ValidationResult.Success;

            if (cus.DOB == null)
                return new ValidationResult("Date of Birth is Required!");

            var age = DateTime.Today.Year - cus.DOB.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 Years Old.");
        }
    }
}