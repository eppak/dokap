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
using System.IO;
using core;

namespace dokap.edit
{
    public partial class upload : core.page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                init(true, true, true);
                if (!uploaded.HasFile) { throw new Exception("No file selected"); }
                if (uploaded.PostedFile.ContentLength / 1024 > config.UPLOAD_MaxSize) { throw new Exception("Invalid file size"); }
                if (!config.UPLOAD_Extensions.Contains((new FileInfo(uploaded.PostedFile.FileName)).Extension.ToLower())) { throw new Exception("Invalid file extension"); };

                uploaded.SaveAs(config.appTmp + uploaded.PostedFile.FileName);
                ftp.putFile(currentDir() + uploaded.PostedFile.FileName, config.appTmp + uploaded.PostedFile.FileName);

                userMessage("File [" + uploaded.PostedFile.FileName + "] uploaded.");
                File.Delete(config.appTmp + uploaded.PostedFile.FileName);
            }
            catch (Exception ex) 
            {
                userMessage("Error: " + ex.Message);            
            }
        }
    }
}