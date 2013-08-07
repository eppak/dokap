using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using core;

namespace dokap.edit
{
    public partial class content : System.Web.UI.Page
    {


        private string fixSlash(string dir) {
            return (dir.EndsWith("/")) ? dir : dir + "/";
        }

        protected string currentDir() {
            if (Request.QueryString["d"] != null)
            {

                return fixSlash(Request.QueryString["d"]);
            }
            else
            {
                return "/";
            }
        }

        protected string backDir(){
           string Res = "";
           for (int i = 1; i < currentDir().Split('/').Length - 2; i++) 
            {
                Res += "/" + currentDir().Split('/')[i];
            }
           return Res + "/";
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string Res = "";

            try
            {
                if (!IsPostBack || !config.loaded) { config.load(); }
                if (currentDir() != "/" && Request.QueryString["s"] != security.sha1SumLoop(currentDir())) { throw new Exception("Security error."); }

                List<core.fsItem> itemsList = core.ftp.dir(currentDir());
                Res += makeItem("content.aspx?d=" + backDir() + "&s=" + security.sha1SumLoop(backDir()), "", "glyphicon-chevron-left");

                foreach (core.fsItem i in itemsList)
                {
                    if (i.isDirectory)
                    {
                        Res += makeItem("content.aspx?d=" + currentDir() + i.Name + "/&s=" + security.sha1SumLoop(currentDir() + i.Name + "/"), i.Name, "glyphicon-folder-open");
                    }

                }

                foreach (core.fsItem i in itemsList)
                {
                    if (!i.isDirectory)
                    {
                        Res += makeItem("edit.aspx?f=" + currentDir() + i.Name + "&s=" + security.sha1sum(currentDir() + i.Name), i.Name, "glyphicon-list-alt");
                    }

                }
            }
            catch (Exception ex) {
                Res = ex.Message;
            }

            Items.Text = Res;            
        }

        protected string makeItem(string link, string name, string icon) {
            return "<a href=\"" + link + "\" class=\"item\"><span class=\"glyphicon " + icon + "\"></span>" + name + "</a>";
        }
    }
}