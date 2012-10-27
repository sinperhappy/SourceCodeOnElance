using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace UI.handler
{
    /// <summary>
    /// AjaxuploadHandler 的摘要说明
    /// </summary>
    public class AjaxuploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                string path = context.Server.MapPath("~/Temp");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var file = context.Request.Files[0];
                string fileName;
                if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
                {
                    string[] files = file.FileName.Split(new char[] { '\\' });
                    fileName = files[files.Length - 1];
                }
                else
                {
                    fileName = file.FileName;
                }

                string msg = "";
                string strFileName = fileName;
                if (string.IsNullOrEmpty(strFileName))
                {
                    msg = "{";
                    msg += string.Format("error:'{0}',\n", @Internationalization.Resources.ChoseFile);
                    msg += string.Format("msg:'{0}'\n", string.Empty);
                    msg += "}";
                }
                else
                {
                    string fileType = Path.GetExtension(fileName).ToLower();
                    fileName = DateTime.Now.Ticks.ToString() + fileType;
                    string filePath = Path.Combine(path, fileName);
                    file.SaveAs(filePath);

                    msg = "{";
                    msg += string.Format("error:'{0}',\n", string.Empty);
                    msg += string.Format("msg:'{0}'\n", fileName);
                    msg += "}";
                }

                context.Response.Write(msg);
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}