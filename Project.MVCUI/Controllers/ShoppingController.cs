using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository _oRep;
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDetailRepository _odRep;

        public ShoppingController()
        {
            _odRep = new OrderDetailRepository();
            _oRep = new OrderRepository();
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();
        }
        public ActionResult ShoppingList(int? page,int? categoryID)
        {
            PaginationVM pavm = new PaginationVM
            {
               PagedProducts=categoryID==null ? _pRep.GetActives().ToPagedList(page ?? 1,9):_pRep.Where(x=>x.CategoryID==categoryID).ToPagedList(page ?? 1,9),
               Categories=_cRep.GetActives()
            };
            if (categoryID != null) TempData["catID"] = categoryID;
            return View(pavm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            Product eklenecekUrun = _pRep.Find(id);

            CartItem ci = new CartItem
            {
                ID=eklenecekUrun.ID,
                Name=eklenecekUrun.ProductName,
                Price=eklenecekUrun.UnitPrice,
                ImagePath=eklenecekUrun.ImagePath
            };
            c.SepetEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");
        }
        public ActionResult CartPage()
        {
            if (Session["scart"]!=null)
            {
                CartPageVM cpvm = new CartPageVM();
                Cart c = Session["scart"] as Cart;
                cpvm.Cart = c;
                return View(cpvm);
            }
            TempData["bos"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("Shopping List");
        }
        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"]!=null)
            {
                Cart c = Session["scart"] as Cart;
                c.SepettenCikar(id);
                if (c.Sepetim.Count==0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizdeki tüm ürünler cıkartılmıstır";
                    return RedirectToAction("Shopping List");
                }
                return RedirectToAction("Cartpage");
            }
            return RedirectToAction("ShoppingList");
        }
        public ActionResult ConfirmOrder()
        {
            AppUser currentUser;
            if (Session["member"] != null)
            {
                currentUser = Session["member"] as AppUser;

            }
            else TempData["anonim"] = "kullanıcı üye degil";
            return View();
        }
       
        



        
    }
}