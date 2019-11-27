using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;
using OfficeOpenXml;

namespace onllineexam.Controllers
{
    [HandleError]
    public class UploadController : Controller
    {
        OnlineExamEntities db = new OnlineExamEntities();

        // GET: Upload
        [HandleError]
        public ActionResult Index(DrpList drp)
        {
            ViewBag.drpData = drp;
            Session["exid"] = drp.examid;
            
            return RedirectToAction("Upload");
        }
        [HandleError]
        public ActionResult Upload(FormCollection formCollection)
        {
            var usersList = new List<QuestionData>();
            
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                TempData["uploadstatus"] = "false";
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    TempData["uploadstatus"] = "true";
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        ViewBag.row = noOfRow - 1;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new QuestionData();

                            //user.testid = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            user.testid = Convert.ToInt32(Session["exid"]);
                            user.Question_number = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            user.Question = workSheet.Cells[rowIterator, 3].Value.ToString();
                            user.option1 = workSheet.Cells[rowIterator, 4].Value.ToString();
                            user.option2 = workSheet.Cells[rowIterator, 5].Value.ToString();
                            user.option3 = workSheet.Cells[rowIterator, 6].Value.ToString();
                            user.option4 = workSheet.Cells[rowIterator, 7].Value.ToString();
                            user.correct_option = Convert.ToInt32(workSheet.Cells[rowIterator, 8].Value);
                            user.Level= workSheet.Cells[rowIterator, 9].Value.ToString();
                            user.Description = workSheet.Cells[rowIterator, 10].Value.ToString();
                            usersList.Add(user);
                        }
                    }
                }
            }

            foreach (var item in usersList)
            {
                db.QuestionDatas.Add(item);
            }
            db.SaveChanges();
            
            return View();
        }
    }
}
