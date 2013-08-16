/*
doKap Copyright (c) 2013 Alessandro Cappellozza (alessandro.cappellozza@gmail.com)

The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of
the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,  WHETHER IN
AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
OR OTHER DEALINGS IN THE SOFTWARE.*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace core
{
    public class config
    {
        public const string STR_APP_NAME = "doKap";
        public const string STR_APP_LINK = "http://www.cappellozza.it";
        public const string STR_APP_PAYOFF = "CMS";
        public const string STR_APP_COPY = "&copy; Alessandro Cappellozza";
        public const string STR_APP_VER = "0.1";
        public const string STR_APP_BUILD = "2";

        public static bool loaded = false;
        public static string Security_Salt = "/";
        public static int Security_Loops = 1;

        public static string FTP_Server = "";
        public static string FTP_Username = "";
        public static string FTP_Password = "";
        public static string FTP_Root = "/";
        public static string APPURL = "";
        public static string RemoteURL = "";
        public static Hashtable ITEMS_Filter = null;

        public static int UPLOAD_MaxSize = 2048;
        public static List<string> UPLOAD_Extensions = null;


        public static void load(bool admin = true) {
            XmlDocument doc = new XmlDocument();
            string ftpElements = (admin) ? "Config/admin" : "Config/user";
            doc.Load(appDataRootPath + "config.xml");
            ITEMS_Filter = new Hashtable();

            Security_Salt = Convert.ToString(doc.SelectSingleNode("Config/security/@salt").InnerText);
            Security_Loops = Convert.ToInt16(doc.SelectSingleNode("Config/security/@loops").InnerText);

            FTP_Server = Convert.ToString(doc.SelectSingleNode("Config/ftp/@server").InnerText);
            FTP_Username = Convert.ToString(doc.SelectSingleNode("Config/ftp/@username").InnerText);
            FTP_Password = Convert.ToString(doc.SelectSingleNode("Config/ftp/@password").InnerText);
            FTP_Root = Convert.ToString(doc.SelectSingleNode("Config/ftp/@root").InnerText);
            RemoteURL = Convert.ToString(doc.SelectSingleNode("Config/ftp/@site").InnerText);
            APPURL = HttpContext.Current.Request.ApplicationPath;
            if (RemoteURL == "") { RemoteURL = APPURL; }

            UPLOAD_MaxSize = Convert.ToInt16(doc.SelectSingleNode("Config/upload/@maxsize").InnerText);
            UPLOAD_Extensions = Convert.ToString(doc.SelectSingleNode("Config/upload/@extensions").InnerText).Split('|').ToList<string>();

            foreach (XmlNode nd in doc.SelectNodes(ftpElements))
            {

                foreach (XmlNode n in nd.ChildNodes)
                {
                    string v = n.InnerText;
                    string t = (n.Attributes.Item(1).Value.ToLower() == "exclude") ? "0" : "1";

                    switch (n.Attributes.Item(0).Value.ToLower())
                        {
                            case "directory":
                                ITEMS_Filter.Add("dir://" + v, t);
                            break;

                            case "extension":
                                ITEMS_Filter.Add("ext://" + v, t);
                            break;
                        }
                }
            }

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

        public static string appTmp
        {
            get { return appDataRootPath + "tmp\\"; }
        }

        public static string appTempaltes
        {
            get { return appDataRootPath + "templates\\"; }
        }

        public static string appSnippet
        {
            get { return appDataRootPath + "snippet\\"; }
        }

        public static string appLogs
        {
            get { return appDataRootPath + "logs\\"; }
        }

        public static List<string> fileTemplates() 
        { 
            List<string> Res = new List<string>();        
            foreach(string f in Directory.GetFiles(config.appTempaltes, "file_*.*"))
            {
                Res.Add((new FileInfo(f)).Name);
            }
            return Res;
        }

        public static bool IncludeDir(string d) 
        {
            if (ITEMS_Filter.ContainsKey("dir://" + d))
            {
                return (Convert.ToString(ITEMS_Filter["dir://" + d]) == "1");
            }
            else
            { 
                if (ITEMS_Filter.ContainsKey("dir://*")) 
                {
                    return (Convert.ToString(ITEMS_Filter["dir://*"]) == "1");                     
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool IncludeExt(string e)
        {
            if (ITEMS_Filter.ContainsKey("ext://" + e))
            {
                return (Convert.ToString(ITEMS_Filter["ext://" + e]) == "1" || Convert.ToString(ITEMS_Filter["ext://" + e]).StartsWith("1:"));
            }
            else
            {
                if (ITEMS_Filter.ContainsKey("ext://*"))
                {
                    return (Convert.ToString(ITEMS_Filter["ext://*"]) == "1" || Convert.ToString(ITEMS_Filter["ext://*"]).StartsWith("1:"));
                }
                else
                {
                    return true;
                }
            }
        }

    }

    public class fsEntry
    {
        public string Name;
        public bool Include;
    }
}