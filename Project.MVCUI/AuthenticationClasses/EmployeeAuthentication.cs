﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.AuthenticationClasses
{
    public class EmployeeAuthentication :AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["manager"]!=null & httpContext.Session["sale"] !=null& httpContext.Session["tech"]!=null& httpContext.Session["mobile"]!=null& httpContext.Session["acc"]!=null & httpContext.Session["ware"]!=null)
            {
                return true;
            }
            httpContext.Response.Redirect("/Home/Login");
            return false;
        }
    }
}