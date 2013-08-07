using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace core
{
    public class config
    {

        public static bool loaded = false;
        public static string Security_Salt = "/";
        public static int Security_Loops = 1;

        public static string FTP_Server = "";
        public static string FTP_Username = "";
        public static string FTP_Password = "";
        public static string FTP_Root = "/";

        public static void load() {
            XmlDocument doc = new XmlDocument();
            doc.Load(appDataRootPath + "config.xml");

            Security_Salt = Convert.ToString(doc.SelectSingleNode("Config/security/@salt").InnerText);
            Security_Loops = Convert.ToInt16(doc.SelectSingleNode("Config/security/@loops").InnerText);

            FTP_Server = Convert.ToString(doc.SelectSingleNode("Config/ftp/@server").InnerText);
            FTP_Username = Convert.ToString(doc.SelectSingleNode("Config/ftp/@username").InnerText);
            FTP_Password = Convert.ToString(doc.SelectSingleNode("Config/ftp/@password").InnerText);
            FTP_Root = Convert.ToString(doc.SelectSingleNode("Config/ftp/@root").InnerText);

            loaded = true;
        }


        public static string appRootPath
        {
            get { return HttpContext.Current.Server.MapPath("~"); }
        }

        public static string appDataRootPath
        {
            get { return appRootPath + "App_Data\\"; }
        }


    }



    public class fsEntry
    {
        public string Name;
        public bool Include;
    }
}