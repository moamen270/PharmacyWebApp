﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.Models.consts
{
    public static class OrderState
    {
        public const string Processing = "Processing";
        public const string Rejected = "Rejected";
        public const string Pending = "Pending";
        public const string Completed = "Completed";
    }

}