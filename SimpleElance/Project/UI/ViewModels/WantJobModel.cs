using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.ViewModels
{
    public class WantJobModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public virtual List<DAL.Subjects> SubjectsList { get; set; }
    }

    public class WantJobList
    {
        public virtual List<WantJobModel> WantList { get; set; }
    }
}