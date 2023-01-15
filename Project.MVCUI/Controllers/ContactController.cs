using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
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
        IssueRepository _İRep;

        public ContactController()
        {
            _İRep = new IssueRepository();
        }

        // GET: Contanct
        public ActionResult AddTechnicalIssue()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTechnicalIssue(Issue issue)
        {
            _İRep.Add(issue);

            string Mail = "Talebinizi aldik en kısa zamanda dönüs yapılacaktır";

            MailService.Send(issue.Email, body: Mail, subject: "Destek Ekibi");

            return RedirectToAction("Contact", "Home");
            
        }

        public ActionResult AddMesage()
        {
            return View();
        }
        
    }
}