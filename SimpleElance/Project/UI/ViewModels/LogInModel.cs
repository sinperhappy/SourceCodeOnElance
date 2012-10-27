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
    public class LogInModel
    {
        [Required(ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "UserNameRequired")]
        [Display(Name = "UserName", ResourceType = typeof(Internationalization.Resources))]
        [StringLength(20, ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "UserNameMaxLength")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Internationalization.Resources), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "PassWord", ResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}