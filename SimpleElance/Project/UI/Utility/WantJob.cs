using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Internationalization;

namespace UI.Utility
{
    public class WantJob
    {
        public Dictionary<int, string> ResourcesList = new Dictionary<int, string>();

        public WantJob()
        {
            ResourcesList.Add(1, Resources.ITProgramming);
            ResourcesList.Add(2, Resources.WebProgramming);
            ResourcesList.Add(3, Resources.WebDesign);
            ResourcesList.Add(4, Resources.MobileApps);
            ResourcesList.Add(5, Resources.ApplicationDevelopment);
            ResourcesList.Add(6, Resources.DesignMultimedia);
            ResourcesList.Add(7, Resources.GraphicDesign);
            ResourcesList.Add(8, Resources.Logos);
            ResourcesList.Add(9, Resources.Illustration);
            ResourcesList.Add(10, Resources.VideographyEditing);
            ResourcesList.Add(11, Resources.WritingTranslation);
            ResourcesList.Add(12, Resources.ArticleWriting);
            ResourcesList.Add(13, Resources.WebContent);
            ResourcesList.Add(14, Resources.Translation);
            ResourcesList.Add(15, Resources.EbooksBlogs);
            ResourcesList.Add(16, Resources.SalesMarketing);
            ResourcesList.Add(17, Resources.SearchOnlineMarketing);
            ResourcesList.Add(18, Resources.LeadGeneration);
            ResourcesList.Add(19, Resources.Advertising);
            ResourcesList.Add(20, Resources.Relemarketing);
            ResourcesList.Add(21, Resources.AdminSupport);
            ResourcesList.Add(22, Resources.DataEntry);
            ResourcesList.Add(23, Resources.VirtualAssistant);
            ResourcesList.Add(24, Resources.Research);
            ResourcesList.Add(25, Resources.Transcription);
        }

        public string GetItem(int index)
        {        
            return ResourcesList.SingleOrDefault(p => p.Key == index).Value;
        }
    }
}
