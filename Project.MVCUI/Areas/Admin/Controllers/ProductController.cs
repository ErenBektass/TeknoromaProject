﻿using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Tools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        ProductRepository _pRep;
        CategoryRepository _cRep;

        public ProductController()
        {
            _pRep=new ProductRepository();
            _cRep=new CategoryRepository();
        }

        
        public ActionResult ProductList(int? id)
        {
            ProductVM pvm = id == null ? new ProductVM
            {
                Products= _pRep.GetAll().Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).ToList()
            } : new ProductVM { Categories = _cRep.Where(x => x.ID == id) };
            return View(pvm);

        }
        // GET: Admin/ProductDetails
        public ActionResult ProductDetails(int id)
        {
            ProductVM pvm = new ProductVM { Product = _pRep.Where(x => x.ID == id).FirstOrDefault() };
            return View(pvm);
        }
        public ActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM()
            {
                Categories = _cRep.GetActives()
            };
            return View(pvm);
        }
        [HttpPost]
        public ActionResult AddProduct(Product product,HttpPostedFileBase image,string filename)
        {
            if (image != null && filename != null)
            {
                product.ImagePath = ImageUploader.UploadImage("/Pictures/", image, filename);
            }
            _pRep.Add(product);
            return RedirectToAction("ProductList");
        }
        public ActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _pRep.Find(id),
                Categories = _cRep.GetActives()
            };
            return View(pvm);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product product, HttpPostedFileBase image)
        {
            if (image != null)
            {
                product.ImagePath = ImageUploader.UploadImage("/Pictures/", image, product.ImagePath);
            }
            _pRep.Update(product);
            return RedirectToAction("ProductList");
        }
        public ActionResult DeleteProduct(int id)
        {
            _pRep.Delete(_pRep.Find(id));
            return RedirectToAction("ProductList");

        }
    }
}