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
    public class QuestionController : Controller
    {
        private OnlineExamEntities db = new OnlineExamEntities();

        // GET: Question
        public ActionResult Index()
        {
            int testid = Convert.ToInt32(Session["testid"]);
            var questionDatas = (from q in db.QuestionDatas
                                where q.testid == 1000
                                 select q).ToList();

            return View(questionDatas.ToList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionData questionData = db.QuestionDatas.Find(id);
            if (questionData == null)
            {
                return HttpNotFound();
            }
            return View(questionData);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "testid,Question_number,Question,option1,option2,option3,option4,correct_option,Level,Description")] QuestionData questionData)
        {
            if (ModelState.IsValid)
            {
                db.QuestionDatas.Add(questionData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name", questionData.testid);
            return View(questionData);
        }

        // GET: Question/Edit/5

        public ActionResult Edit(int? id, int? id1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionData questionData = db.QuestionDatas.Find(id,id1);
            if (questionData == null)
            {
                return HttpNotFound();
            }
            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name", questionData.testid);
            return View(questionData);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "testid,Question_number,Question,option1,option2,option3,option4,correct_option,Level,Description")] QuestionData questionData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name", questionData.testid);
            return View(questionData);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id, int? id1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionData questionData = db.QuestionDatas.Find(id,id);
            if (questionData == null)
            {
                return HttpNotFound();
            }
            return View(questionData);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionData questionData = db.QuestionDatas.Find(id);
            db.QuestionDatas.Remove(questionData);
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
