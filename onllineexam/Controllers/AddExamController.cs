using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;

namespace onllineexam.Controllers
{
    [HandleError]
    public class AddExamController : Controller
    {
        OnlineExamEntities db = new OnlineExamEntities();
        // GET: AddExam
        [HandleError]
        public ActionResult CreateTest()
        {
            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name");
            ViewBag.Teach_id = new SelectList(db.Teachers, "TeacherID", "Teach_Name");
            ViewBag.Test_id = new SelectList(db.QuestionFiles, "testid", "subname");
            return View();
        }
        // POST: Exam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTest([Bind(Include = "Test_id,Test_name,Teach_id,Test_date,Test_time,sub_id")] TestGenerator testGenerator)
        {
            if (ModelState.IsValid)
            {
                db.TestGenerators.Add(testGenerator);
                db.SaveChanges();
                 var testid = db.TestGenerators.Max(r=>r.Test_id);
                Session["exid"] = testid;
                return RedirectToAction("SelectTest");
            }

            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name", testGenerator.sub_id);
            ViewBag.Teach_id = new SelectList(db.Teachers, "TeacherID", "Teach_Name", testGenerator.Teach_id);
            ViewBag.Test_id = new SelectList(db.QuestionFiles, "testid", "subname", testGenerator.Test_id);
            return View();
        }
        [HandleError]
        public ActionResult SelectTest()
        {
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
        [HandleError]
        public void SetFromDrp(int val)
        {
            Session["exid"]=val;

            ViewBag.drpVal = val;
        }
    }
}