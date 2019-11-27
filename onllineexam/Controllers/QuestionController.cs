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
        public ActionResult Index(DrpList drp)
        {
            ViewBag.drpData = drp;
            Session["exid"] = drp.examid;
            int testid = Convert.ToInt32(Session["exid"]);
            var questionDatas = (from q in db.QuestionDatas
                                where q.testid == testid
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
        public ActionResult Create([Bind(Include = "Question,option1,option2,option3,option4,correct_option,Level,Description")] QuestionData questionData)
        {
            int testid = (int)Session["exid"];
            if (ModelState.IsValid)
            {
                questionData.testid = (int)Session["exid"];
                questionData.Question_number = ((from e
                                                in db.QuestionDatas
                                                where e.testid == testid
                                                orderby e.Question_number descending
                                                select e.Question_number).Take(1).First())+1;
                db.QuestionDatas.Add(questionData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.testid = new SelectList(db.TestGenerators, "Test_id", "Test_name", questionData.testid);
            return RedirectToAction("Index");
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
