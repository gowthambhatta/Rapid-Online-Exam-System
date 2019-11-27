using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using onllineexam.Models;

namespace onllineexam.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {
         OnlineExamEntities db = new OnlineExamEntities();

        // GET: Logins
        public ActionResult Index()
        {
            return View(db.Logins.ToList());
        }
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(lOGIN1 log)
        {
            if (!ModelState.IsValid)
            {
                TempData["emails"] = log.email;
                var User_type = log.Type;
                if (User_type == "Student")
                {
                    var logmail = (from e in db.Logins
                                   where e.Email == log.email && e.Password == log.pass && e.Type == log.Type
                                   select e).SingleOrDefault();
                    if (logmail == null)
                    {
                        ModelState.AddModelError("", "User Doen't Exists");
                        return View();
                    }
                    else
                    {
                        string name1 = logmail.Students.First().F_Name.ToString();
                        Session["login"] = name1;
                        Session["sid"] = logmail.Students.First().Stu_ID;
                        Session["student"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else if (User_type == "Teacher")
                {
                   
                    var logmail = (from e in db.Logins
                                   where e.Email == log.email && e.Password == log.pass &&e.Type == log.Type
                                   select e).SingleOrDefault();
                  
                    if (logmail == null)
                    {
                        ModelState.AddModelError("", "User Doesn't Exist Please Register or Check e-mail and Password");
                        return View();
                    }
                    else 
                    {
                        string name1 = logmail.Teachers.First().Teach_Name;
                        Session["login"] = name1;
                        Session["tid"] = logmail.Teachers.First().TeacherID;
                        Session["Teacher"] = true;
                        return RedirectToAction("Index", "TeacherAccess");
                    }
                    
                }
            }
            else
            {
                ModelState.AddModelError("", "Enter Valid Login emailid annd Password");
                return View();
            }

            return View();
        }

        public JsonResult IsAlreadySigned(String Email)
        {
            return Json(!db.Logins.Any(User => User.Email.ToLower() == Email.ToLower()),
                JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region SignUp
        public ActionResult Sign_Up()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Sign_Up(lOGIN1 log)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["emails"] = log.email;
                    var User_type = log.Type;
                    if (User_type == "Student")
                    {
                        var email_id = (from e in db.Students
                                        where e.Stu_Email == log.email 
                                        select e.Stu_Email).SingleOrDefault();
                        var logmail = (from e in db.Logins
                                       where e.Email == log.email && e.Password == log.pass &&log.Type==e.Type
                                       select e.Email).SingleOrDefault();


                        if (logmail == null)
                        {
                            Login lg = new Login();
                            lg.Email = log.email;
                            lg.Password = log.pass;
                            lg.Type = log.Type;
                            db.Logins.Add(lg);
                            db.SaveChanges();
                            return RedirectToAction("Stu_Registration");
                        }
                        else if (email_id != logmail)
                        {
                            return RedirectToAction("Stu_Registration");
                        }
                        else if (email_id == logmail)
                        {
                            ModelState.AddModelError("", "User Already Exists!\n Please Login from Login Tab");
                            return View();
                        }
                    }
                    else if (User_type == "Teacher")
                    {
                        var email_id = (from e in db.Teachers
                                        where e.Teach_Email == log.email
                                        select e.Teach_Email).SingleOrDefault();
                        var logmail = (from e in db.Logins
                                       where e.Email == log.email && e.Password == log.pass && log.Type == e.Type
                                       select e.Email).SingleOrDefault();
                        if (logmail == null)
                        {
                            Login lg = new Login();
                            lg.Email = log.email;
                            lg.Password = log.pass;
                            lg.Type = log.Type;
                            db.Logins.Add(lg);
                            db.SaveChanges();
                            return RedirectToAction("Teach_Registration");
                        }
                        else if (email_id == logmail)
                        {
                            ModelState.AddModelError("", "Teacher already Exists \nPlease Login from Login Tab");
                            return View();
                        }
                        else if (email_id != logmail)
                        {
                            return RedirectToAction("Teach_Registration");
                        }
                    }
                    else
                    {
                        return View();
                    }
                }

            }

            catch (Exception e)
            {
                Response.Write("erroe Occured" + e.Message);
            }

            return View();
        }
        #endregion

        #region StudentRegistraion
        public ActionResult Stu_Registration()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Stu_Registration(Student std)
        {
            if (ModelState.IsValid)
            {
                std.Stu_Email = Convert.ToString(TempData["emails"]);
                db.Students.Add(std);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Please Fill the Required Details");
                return View();
            }
        }
        #endregion

        #region TeacherRegistraton
        public ActionResult Teach_Registration()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Teach_Registration(Teacher tch)
        {
            if (ModelState.IsValid)
            {
                tch.Teach_Email = Convert.ToString(TempData["emails"]);
                db.Teachers.Add(tch);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Please Fill the Required Details");
                return View();
            }


            
        }
        #endregion

        #region LogOff
        //[HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
        #endregion

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Login f)
        {
            var CustEmail = f.Email;
            
            var res = (from i in db.Logins
                       where i.Email == CustEmail  
                       select i.Password).SingleOrDefault();
            if (res != null)

            {

                string to = f.Email; //To address    
                string from = "rapidexamsystem@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Welcome To Rapid Online System" + " <br />" + "Email id " + f.Email + " <br />" + "Your Password: " + res;
                message.Subject = "forgot Password";
                message.Body = mailbody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("rapidexamsystem@gmail.com", "rapido@123");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                TempData["Forget"] = "E-mail Sent Successfully";
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}