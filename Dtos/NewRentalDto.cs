using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyStore.Dtos
{
    public class NewRentalDto
    {
        public int CustomerID { get; set; }
        public List<int> MovieIDs { get; set; }
    }
}