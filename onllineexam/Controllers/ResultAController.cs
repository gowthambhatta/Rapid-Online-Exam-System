using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;
using Rotativa;

namespace onllineexam.Controllers
{
    [HandleError]
    public class ResultAController : Controller
    {
        private OnlineExamEntities db = new OnlineExamEntities();

        // GET: ResultA
        //public ActionResult Index()
        //{
        //    return View(db.Result_By_Test_Result.ToList());
        //}

        public ActionResult Result_By_Stuandsub1(Student sid, Subject subid)
        {
            int sub_id = Convert.ToInt32(subid.Sub_ID);
            //return View(db.Result_By_stu_and_sub(sid.Stu_ID, sub_id).ToList());
            return View(db.Result_By_stu_and_sub(501, 103).ToList());
        }

        public ActionResult PrintViewToPdfSS()
        {
            var report = new ActionAsPdf("Result_By_Stuandsub1");
            return report;
        }

        public ActionResult Result_By_Test(TestGenerator id)
        {
            //int? i = id.Test_id;
            return View(db.Result_By_Test(49312).ToList());
        }

        public ActionResult Result_By_Subject1(Subject subid)
        {
            //int? i = .Sub_ID;
            int sub_id = Convert.ToInt32(subid.Sub_ID);
            return View(db.Result_By_subject(501).ToList());
        }
        public ActionResult PrintViewToPdfS()
        {
            var report = new ActionAsPdf("Result_By_Subject1");
            return report;
        }

        public ActionResult Result_By_Stuandtest(Student sid, TestGenerator tid)
        {
            //int sub_id = Convert.ToInt32(subid.Sub_ID);
            //return View(db.Result_By_stu_and_test(sid.Stu_ID,tid.Test_id).ToList());
            return View(db.Result_By_stu_and_test(103, 49312).ToList());
        }
        public ActionResult PrintViewToPdfST()
        {
            var report = new ActionAsPdf("Result_By_Stuandtest");
            return report;
        }


        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Result_By_Test");
            return report;
        }

        //public ActionResult PrintPartialViewToPdf(int? id)
        //{

        //    //int sub_id = Convert.ToInt32(id.Sub_ID);
        //    //db.Result_By_subject(id);
        //    var report = new PartialViewAsPdf("~/Views/Shared/ResultPDF.cshtml", db.Result_By_subject(id));
        //    return report;

        //}
    }
}
