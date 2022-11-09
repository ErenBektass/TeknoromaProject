using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRepository _apRep;
        AppUserProfileRepository _apUserProfile;

        public RegisterController()
        {
            _apRep = new AppUserRepository();
            _apUserProfile = new AppUserProfileRepository();
        }
        public ActionResult RegisterNow()
        {
            return View();
        }
        
        

    }
}