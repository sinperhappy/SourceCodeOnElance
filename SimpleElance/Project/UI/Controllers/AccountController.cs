using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using DAL;
using UI.ViewModels;

namespace UI.Controllers
{ 
    public class AccountController : BaseController
    {
        private ProjectEntities DbEntities = new ProjectEntities();

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("LogIn", "Account");
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel LogIn)
        {
            if (ModelState.IsValid)
            {
                BLL.MdPassWord DESPassWord = new BLL.MdPassWord();
                LogIn.PassWord = DESPassWord.Encrypt(LogIn.PassWord);

                var UserResult = DbEntities.UserInfo.SingleOrDefault(p => p.UserName == LogIn.UserName && p.PassWord == LogIn.PassWord);
                if (UserResult != null)
                {
                    Session["UserLogin"] = UserResult;
                    if (UserResult.Type == 0)
                    {
                        return RedirectToAction("EmployerIndex", "Home");
                    }
                    else
                    {
                        return RedirectToAction("WorkerIndex", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Internationalization.Resources.LoginFailed);
                }
            }

            return View(LogIn);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection FormPost)
        {
            string check = FormPost["Type"];
            if (check == "0")
            {
                return RedirectToAction("AddEmployer", "Account");
            }
            else
            if (check == "1")
            {
                return RedirectToAction("AddWorker", "Account");
            }
            else
            {
                ModelState.AddModelError("", "failed, please check the right type.");
            }

            return View();
        }

        public ActionResult AddEmployer()
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();
            ItemList.Add(new SelectListItem { Value = "0", Text = Internationalization.Resources.Select });
            ItemList.Add(new SelectListItem { Value = "1", Text = Internationalization.Resources.Blog });
            ItemList.Add(new SelectListItem { Value = "2", Text = Internationalization.Resources.Conference });
            ItemList.Add(new SelectListItem { Value = "3", Text = Internationalization.Resources.Coworker });
            ItemList.Add(new SelectListItem { Value = "4", Text = Internationalization.Resources.Facebook });
            ItemList.Add(new SelectListItem { Value = "5", Text = Internationalization.Resources.Friend });
            ItemList.Add(new SelectListItem { Value = "6", Text = Internationalization.Resources.Linkedln });
            ItemList.Add(new SelectListItem { Value = "7", Text = Internationalization.Resources.OnlineAdvertisement });
            ItemList.Add(new SelectListItem { Value = "8", Text = Internationalization.Resources.OnlineNewsArticle });
            ItemList.Add(new SelectListItem { Value = "9", Text = Internationalization.Resources.SocialMediaSite });
            ItemList.Add(new SelectListItem { Value = "10", Text = Internationalization.Resources.TV });
            ItemList.Add(new SelectListItem { Value = "11", Text = Internationalization.Resources.Twitter });
            ItemList.Add(new SelectListItem { Value = "12", Text = Internationalization.Resources.WebSearchEngine });
            ItemList.Add(new SelectListItem { Value = "13", Text = Internationalization.Resources.Other });
            ViewData["HowHear"] = new SelectList(ItemList, "Value", "Text");

            var CountryResult = DbEntities.Country.ToList();
            ViewData["CountryName"] = new SelectList(CountryResult, "CountryID", "CountryName");

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployer(EmployerModel employer, FormCollection FormPost)
        {
            if (ModelState.IsValid)
            {
                string CountryId = FormPost["CountryName"];
                string HowId = FormPost["HowHear"];

                BLL.MdPassWord DESPassword = new BLL.MdPassWord();
                UserInfo EmployerInfo = new UserInfo();
                EmployerInfo.UserID = BLL.BaseUtility.GenerateGUID();
                EmployerInfo.FirstName = employer.FirstName;
                EmployerInfo.LastName = employer.LastName;
                EmployerInfo.Email = employer.Email;
                EmployerInfo.CompanyName = employer.CompanyName;
                EmployerInfo.CountryID = Convert.ToInt32(CountryId);
                EmployerInfo.UserName = employer.UserName;
                EmployerInfo.PassWord = DESPassword.Encrypt(employer.PassWord);
                EmployerInfo.How = Convert.ToInt32(HowId);
                EmployerInfo.Type = 0;

                DbEntities.UserInfo.AddObject(EmployerInfo);
                DbEntities.SaveChanges();

                Session["NewUser"] = EmployerInfo;

                return RedirectToAction("VerifyEmail", "Account");
            }

            return View();
        }

        public ActionResult AddWorker()
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();
            ItemList.Add(new SelectListItem { Value = "0", Text = Internationalization.Resources.Select });
            ItemList.Add(new SelectListItem { Value = "1", Text = Internationalization.Resources.Blog });
            ItemList.Add(new SelectListItem { Value = "2", Text = Internationalization.Resources.Conference });
            ItemList.Add(new SelectListItem { Value = "3", Text = Internationalization.Resources.Coworker });
            ItemList.Add(new SelectListItem { Value = "4", Text = Internationalization.Resources.Facebook });
            ItemList.Add(new SelectListItem { Value = "5", Text = Internationalization.Resources.Friend });
            ItemList.Add(new SelectListItem { Value = "6", Text = Internationalization.Resources.Linkedln });
            ItemList.Add(new SelectListItem { Value = "7", Text = Internationalization.Resources.OnlineAdvertisement });
            ItemList.Add(new SelectListItem { Value = "8", Text = Internationalization.Resources.OnlineNewsArticle });
            ItemList.Add(new SelectListItem { Value = "9", Text = Internationalization.Resources.SocialMediaSite });
            ItemList.Add(new SelectListItem { Value = "10", Text = Internationalization.Resources.TV });
            ItemList.Add(new SelectListItem { Value = "11", Text = Internationalization.Resources.Twitter });
            ItemList.Add(new SelectListItem { Value = "12", Text = Internationalization.Resources.WebSearchEngine });
            ItemList.Add(new SelectListItem { Value = "13", Text = Internationalization.Resources.Other });
            ViewData["HowHear"] = new SelectList(ItemList, "Value", "Text");

            var CountryResult = DbEntities.Country.ToList();
            ViewData["CountryName"] = new SelectList(CountryResult, "CountryID", "CountryName");

            return View();
        }

