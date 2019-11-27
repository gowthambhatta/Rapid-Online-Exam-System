using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;

namespace onllineexam.Controllers
{
    [HandleError]
    public class ResultsController : Controller
    {
        private OnlineExamEntities db = new OnlineExamEntities();

        // GET: Results
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Student).Include(r => r.Subject).Include(r => r.TestGenerator);
            return View(results.ToList());
        }

        // GET: Results/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results/Create
        public ActionResult Create()
        {

            int score = Convert.ToInt32(Session["correctAns"]);
            if (score >= 9)
            {
                ViewBag.grade = "A+";
                ViewBag.quality = "Best";
            }
            else if (score >= 8)
            {
                ViewBag.grade = "A";
                ViewBag.quality = "Better";
            }
            else if (score >= 5)
            {
                ViewBag.grade = "B";
                ViewBag.quality = "Good";
            }
            else
            {
                ViewBag.grade = "F";
                ViewBag.quality = "Fail";
            }

            Session["grade"] = ViewBag.grade;
            Session["quality"] = ViewBag.quality;

            ViewBag.candidate_id = 1;
            ViewBag.exam_id = 1;
            return View();
        }

        // POST: /Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "exam_id,candidate_id,total,grade,quality")] Result result_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result_tbl);
                db.SaveChanges();
                return RedirectToAction("ShowResult");
            }

            ViewBag.candidate_id = new SelectList(db.Students, "Stu_ID", "F_Name", result_tbl.Stu_id);
            ViewBag.exam_id = new SelectList(db.TestGenerators, "Test_id", "Test_name", result_tbl.testid);
            return View(result_tbl);
        }
        public ActionResult ShowResult()
        {

            return View();

        }

        // GET: Results/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stu_id = new SelectList(db.Students, "Stu_ID", "F_Name", result.Stu_id);
            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name", result.sub_id);
            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name", result.testid);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResId,Stu_id,sub_id,testid,score1,score2,score3,Status")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stu_id = new SelectList(db.Students, "Stu_ID", "F_Name", result.Stu_id);
            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name", result.sub_id);
            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name", result.testid);
            return View(result);
        }

        // GET: Results/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Evaluate()
        {
            int score = Convert.ToInt32(Session["correctAns"]);
            float percent = (score * 100) / 6;
            ViewBag.percent = percent;
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
