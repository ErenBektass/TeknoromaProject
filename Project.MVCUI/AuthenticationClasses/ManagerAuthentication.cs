using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.AuthenticationClasses
{
    public class ManagerAuthentication : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["manager"]!=null & httpContext.Session["ware"]!=null & httpContext.Session["accounter"]!=null & httpContext.Session["sale"]!=null)
            {
                return true;
            }
            httpContext.Response.Redirect("/Login/Login");
            return false;
        }
    }

	
    
}