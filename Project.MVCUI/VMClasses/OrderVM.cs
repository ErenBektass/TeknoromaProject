﻿using Project.ENTITIES.Models;
using Project.MVCUI.ConsumerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class OrderVM
    {
        public Order  Order { get; set; }
        public List<Order>  Orders { get; set; }
        public List<AppUser>  AppUsers { get; set; }
        public List<Employee>  Employees { get; set; }
        public List<Shipper>  Shippers { get; set; }
        public PaymentDTO  PaymentDTO { get; set; }

    }
}