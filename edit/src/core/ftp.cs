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
using FtpLib;

namespace core
{
    public class ftp
    {
        private static bool visibleItem(string name) {
            if (name.StartsWith(".")) { return false; }

            return true;
        }


        protected static FtpConnection openConn() {
            FtpConnection ftpClient = new FtpConnection(config.FTP_Server, config.FTP_Username, config.FTP_Password);
            ftpClient.Open();
            ftpClient.Login();

            return ftpClient;
        }



        public static List<fsItem> dir(string dirName)
        {
            List<fsItem> Res = new List<fsItem>();
            FtpConnection ftpClient = openConn();  
            ftpClient.SetCurrentDirectory(config.FTP_Root + dirName);

            foreach (FtpDirectoryInfo fd in ftpClient.GetDirectories())
                { 
                    if (visibleItem(fd.Name)) {
                         if (config.IncludeDir(utils.fixSlash(utils.fixSlash(dirName) + fd.Name))) 
                         {
                            Res.Add(new fsItem() {Name = fd.Name, isDirectory = true, lastModify = Convert.ToDateTime(fd.LastWriteTime) }); 
                         }
                    }
                }

            foreach (FtpFileInfo f in ftpClient.GetFiles())
                {
                    if (visibleItem(f.Name)) {
                        if (config.IncludeExt(f.Extension.ToLower()))
                        {
                            Res.Add(new fsItem() { Name = f.Name, isDirectory = false, lastModify = Convert.ToDateTime(f.LastWriteTime), editable = true, editors = config.editorsExt(f.Extension.ToLower()) });
                        }
                    }
                }

            ftpClient.Close();
            return Res;
        }


        public static void createDir(string d, string nd)
        {
            FtpConnection ftpClient = openConn();
            ftpClient.SetCurrentDirectory(config.FTP_Root + d);
            ftpClient.CreateDirectory(nd);
            ftpClient.Close();
        }

        public static void createFile(string d, string tf, string nf)
        {
            FtpConnection ftpClient = openConn();  
            ftpClient.SetCurrentDirectory(config.FTP_Root + d);
            ftpClient.PutFile(tf, nf);
            ftpClient.Close();
        }

        public static void getFile(string f, string l)
        {
            FtpConnection ftpClient = openConn();
            ftpClient.GetFile(config.FTP_Root + f, l, false);
            ftpClient.Close();
        }

        public static void putFile(string f, string l)
        {
            FtpConnection ftpClient = openConn();
            ftpClient.PutFile(l, config.FTP_Root + f);
            ftpClient.Close();
        }
    }



    public class fsItem
    {
        public string Name;
        public bool isDirectory;
        public DateTime lastModify;
        public bool editable = false;
        public string editors;
    }
}