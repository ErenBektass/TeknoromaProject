﻿using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class ExpenseVM
    {
        public Expense Expense { get; set; }
        public List<Expense>  Expenses { get; set; }
    }
}