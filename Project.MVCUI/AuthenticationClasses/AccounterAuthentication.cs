﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.AuthenticationClasses
{
    public class AccounterAuthentication: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["accounter"]!=null)
            {
                return true;
            }
            httpContext.Response.Redirect("/HomeLogin");
            return false;
        }
    }
}