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
using System.Web.UI;
using System.Web.UI.WebControls;
using core;

namespace dokap.edit
{
    public partial class content : core.page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string Res = "";
            try
            {
                init(true, false, true);
                List<core.fsItem> itemsList = core.ftp.dir(currentDir());
                Res += css.browserItem("content.aspx?d=" + backDir() + "&s=" + security.sha1SumLoop(backDir()),"Back", "", "glyphicon-chevron-left");

                foreach (core.fsItem i in itemsList)
                {
                    if (i.isDirectory)
                    {
                        Res += css.browserItem("content.aspx?d=" + currentDir() + i.Name + "/&s=" + security.sha1SumLoop(currentDir() + i.Name + "/"), i.Name, i.lastModify.ToShortDateString(), "glyphicon-folder-open");
                    }

                }

                foreach (core.fsItem i in itemsList)
                {
                    if (!i.isDirectory)
                    {
                        string link = (i.editable) ? "modify.aspx?e=" + i.editors + "&d=" + currentDir() + i.Name + "&s=" + security.sha1SumLoop(currentDir() + i.Name) : "#";
                        Res += css.browserItem(link, i.Name, i.lastModify.ToShortDateString(), "glyphicon-list-alt");
                    }
                }
            }
            catch (Exception ex) {
                Res = css.userMessageKO(ex.Message);
            }

            Items.Text = Res;            
        }
    }
}