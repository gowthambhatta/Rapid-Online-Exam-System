using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;

namespace onllineexam.Controllers
{
    [HandleError]
    public class SelectExamController : Controller
    {
        OnlineExamEntities db = new OnlineExamEntities();
        // GET: SelectExam
        public ActionResult Index()
        {
            int count = 0;
            var name = db.TestGenerators.ToList();
            SelectList list = new SelectList(name, "Test_id", "Test_name");
            DrpList drp = new DrpList();
            drp.Examlist = name;
            drp.QuestionNo = 1;
            ViewBag.name = list;
            Session["name"] = ViewBag.name;
            Session["correctAns"] = 0;
            return View(drp);
        }
        [HttpPost]
        public void SetFromDrp(int val)
        {
            ViewBag.drpVal = val;
        }
    }

}