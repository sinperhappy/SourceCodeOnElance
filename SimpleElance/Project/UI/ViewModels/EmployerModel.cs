using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UI.ViewModels
{
    public class EmployerModel
    {
        public string UserID { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        [StringLength(20, ErrorMessageResourceName = "UserNameMaxLength", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        [StringLength(20, ErrorMessageResourceName = "UserNameMaxLength", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string LastName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceName = "EmailFaield", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string Email { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Internationalization.Resources))]
        public int CountryID { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Internationalization.Resources))]
        public string CompanyName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "UserNameRequired")]
        [Display(Name = "UserName", ResourceType = typeof(Internationalization.Resources))]
        [StringLength(20, ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "UserNameMaxLength")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "PassWord", ResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Display(Name = "RetypePassWord", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "RetypePassWordRequired")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "PassWordFaield")]
        public string RetypePassword { get; set; }

        [Display(Name = "How", ResourceType = typeof(Internationalization.Resources))]
        public string How { get; set; }
    }
}