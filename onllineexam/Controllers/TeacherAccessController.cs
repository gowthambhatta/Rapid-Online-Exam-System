using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onllineexam.Controllers
{
    [HandleError]
    public class TeacherAccessController : Controller
    {
         //GET: TeacherAccess
        public ActionResult Index()
        {
            return View();
        }
       
    }
}