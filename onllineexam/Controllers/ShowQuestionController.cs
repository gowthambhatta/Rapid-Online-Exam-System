 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;

namespace onllineexam.Controllers
{
    [HandleError]
    public class ShowQuestionController : Controller
    {

        static int count = 0;
        public ActionResult Index()
        {
            return View();
        }
        // GET: ShowQuestion
        OnlineExamEntities db = new OnlineExamEntities();
        [HttpPost]
        public ActionResult Index(DrpList drp)
        {

            ViewBag.drpData = drp;
            ViewBag.questionNo = drp.QuestionNo;
            TempData["a"] = drp.QuestionNo;
            
            Session["subid"] = 505;

            if (drp.QuestionNo == 1)
            //if (questionNo == null || id == null)
            {
                Session["exid"] = drp.examid;
                //Session["level1"] = db.MaximumQuestnNumINLevel(drp.examid, 1);
                //Session["level2"] = db.NumOfQuestByLevel((int)Session["exid"], 2);
                //Session["level3"] = db.NumOfQuestByLevel((int)Session["exid"], 3);
                QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == 1 && m.testid == drp.examid);


                TempData["qData"] = SingleQuestion;
                return RedirectToAction("NextQuestion");
                //return View(SingleQuestion);
            }
            else
            {


                QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == drp.QuestionNo && m.testid == drp.examid);
                int qus = (int)drp.QuestionNo;

                //for (int i = 0; i <= drp.QuestionNo; i++)
                //{
                //    ViewBag.questionNo = qusno + i;
                //}


                return View(SingleQuestion);
            }

        }


        int add = 0;

        public ActionResult Next(QuestionData aaa)
        {
            int qId = (int)aaa.Question_number + 1;
            QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == qId && m.testid == aaa.testid);
            ViewBag.questionNo = qId;
            TempData["a"] = SingleQuestion.Question_number;
            TempData["qData"] = SingleQuestion;
            return RedirectToAction("NextQuestion");
        }


        public ActionResult NextQuestion()
        {
            int qNo = Convert.ToInt32(TempData["a"]);
            ViewBag.questionNo = qNo;
            QuestionData a = (QuestionData)TempData["qData"];
            // TempData.Keep("a");
            // TempData.Keep("qData");
            return View(a);

        }
        [HttpPost]
        public ActionResult NextQuestion(QuestionData aaa)
        {
            int level = Convert.ToInt32(aaa.Level);
            if (aaa.correct_option == aaa.selectedvalue && aaa.Question_number != 1)
            {
                Session["correctAns"] = Convert.ToInt32(Session["correctAns"]) + 1;
            }
            else if (aaa.correct_option == aaa.selectedvalue && aaa.Question_number == 1)
            {
                Session["correctAns"] = 1;
            }
            count++;
            if (level == 1)
            {
                //var levelone = Session["level1"];
                // int? level1 = Convert.ToInt32(levelone);
                if (aaa.Question_number == 2)
                {
                    float percent = ((int)Session["correctAns"] * 100) / count;
                    if (percent >= 65)
                    {
                        Session["message"] = "Congratualtion you have cleared level 1,Continue with level 2";
                        int qId = (int)aaa.Question_number + 1;
                        QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == qId && m.testid == aaa.testid);
                        ViewBag.questionNo = qId;
                        TempData["a"] = SingleQuestion.Question_number;
                        TempData["qData"] = SingleQuestion;
                        count = 0;
                        db.InsertScore1((int)Session["sid"], (int)Session["subid"], (int)Session["exid"], (int)Session["correctAns"]);
                        Session["resid"] = db.Results.Max(r => r.ResId);
                        Session["correctAns"] = 0;
                        TempData["status"] = "pass";
                        return RedirectToAction("NextQuestion");
                    }
                    else
                    {
                        Session["message"] = "Better Luck Next Time";
                        TempData["status"] = "fail";
                        return View("Thankyou");
                    }
                }
                else
                {
                    int qId = (int)aaa.Question_number + 1;
                    QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == qId && m.testid == aaa.testid);
                    ViewBag.questionNo = qId;
                    TempData["a"] = SingleQuestion.Question_number;
                    TempData["qData"] = SingleQuestion;
                    return RedirectToAction("NextQuestion");
                }

            }
            else if (level == 2)
            {
                //int level2 = (int)Session["level2"];
                if (aaa.Question_number == 4)
                {
                    float percent = ((int)Session["correctAns"] * 100) / count;
                    if (percent >= 65)
                    {
                        Session["message"] = "Congratualtion you have cleared level 2,Continue with level 3";
                        int qId = (int)aaa.Question_number + 1;
                        QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == qId && m.testid == aaa.testid);
                        ViewBag.questionNo = qId;
                        count = 0;
                        db.InsertScore2((int)Session["resid"], (int)Session["correctAns"]);
                        Session["correctAns"] = 0;
                        TempData["a"] = SingleQuestion.Question_number;
                        TempData["qData"] = SingleQuestion;
                        TempData["status"] = "pass";
                        return RedirectToAction("NextQuestion");
                    }
                    else
                    {
                        Session["message"] = "Better Luck Next Time";
                        TempData["status"] = "fail";
                        return View("Thankyou");
                    }
                }
                else
                {
                    int qId = (int)aaa.Question_number + 1;
                    QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == qId && m.testid == aaa.testid);
                    ViewBag.questionNo = qId;
                    TempData["a"] = SingleQuestion.Question_number;
                    TempData["qData"] = SingleQuestion;
                    return RedirectToAction("NextQuestion");
                }

            }
            else if (level == 3)
            {
                //int level3 = (int)Session["level3"];
                if (aaa.Question_number == 6)
                {
                    float percent = ((int)Session["correctAns"] * 100) / count;
                    if (percent >= 65)
                    {
                        Session["message"] = "Congratualtion you have cleared level 3";
                        db.InsertScore3((int)Session["resid"], (int)Session["correctAns"]);
                        TempData["status"] = "pass";
                        return View("Thankyou");
                    }
                    else
                    {
                        Session["message"] = "Better Luck Next Time";
                        TempData["status"] = "fail";
                        return View("Thankyou");
                    }
                }
                else
                {
                    int qId = (int)aaa.Question_number + 1;
                    QuestionData SingleQuestion = db.QuestionDatas.SingleOrDefault(m => m.Question_number == qId && m.testid == aaa.testid);
                    ViewBag.questionNo = qId;
                    TempData["a"] = SingleQuestion.Question_number;
                    TempData["qData"] = SingleQuestion;
                    return RedirectToAction("NextQuestion");
                }
            }
            return View();


        }
    }
}