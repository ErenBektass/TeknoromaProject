﻿using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {
        CategoryRepository _cRep;

        public CategoryController()
        {
            _cRep = new CategoryRepository();
        }

        // GET: Admin/Category
        public ActionResult CategoryList(int? id)
        {
            CategoryVM cvm = id == null ? new CategoryVM
            {
                Categories = _cRep.GetAll().Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).ToList()

            } : new CategoryVM { Categories = _cRep.Where(x => x.ID == id) };
            return View(cvm);
        }

        // GET: Admin/CategoryDetails
        public ActionResult CategoryDetails(int id)
        {
            CategoryVM categoryVM = new CategoryVM { Category = _cRep.Where(x => x.ID == id).FirstOrDefault() };
            return View(categoryVM);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _cRep.Add(category);
            return RedirectToAction("CategoryList");
        }
        public ActionResult UpdateCategory(int id)
        {
            CategoryVM cvm=new CategoryVM { Category=_cRep.Find(id) };
            return View(cvm);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            _cRep.Update(category);
            return RedirectToAction("CategoryList");
        }
        public ActionResult DeleteCategory(int id)
        {
            _cRep.Delete(_cRep.Find(id));
            return RedirectToAction("CategoryList");
        }
    }
}