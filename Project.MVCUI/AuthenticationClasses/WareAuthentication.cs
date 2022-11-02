using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.AuthenticationClasses
{
    public class WareAuthentication:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["ware"]!=null & httpContext.Session["sale"]!=null & httpContext.Session["manager"]!=null)
            {
                return true;
            }
            httpContext.Response.Redirect("/Home/Login");
            return false;
        }
    }
}