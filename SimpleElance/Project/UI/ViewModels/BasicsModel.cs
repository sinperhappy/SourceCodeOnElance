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
    [Table("BasicsInfo")]
    public class BasicsModel
    {
        [Key]
        public string ID { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Internationalization.Resources))]
        public int CountryId { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Internationalization.Resources))]
        [Required(ErrorMessageResourceName = "AddressRequired", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string Address { get; set; }

        [Display(Name = "Address2", ResourceType = typeof(Internationalization.Resources))]
        public string Address2 { get; set; }

        [Display(Name = "City", ResourceType = typeof(Internationalization.Resources))]
        public string City { get; set; }

        [Display(Name = "State", ResourceType = typeof(Internationalization.Resources))]
        public string Province { get; set; }

        [Display(Name = "Zip", ResourceType = typeof(Internationalization.Resources))]
        public string PostalCode { get; set; }

        [Display(Name = "CountryPhone", ResourceType = typeof(Internationalization.Resources))]
        public int? PhoneNumberId { get; set; }

        [Display(Name = "AreaCode", ResourceType = typeof(Internationalization.Resources))]
        public string AreaCode { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Internationalization.Resources))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "PhoneNumberField", ErrorMessageResourceType = typeof(Internationalization.Resources))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ext", ResourceType = typeof(Internationalization.Resources))]
        public string Ext { get; set; }
    }
}