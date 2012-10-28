using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.ViewModels;
using DAL;

namespace UI.Controllers.Manage
{
    public class ManageController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ProjectEntities DbEntities = new ProjectEntities();
            if (Session["UserLogin"] != null)
            {
                var UserInfo = (DAL.UserInfo)Session["UserLogin"];
                BLL.MdPassWord DESPassWord = new BLL.MdPassWord();
                ViewModels.UserInfoModel userEntity = new ViewModels.UserInfoModel();

                userEntity.CompanyName = UserInfo.CompanyName;
                ViewData["CountryName"] = DbEntities.Country.SingleOrDefault(p => p.CountryID == UserInfo.CountryID).CountryName;
                userEntity.Email = UserInfo.Email;
                userEntity.FirstName = UserInfo.FirstName;
                userEntity.LastName = UserInfo.LastName;
                userEntity.UserName = UserInfo.UserName;
                //userEntity.AccountType = (int)UserInfo.AccountType;
                userEntity.CountryID = (int)UserInfo.CountryID;
                userEntity.DisplayName = UserInfo.DisplayName;
                userEntity.Email = UserInfo.Email;
                userEntity.How = (int)UserInfo.How;
                userEntity.PassWord = UserInfo.PassWord;
                userEntity.Type = UserInfo.Type;
                userEntity.UserID = UserInfo.UserID;
                userEntity.UserName = UserInfo.UserName;
                //ViewData["PassWord"] = DESPassWord.Decrypt(UserInfo.PassWord);

                return View(userEntity);
            }
            else
            {
                return RedirectToAction("SignIn", "Manage");
            }
        }


        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LogInModel LogIn)
        {
            ProjectEntities DbEntities = new ProjectEntities();

            if (ModelState.IsValid)
            {
                BLL.MdPassWord DESPassWord = new BLL.MdPassWord();
                LogIn.PassWord = DESPassWord.Encrypt(LogIn.PassWord);

                var UserResult = DbEntities.UserInfo.SingleOrDefault(p => p.UserName == LogIn.UserName && p.PassWord == LogIn.PassWord );

                if (UserResult != null)
                {
                    Session["UserLogin"] = UserResult;
                    if (UserResult.Type == 2)
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                    else
                    {
                        ModelState.AddModelError("", Internationalization.Resources.LoginFailed);
                    }
                }
                else
                {
                    ModelState.AddModelError("", Internationalization.Resources.LoginFailed);
                }
            }

            return View(LogIn);
        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("SignIn", "Manage"); 
        }
    }
}
