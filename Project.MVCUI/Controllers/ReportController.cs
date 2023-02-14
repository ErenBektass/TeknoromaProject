using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ReportController : Controller
    {
        // GET: Repot
        public ActionResult Index()
        {
              
            ReportVM rvm = new ReportVM
            {
                
            };

            return View();
        }
    }
}