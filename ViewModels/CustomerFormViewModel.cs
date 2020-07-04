using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyStore.Models;

namespace VidlyStore.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MemberShipType> MemberShipType { get; set; }
        public Customer Customer { get; set; }
    }
}