        [HttpPost]
        public ActionResult AddWorker(WorkerModel worker, FormCollection FormPost)
        {
            if (ModelState.IsValid)
            {
                string CountryId = FormPost["CountryName"];
                string HowId = FormPost["HowHear"];
                string AccountType = FormPost["AccountType"];

                BLL.MdPassWord DESPassword = new BLL.MdPassWord();
                UserInfo WorkerInfo = new UserInfo();
                WorkerInfo.UserID = BLL.BaseUtility.GenerateGUID();
                WorkerInfo.FirstName = worker.FirstName;
                WorkerInfo.LastName = worker.LastName;
                WorkerInfo.Email = worker.Email;
                WorkerInfo.CountryID = Convert.ToInt32(CountryId);
                WorkerInfo.UserName = worker.UserName;
                WorkerInfo.PassWord = DESPassword.Encrypt(worker.PassWord);
                WorkerInfo.How = Convert.ToInt32(HowId);
                WorkerInfo.AccountType = Convert.ToInt32(AccountType);
                WorkerInfo.DisplayName = worker.DisplayName;
                WorkerInfo.Type = 1;

                DbEntities.UserInfo.AddObject(WorkerInfo);
                DbEntities.SaveChanges();


                Session["NewUser"] = WorkerInfo;

                return RedirectToAction("WantJob", "Account");
            }

            return View();
        }

        [HttpPost]
        public JsonResult CheckUser(string UserName)
        {
            var Result = DbEntities.UserInfo.Where(p => p.UserName == UserName);
            if (Result.Count() != 0)
            {
                var JsonData = new { data = 1, value = Internationalization.Resources.CheckUser };
                return Json(JsonData);
            }
            else
            {
                var JsonData = new { data = 0, value = "" };
                return Json(JsonData);
            }
        }

