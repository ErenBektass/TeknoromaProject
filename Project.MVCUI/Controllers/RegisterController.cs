using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
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
        [HttpPost]
        public ActionResult RegisterNow(AppUserVM apvm)
        {
            AppUser user = apvm.AppUser;
            AppUserProfile profile = apvm.Profile;

            user.Password = Crypt.Encrypt(user.Password);//sifreyi kriptoladık
            user.ConfirmPassword = Crypt.Encrypt(user.ConfirmPassword);

            if (_apRep.Any(x=>x.UserName==user.UserName))
            {
                ViewBag.ZatenVar ="Kullanıcı ismi daha önce alınmıs";
                return View();
            }
            else if (_apRep.Any(x=>x.Email==user.Email))
            {
                ViewBag.ZatenVar= "Email zaten kayıtlı";
                return View();
            }
            //Kullanıcı basarılı bir şekilde register işlemini tamamladıysa ona mail gönder...
            string gonderilecekEmail = "Tebrikler...Hesabınız olusturulmustur...Hesabınızı aktive etmek https://localhost:44350/Register/Activation/" + user.ActivationCode + "linkine tıklayabilirsiniz";
            MailService.Send(user.Email,body: gonderilecekEmail,subject: "Hesap AKtivasyon!!!");
            _apRep.Add(user);
            if (!string.IsNullOrEmpty(profile.FirstName)& ! string.IsNullOrEmpty(profile.LastName)&!string.IsNullOrEmpty(profile.Address)&!string.IsNullOrEmpty(profile.TCNO))
            {
                profile.ID = user.ID;
                _apUserProfile.Add(profile);
            }
            return View("RegisterOK");

            
        }
        public ActionResult Activation(Guid id)
        {
            AppUser aktifEdilecek = _apRep.FirstOfDefault(x => x.ActivationCode == id);
            if (aktifEdilecek!=null)
            {
                aktifEdilecek.Active = true;
                _apRep.Update(aktifEdilecek);
                TempData["HesapAktif"]= "Hesabınız aktif hale getirildi";
                return RedirectToAction("Login", "Home");
            }
            //Supheli bir aktivite
            TempData["HesapAktifMi"]= "Hesabınız bulunamadı";
            return RedirectToAction("Login", "Home");
        }
        public ActionResult RegisterOK()
        {
            return View();
        }



        

    }
}