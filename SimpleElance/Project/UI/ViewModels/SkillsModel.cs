using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UI.ViewModels
{
    [Table("SkillsInfo")]
    public class SkillsModel
    {
        [Key]
        public string ID { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Picture", ResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }

        [Display(Name = "Tagline", ResourceType = typeof(Internationalization.Resources))]
        public string TagLine { get; set; }

        [Display(Name = "HourlyRate", ResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.Currency, ErrorMessageResourceName = "HourlyRateFailed", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public double? MyRate { get; set; }

        [Display(Name = "SystemRate", ResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.Currency, ErrorMessageResourceName = "SystemRateFailed", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public double? SystemRate { get; set; }

        [Display(Name = "OverView", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceName = "OverViewRequired", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string OverView { get; set; }

        [Display(Name = "Skills", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceName = "SkillsRequired", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string YourSkills { get; set; }
    }
}