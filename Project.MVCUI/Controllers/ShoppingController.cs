using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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

        
        public ActionResult ShoppingList()
        {

            //if (Session["member"] == null)
            //{
            //    return RedirectToAction("../Login/Login");
            //}


            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            List<CartItem> cards = c.Sepetim;
            ShoppingVM spvm = new ShoppingVM
            {
                Cards = cards
            };

            return View(spvm);
        }

        public ActionResult ShoppingList_Eski(int? page,int? categoryID)
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
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("ShoppingList");
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

        //https://localhost:44350/api/Payment/ReceivePayment mail senden acinca buraya yapistiricaksin local kodunu
        [HttpPost]
        public ActionResult ConfirmOrder(OrderVM ovm)
        {
            bool result;
            Cart sepet = Session["scart"] as Cart;
            ovm.Order.TotalPrice = ovm.PaymentDTO.ShoppingPrice = sepet.TotalPrice;

            #region APISection

            using (HttpClient client=new HttpClient())
            {
                client.BaseAddress=new Uri("https://localhost:44366/api/");
                Task<HttpResponseMessage>   postTask=client.PostAsJsonAsync("Payment/ReceivePayment", ovm.PaymentDTO);
                HttpResponseMessage sonuc;


                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception)
                {

                    TempData["baglantiRed"]= "Banka baglantiyi reddetti";
                    return RedirectToAction("ResultOrder");
                }
                if (sonuc.IsSuccessStatusCode) result = true;
                else result = false;

                if (result)
                {
                    if (Session["member"]!=null)
                    {
                        AppUser user = Session["member"] as AppUser;
                        ovm.Order.AppUserID = user.ID;
                        ovm.Order.UserName = user.UserName;
                    }
                    else
                    {
                        ovm.Order.AppUserID = null;
                        ovm.Order.UserName = TempData["anonim"].ToString();
                    }
                    ovm.Order.OrderDate = DateTime.Now;
                    ovm.Order.CreatedDate = DateTime.Now;
                   _oRep.Add(ovm.Order); //OrderRepository bu noktada Order'i eklerken onun ID'sini olusturuyor

                    foreach (CartItem item in sepet.Sepetim)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = ovm.Order.ID;
                        od.ProductID = item.ID;
                        od.UnitPrice = item.Price;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odRep.Add(od);

                        //Stoktan da düsürelim
                        Product stokDus = _pRep.Find(item.ID);
                        stokDus.UnitsInStock -= item.Amount;
                        _pRep.Update(stokDus);
                    }
                    TempData["odemesonuc"]= "Siparişiniz  bize ulasmıstır...Tesekkür ederiz";
                    MailService.Send(ovm.Order.Email,body: $"Siparişiniz basarıyla alındı {ovm.Order.TotalPrice}");
                    Session.Remove("scart");
                    return RedirectToAction("ResultOrder");

                }
                else
                {
                    Task<string> s = sonuc.Content.ReadAsStringAsync();
                    TempData["sorun"] = s.Result;
                    return RedirectToAction("ResultOrder");
                }


            }



            #endregion
        }


        public ActionResult ResultOrder()
        {
            return View();
        }



    }
}