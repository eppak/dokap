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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

namespace core
{
    public class page : System.Web.UI.Page
    {
        protected core.config _config;

        public core.config conf
        {
            get { loadConfig(); return _config; }
        }

        protected void loadConfig() 
        {
            if (!IsPostBack || !config.loaded) { config.load(); }
        }

        protected void checkSecutiry(bool fixUrl = true) 
        {
            if (currentDir(fixUrl) != "/" && Request.QueryString["s"] != security.sha1SumLoop(currentDir(fixUrl))) { throw new Exception("Security error. "); }
        }

        protected string currentDir(bool fixSlash = true)
        {
            if (Request.QueryString["d"] != null)
            {
                return (fixSlash) ? utils.fixSlash(Request.QueryString["d"]) : Request.QueryString["d"];
            }
            else
            {
                return "/";
            }
        }

        protected string currentFile()
        {
            if (Request.QueryString["d"] != null)
            {

                return Request.QueryString["d"];
            }
            else
            {
                return "/";
            }
        }

        protected string currentDirLink()
        {
            return ("d=" + currentDir() + "&s=" + security.sha1SumLoop(currentDir()));
        }

        protected string backDirLink()
        {
            return ("content.aspx?d=" + currentDir() + "&s=" + security.sha1SumLoop(currentDir()));
        }

        protected string backFileLink()
        {
            string d = currentFile().Replace((new FileInfo(currentFile())).Name, "");
            return ("content.aspx?d=" + d + "&s=" + security.sha1SumLoop(d));
        }

        protected string backDir()
        {
            string Res = "";
            for (int i = 1; i < currentDir().Split('/').Length - 2; i++)
            {
                Res += "/" + currentDir().Split('/')[i];
            }
            return Res + "/";
        }

        protected string breadcrumb()
        {
            string Res = "";
            foreach (string b in currentDir().Split('/'))
            {
                if (b != "") { Res += css.bcItem(b); }
            }
            return Res;
        }

        protected void userMessage(string m, bool error = false) 
        {
            ((Literal)this.Master.FindControl("userMessage")).Text = (error) ? css.userMessageKO(m) : css.userMessageOK(m);
        }

        protected void js(string m)
        {
            ((Literal)this.Master.FindControl("js")).Text = m;
        }

        protected void checkAuth()
        {
            if (!auth.isLogged()) { Response.Redirect("default.aspx", true); }        
        }

        protected void init(bool checkSecurity, bool fixUrl = true, bool needAuth = true) 
        {
            loadConfig();
            if (checkSecurity) { checkSecutiry(fixUrl); }
            if (needAuth) { checkAuth();  }
        }
    }
}