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
    public class ExamController : Controller
    {
        private OnlineExamEntities db = new OnlineExamEntities();

        // GET: Exam
        public ActionResult Index()
        {
            var testGenerators = db.TestGenerators.Include(t => t.Subject).Include(t => t.Teacher).Include(t => t.QuestionFile);
            return View(testGenerators.ToList());
        }

        // GET: Exam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGenerator testGenerator = db.TestGenerators.Find(id);
            if (testGenerator == null)
            {
                return HttpNotFound();
            }
            return View(testGenerator);
        }

        // GET: Exam/Create
        public ActionResult Create()
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Test_id,Test_name,Teach_id,Test_date,Test_time,sub_id")] TestGenerator testGenerator)
        {
            if (ModelState.IsValid)
            {
                db.TestGenerators.Add(testGenerator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name", testGenerator.sub_id);
            ViewBag.Teach_id = new SelectList(db.Teachers, "TeacherID", "Teach_Name", testGenerator.Teach_id);
            ViewBag.Test_id = new SelectList(db.QuestionFiles, "testid", "subname", testGenerator.Test_id);
            return View(testGenerator);
        }

        // GET: Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGenerator testGenerator = db.TestGenerators.Find(id);
            if (testGenerator == null)
            {
                return HttpNotFound();
            }
            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name", testGenerator.sub_id);
            ViewBag.Teach_id = new SelectList(db.Teachers, "TeacherID", "Teach_Name", testGenerator.Teach_id);
            ViewBag.Test_id = new SelectList(db.QuestionFiles, "testid", "subname", testGenerator.Test_id);
            return View(testGenerator);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Test_id,Test_name,Teach_id,Test_date,Test_time,sub_id")] TestGenerator testGenerator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testGenerator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sub_id = new SelectList(db.Subjects, "Sub_ID", "sub_Name", testGenerator.sub_id);
            ViewBag.Teach_id = new SelectList(db.Teachers, "TeacherID", "Teach_Name", testGenerator.Teach_id);
            ViewBag.Test_id = new SelectList(db.QuestionFiles, "testid", "subname", testGenerator.Test_id);
            return View(testGenerator);
        }

        // GET: Exam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGenerator testGenerator = db.TestGenerators.Find(id);
            if (testGenerator == null)
            {
                return HttpNotFound();
            }
            return View(testGenerator);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestGenerator testGenerator = db.TestGenerators.Find(id);
            db.TestGenerators.Remove(testGenerator);
            db.SaveChanges();
            return RedirectToAction("Index");
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
