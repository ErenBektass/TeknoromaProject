using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Project.MVCUI.Controllers
{
    public class ContactController : Controller
    {
        IssueRepository _IRep;

        public ContactController()
        {
            _IRep = new IssueRepository();
        }

        public ActionResult IssueList(int? id)
        {
            IssueVM isvm = id == null ? new IssueVM
            {
                Issues = _IRep.GetAll().Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).ToList()
            } : new IssueVM { Issue = _IRep.Where(x => x.ID == id).FirstOrDefault() };
            return View(isvm);

        }

        // GET: Contanct
        public ActionResult AddIssue()
        {
            return View();
        }

        public ActionResult IssueDetails(int id)
        {
            IssueVM isvm = new  IssueVM { Issue = _IRep.Where(x => x.ID == id).FirstOrDefault() };
            return View(isvm);
        }

        [HttpPost]
        public ActionResult AddIssue(Issue issue)
        {
            _IRep.Add(issue);

            string Mail = "Talebinizi aldik en kısa zamanda dönüs yapılacaktır";

            MailService.Send(issue.Email, body: Mail, subject: "Destek Ekibi");

            return RedirectToAction("IssueList");
            
        }

        public ActionResult UpdateIssue(int id)
        {
            IssueVM isvm = new IssueVM
            {
                Issue = _IRep.Find(id)
                
            };
            return View(isvm);
        }
        [HttpPost]
        public ActionResult UpdateIssue(Issue issue)
        {
            _IRep.Update(issue);
            return RedirectToAction("IssueList");
        }


        public ActionResult AddMesage()
        {
            return View();
        }

        
        public ActionResult DeleteIssue(int id)
        {
            _IRep.Delete(_IRep.Find(id));
            return RedirectToAction("IssueList");
        }
        
    }
}