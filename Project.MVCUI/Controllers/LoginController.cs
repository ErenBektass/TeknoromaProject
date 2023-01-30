using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class LoginController : Controller
    {

        AppUserRepository _apRep;

        public LoginController()
        {
            _apRep=new AppUserRepository();
        }


        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser user = _apRep.FirstOfDefault(x => x.UserName == appUser.UserName);
            if (user==null)
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alınmıs";
                return View();
            }

            string decrypted=Crypt.Encrypt(appUser.Password);
            
            if (user.Password==decrypted&&user.Role==ENTITIES.Enums.UserRole.Admin)
            {
                Session["admin"] = user;
                return RedirectToAction("AppUserList", "AppUser", new { Area = "Administration" });
            }
            else if(user.Password==decrypted&&user.Role==ENTITIES.Enums.UserRole.BranchManager)
            {
                Session["manager"]=user;
                return RedirectToAction("AppUserList", "AppUser", new { Area = "Administration" });
            }
            else if (user.Password == decrypted && user.Role ==ENTITIES.Enums.UserRole.SalesRepresentative)
            {
                Session["sales"] = user;
                return RedirectToAction("ProductList", "Product", new { Area = "Administration" });
            }
            else if (user.Password == decrypted && user.Role ==ENTITIES.Enums.UserRole.AccountingRepresentative)
            {
                Session["account"] = user;
                return RedirectToAction("ExpenseList", "Expense", new { Area = "Administration" });
            }
            else if (user.Password == decrypted && user.Role ==ENTITIES.Enums.UserRole.WarehouseRepresentative)
            {
                Session["ware"] = user;
                return RedirectToAction("ProductList", "Product", new { Area = "Administration" });

            }
            else if (user.Password == decrypted && user.Role ==ENTITIES.Enums.UserRole.TechnicalServiceRepresentative)
            {
                Session["technical"] = user;
                return RedirectToAction("IssueList", "Issue", new { Area = "Administration" });
            }
            else if (user.Password == decrypted && user.Role ==ENTITIES.Enums.UserRole.MobileSalesRepresentative)
            {
                Session["mobileSales"] = user;
                return RedirectToAction("ProductList","Product", new { Area = "Administration" });

            }
            else if (user.Password == decrypted && user.Role ==ENTITIES.Enums.UserRole.Member)
            {
                if (!user.Active)
                {
                    return AktifKontrol();
                }

                Session["member"] = user;
                return RedirectToAction("ShoppingList", "Shopping");
            }
            ViewBag.ZatenVar = "Kullanıcı yok";
            return View();
         
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");

        }
        public ActionResult AktifKontrol()
        {
            ViewBag.AktifDegil = "Hesabiniz aktif degil";
            return View("Login");
        }

    }
}