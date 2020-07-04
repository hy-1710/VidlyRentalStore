﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyStore.Dtos
{
    public class MemberShipTypeDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationinMonth { get; set; }
        public byte DiscountRate { get; set; }
    }
}