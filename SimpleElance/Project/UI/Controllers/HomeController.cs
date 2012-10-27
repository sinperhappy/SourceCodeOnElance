using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {
        private ProjectEntities DbEntities = new ProjectEntities();

        public ActionResult EmployerIndex()
        {
            if (Session["UserLogin"] != null)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                BLL.MdPassWord DESPassWord = new BLL.MdPassWord();
                ViewModels.EmployerModel EmployerInfo = new ViewModels.EmployerModel();

                EmployerInfo.CompanyName = UserInfo.CompanyName;
                ViewData["CountryName"] = DbEntities.Country.SingleOrDefault(p => p.CountryID == UserInfo.CountryID).CountryName;
                EmployerInfo.Email = UserInfo.Email;
                EmployerInfo.FirstName = UserInfo.FirstName;
                EmployerInfo.LastName = UserInfo.LastName;
                EmployerInfo.UserName = UserInfo.UserName;
                ViewData["PassWord"] = DESPassWord.Decrypt(UserInfo.PassWord);

                switch (UserInfo.How)
                {
                    case 0:
                        ViewData["HowHear"] = Internationalization.Resources.Select;
                        break;
                    case 1:
                        ViewData["HowHear"] = Internationalization.Resources.Blog;
                        break;
                    case 2:
                        ViewData["HowHear"] = Internationalization.Resources.Conference;
                        break;
                    case 3:
                        ViewData["HowHear"] = Internationalization.Resources.Coworker;
                        break;
                    case 4:
                        ViewData["HowHear"] = Internationalization.Resources.Facebook;
                        break;
                    case 5:
                        ViewData["HowHear"] = Internationalization.Resources.Friend;
                        break;
                    case 6:
                        ViewData["HowHear"] = Internationalization.Resources.Linkedln;
                        break;
                    case 7:
                        ViewData["HowHear"] = Internationalization.Resources.OnlineAdvertisement;
                        break;
                    case 8:
                        ViewData["HowHear"] = Internationalization.Resources.OnlineNewsArticle;
                        break;
                    case 9:
                        ViewData["HowHear"] = Internationalization.Resources.SocialMediaSite;
                        break;
                    case 10:
                        ViewData["HowHear"] = Internationalization.Resources.TV;
                        break;
                    case 11:
                        ViewData["HowHear"] = Internationalization.Resources.Twitter;
                        break;
                    case 12:
                        ViewData["HowHear"] = Internationalization.Resources.WebSearchEngine;
                        break;
                    case 13:
                        ViewData["HowHear"] = Internationalization.Resources.Other;
                        break;
                }

                return View(EmployerInfo);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        public ActionResult WorkerIndex()
        {
            if (Session["UserLogin"] != null)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                BLL.MdPassWord DESPassWord = new BLL.MdPassWord();
                ViewModels.WorkerInfo CurrentWorker = new ViewModels.WorkerInfo();
                ViewModels.WorkerModel WorkerInfo = new ViewModels.WorkerModel();

                WorkerInfo.UserID = UserInfo.UserID;
                WorkerInfo.FirstName = UserInfo.FirstName;
                WorkerInfo.LastName = UserInfo.LastName;
                WorkerInfo.Email = UserInfo.Email;
                ViewData["CountryName"] = DbEntities.Country.SingleOrDefault(p => p.CountryID == UserInfo.CountryID).CountryName;
                WorkerInfo.UserName = UserInfo.UserName;
                ViewData["PassWord"] = DESPassWord.Decrypt(UserInfo.PassWord);

                switch (UserInfo.How)
                {
                    case 0:
                        ViewData["HowHear"] = Internationalization.Resources.Select;
                        break;
                    case 1:
                        ViewData["HowHear"] = Internationalization.Resources.Blog;
                        break;
                    case 2:
                        ViewData["HowHear"] = Internationalization.Resources.Conference;
                        break;
                    case 3:
                        ViewData["HowHear"] = Internationalization.Resources.Coworker;
                        break;
                    case 4:
                        ViewData["HowHear"] = Internationalization.Resources.Facebook;
                        break;
                    case 5:
                        ViewData["HowHear"] = Internationalization.Resources.Friend;
                        break;
                    case 6:
                        ViewData["HowHear"] = Internationalization.Resources.Linkedln;
                        break;
                    case 7:
                        ViewData["HowHear"] = Internationalization.Resources.OnlineAdvertisement;
                        break;
                    case 8:
                        ViewData["HowHear"] = Internationalization.Resources.OnlineNewsArticle;
                        break;
                    case 9:
                        ViewData["HowHear"] = Internationalization.Resources.SocialMediaSite;
                        break;
                    case 10:
                        ViewData["HowHear"] = Internationalization.Resources.TV;
                        break;
                    case 11:
                        ViewData["HowHear"] = Internationalization.Resources.Twitter;
                        break;
                    case 12:
                        ViewData["HowHear"] = Internationalization.Resources.WebSearchEngine;
                        break;
                    case 13:
                        ViewData["HowHear"] = Internationalization.Resources.Other;
                        break;
                }

                switch (UserInfo.AccountType)
                {
                    case 0:
                        ViewData["AccountType"] = Internationalization.Resources.Individual;
                        break;
                    case 1:
                        ViewData["AccountType"] = Internationalization.Resources.Business;
                        break;
                }

                WorkerInfo.DisplayName = UserInfo.DisplayName;

                CurrentWorker.WorkerBasic = WorkerInfo;

                UI.ViewModels.BasicsModel BasicsModel = new ViewModels.BasicsModel();
                UI.Utility.ModelCopier.CopyModel(DbEntities.BasicsInfo.SingleOrDefault(p => p.UserId == UserInfo.UserID), BasicsModel);
                CurrentWorker.BasicsInfo = BasicsModel;

                UI.ViewModels.SkillsModel SkillsModel = new ViewModels.SkillsModel();
                UI.Utility.ModelCopier.CopyModel(DbEntities.SkillsInfo.SingleOrDefault(p => p.UserId == UserInfo.UserID), SkillsModel);
                CurrentWorker.SkillsInfo = SkillsModel;

                if (CurrentWorker.BasicsInfo != null)
                {
                    var TempCountry = DbEntities.Country.FirstOrDefault(p => p.CountryID == CurrentWorker.BasicsInfo.CountryId);
                    if (TempCountry != null)
                    {
                        ViewData["CountryInfo"] = TempCountry.CountryName;
                    }

                    var TempNumber = DbEntities.PhoneNumber.FirstOrDefault(p => p.PhoneNumberID == CurrentWorker.BasicsInfo.PhoneNumberId);
                    if (TempNumber != null)
                    {
                        ViewData["PhoneNumber"] = TempNumber.PhoneNumberName;
                    }
                }

                CurrentWorker.JobList = new ViewModels.WantJobList();
                CurrentWorker.JobList.WantList = new List<ViewModels.WantJobModel>();
                var CurrentWant = DbEntities.WantJob.Where(p => p.WorkerID == UserInfo.UserID);
                UI.Utility.WantJob WantJobCulture = new Utility.WantJob();

                foreach (var CurrentTemp in CurrentWant)
                {
                    var CurrentSubject = DbEntities.Subjects.SingleOrDefault(p => p.ID == CurrentTemp.SubjectsID);
                    if (CurrentSubject != null && CurrentSubject.ParentId == null)
                    {
                        ViewModels.WantJobModel WantJob = new ViewModels.WantJobModel();
                        WantJob.SubjectsList = new List<Subjects>();
                        WantJob.ID = CurrentSubject.ID;
                        WantJob.Name = WantJobCulture.GetItem(CurrentSubject.ID);

                        var SubjectSub = DbEntities.Subjects.Where(p => p.ParentId == CurrentSubject.ID);
                        foreach (var TempSub in SubjectSub)
                        {
                            Subjects TempSubject = new Subjects();
                            TempSubject.ID = TempSub.ID;
                            TempSubject.Name = WantJobCulture.GetItem(TempSub.ID);
                            TempSubject.ParentId = TempSub.ParentId;

                            WantJob.SubjectsList.Add(TempSubject);
                        }

                        CurrentWorker.JobList.WantList.Add(WantJob);
                    }
                }

                return View(CurrentWorker);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        public ActionResult AddBasics()
        {
            if (Session["UserLogin"] != null)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                var BasicsInfo = DbEntities.BasicsInfo.SingleOrDefault(p => p.UserId == UserInfo.UserID);

                if (BasicsInfo == null)
                {
                    ViewBag.CountryList = new SelectList(DbEntities.Country.ToList(), "CountryID", "CountryName");
                    ViewBag.PhoneNumberList = new SelectList(DbEntities.PhoneNumber.ToList(), "PhoneNumberID", "PhoneNumberName");

                    return View();
                }
                else
                {
                    ViewModels.BasicsModel BasicsView = new ViewModels.BasicsModel();
                    BasicsView.ID = BasicsInfo.ID;
                    BasicsView.UserId = BasicsInfo.UserId;
                    BasicsView.CountryId = BasicsInfo.CountryId;
                    BasicsView.Address = BasicsInfo.Address;
                    BasicsView.Address2 = BasicsInfo.Address2;
                    BasicsView.City = BasicsInfo.City;
                    BasicsView.Province = BasicsInfo.Province;
                    BasicsView.PostalCode = BasicsInfo.PostalCode;
                    BasicsView.PhoneNumberId = BasicsInfo.PhoneNumberId;
                    BasicsView.AreaCode = BasicsInfo.AreaCode;
                    BasicsView.PhoneNumber = BasicsInfo.PhoneNumber;
                    BasicsView.Ext = BasicsInfo.Ext;

                    ViewBag.CountryList = new SelectList(DbEntities.Country.ToList(), "CountryID", "CountryName", BasicsInfo.CountryId);
                    ViewBag.PhoneNumberList = new SelectList(DbEntities.PhoneNumber.ToList(), "PhoneNumberID", "PhoneNumberName", BasicsInfo.PhoneNumberId);

                    return View(BasicsView);
                }
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpPost]
        public ActionResult AddBasics(UI.ViewModels.BasicsModel BasicsInfo)
        {
            if (ModelState.IsValid)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                var BasicsResult = DbEntities.BasicsInfo.SingleOrDefault(p => p.UserId == UserInfo.UserID);

                if (BasicsResult == null)
                {
                    DAL.BasicsInfo NewBasics = new BasicsInfo();

                    NewBasics.ID = BLL.BaseUtility.GenerateGUID();
                    NewBasics.UserId = UserInfo.UserID;
                    NewBasics.CountryId = BasicsInfo.CountryId;
                    NewBasics.Address = BasicsInfo.Address;
                    NewBasics.Address2 = BasicsInfo.Address2;
                    NewBasics.City = BasicsInfo.City;
                    NewBasics.Province = BasicsInfo.Province;
                    NewBasics.PostalCode = BasicsInfo.PostalCode;
                    NewBasics.PhoneNumberId = BasicsInfo.PhoneNumberId;
                    NewBasics.AreaCode = BasicsInfo.AreaCode;
                    NewBasics.PhoneNumber = BasicsInfo.PhoneNumber;
                    NewBasics.Ext = BasicsInfo.Ext;

                    DbEntities.BasicsInfo.AddObject(NewBasics);
                    DbEntities.SaveChanges();
                }
                else
                {
                    BasicsResult.UserId = UserInfo.UserID;
                    BasicsResult.CountryId = BasicsInfo.CountryId;
                    BasicsResult.Address = BasicsInfo.Address;
                    BasicsResult.Address2 = BasicsInfo.Address2;
                    BasicsResult.City = BasicsInfo.City;
                    BasicsResult.Province = BasicsInfo.Province;
                    BasicsResult.PostalCode = BasicsInfo.PostalCode;
                    BasicsResult.PhoneNumberId = BasicsInfo.PhoneNumberId;
                    BasicsResult.AreaCode = BasicsInfo.AreaCode;
                    BasicsResult.PhoneNumber = BasicsInfo.PhoneNumber;
                    BasicsResult.Ext = BasicsInfo.Ext;
                    
                    DbEntities.SaveChanges();
                }

                return RedirectToAction("AddSkills", "Home");
            }

            ViewBag.CountryList = new SelectList(DbEntities.Country.ToList(), "CountryID", "CountryName", BasicsInfo.CountryId);
            ViewBag.PhoneNumberList = new SelectList(DbEntities.PhoneNumber.ToList(), "PhoneNumberID", "PhoneNumberName", BasicsInfo.PhoneNumberId);
            return View(BasicsInfo);
        }

        public ActionResult AddSkills()
        {
            if (Session["UserLogin"] != null)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                var SkillsResult = DbEntities.SkillsInfo.SingleOrDefault(p => p.UserId == UserInfo.UserID);

                if (SkillsResult == null)
                {
                    return View();
                }
                else
                {
                    UI.ViewModels.SkillsModel SkillsInfo = new ViewModels.SkillsModel();
                    SkillsInfo.ID = SkillsResult.ID;
                    SkillsInfo.UserId = SkillsResult.UserId;
                    SkillsInfo.PhotoPath = SkillsResult.PhotoPath;
                    SkillsInfo.TagLine = SkillsResult.TagLine;
                    SkillsInfo.MyRate = SkillsResult.MyRate;
                    SkillsInfo.SystemRate = SkillsResult.SystemRate;
                    SkillsInfo.OverView = SkillsResult.OverView;
                    SkillsInfo.YourSkills = SkillsResult.YourSkills;

                    return View(SkillsInfo);
                }
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpPost]
        public ActionResult AddSkills(UI.ViewModels.SkillsModel SkillsInfo, FormCollection FormPost)
        {
            if (ModelState.IsValid)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                var SkillsResult = DbEntities.SkillsInfo.SingleOrDefault(p => p.UserId == UserInfo.UserID);
                SkillsInfo.PhotoPath = FormPost["PhotoPathHidden"];

                if (SkillsResult == null)
                {
                    DAL.SkillsInfo NewSkills = new SkillsInfo();
                    NewSkills.ID = BLL.BaseUtility.GenerateGUID();
                    NewSkills.UserId = UserInfo.UserID;
                    NewSkills.PhotoPath = SkillsInfo.PhotoPath;
                    NewSkills.TagLine = SkillsInfo.TagLine;
                    NewSkills.MyRate = SkillsInfo.MyRate;
                    NewSkills.SystemRate = SkillsInfo.SystemRate;
                    NewSkills.OverView = SkillsInfo.OverView;
                    NewSkills.YourSkills = SkillsInfo.YourSkills;

                    DbEntities.SkillsInfo.AddObject(NewSkills);
                    DbEntities.SaveChanges();
                }
                else
                {
                    SkillsResult.UserId = UserInfo.UserID;
                    SkillsResult.PhotoPath = SkillsInfo.PhotoPath;
                    SkillsResult.TagLine = SkillsInfo.TagLine;
                    SkillsResult.MyRate = SkillsInfo.MyRate;
                    SkillsResult.SystemRate = SkillsInfo.SystemRate;
                    SkillsResult.OverView = SkillsInfo.OverView;
                    SkillsResult.YourSkills = SkillsInfo.YourSkills;

                    DbEntities.SaveChanges();
                }
            }

            return View(SkillsInfo);
        }

        protected override void Dispose(bool disposing)
        {
            DbEntities.Dispose();
            base.Dispose(disposing);
        }
    }
}