        public ActionResult VerifyEmail()
        {
            if (Session["NewUser"] != null)
            {
                var User = (DAL.UserInfo)Session["NewUser"];
                ViewData["UserEmail"] = User.Email;
                
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        public ActionResult SendEmail()
        {
            if (Session["NewUser"] != null)
            {
                var User = (DAL.UserInfo)Session["NewUser"];

                DAL.Mail MailMessage = new Mail();
                MailMessage.ID = BLL.BaseUtility.GenerateGUID();
                MailMessage.FromAddress = "no-replay@task.com";
                MailMessage.ToAddress = User.Email;
                MailMessage.UserName = "taskpublish";
                MailMessage.PassWord = "taskpublish";
                MailMessage.ServerAddress = "smtp.task.com";
                MailMessage.Port = 25;
                MailMessage.Subject = Internationalization.Resources.SendEmailSubject;
                MailMessage.Body = Internationalization.Resources.SendEmailBody;

                try
                {
                    DbEntities.Mail.AddObject(MailMessage);
                    DbEntities.SaveChanges();

                    Session["UserLogin"] = Session["NewUser"];
                    Session.Remove("NewUser");
                    ViewData["UserType"] = User.Type;

                    ModelState.AddModelError("", Internationalization.Resources.SendEmailSuccess);
                }
                catch
                {
                    ModelState.AddModelError("", Internationalization.Resources.SendEmailFaield);
                }

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        public ActionResult Welcome()
        {
            if (Session["UserLogin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }

        }

        public ActionResult WantJob()
        {
            if (Session["NewUser"] != null)
            {
                WantJobList JobList = new WantJobList();
                JobList.WantList = new List<WantJobModel>();
                UI.Utility.WantJob WantJobCulture = new Utility.WantJob();
                

                var SubjectParent = DbEntities.Subjects.Where(p => p.ParentId == null);
                foreach (var TempParent in SubjectParent)
                {
                    WantJobModel WantJob = new WantJobModel();
                    WantJob.SubjectsList = new List<Subjects>();
                    WantJob.ID = TempParent.ID;
                    //WantJob.Name = TempParent.Name;
                    WantJob.Name = WantJobCulture.GetItem(TempParent.ID);

                    var SubjectSub = DbEntities.Subjects.Where(p => p.ParentId == TempParent.ID);
                    foreach (var TempSub in SubjectSub)
                    {
                        Subjects TempSubject = new Subjects();
                        TempSubject.ID = TempSub.ID;
                        TempSubject.Name = WantJobCulture.GetItem(TempSub.ID);
                        TempSubject.ParentId = TempSub.ParentId;

                        WantJob.SubjectsList.Add(TempSubject);
                    }

                    JobList.WantList.Add(WantJob);
                }

                return View(JobList);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpPost]
        public ActionResult WantJob(WantJobList JobList, FormCollection FormPost)
        {
            var User = (DAL.UserInfo)Session["NewUser"];
            string SelectStr = FormPost["subject-select"];

            if (SelectStr != "")
            {
                try
                {
                    string[] SelectResult = SelectStr.Substring(0, SelectStr.Length - 1).Split(',');
                    string NewWorkerId = User.UserID;

                    for (int i = 0; i < SelectResult.Count(); i++)
                    {
                        DAL.WantJob WorkerWant = new WantJob();
                        WorkerWant.ID = BLL.BaseUtility.GenerateGUID();
                        WorkerWant.SubjectsID = Convert.ToInt32(SelectResult[i]);
                        WorkerWant.WorkerID = NewWorkerId;

                        DbEntities.WantJob.AddObject(WorkerWant);
                        DbEntities.SaveChanges();
                    }

                    return RedirectToAction("VerifyEmail", "Account");
                }
                catch
                {
                    ModelState.AddModelError("", "failed, please try again.");
                    return View(JobList);
                }
            }
            else
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
        }

        protected override void Dispose(bool disposing)
        {
            DbEntities.Dispose();
            base.Dispose(disposing);
        }
    }
}