﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCRUDApplication.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}