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

        public static List<fsItem> dir(string dirName)
        {
            List<fsItem> Res = new List<fsItem>();
            FtpConnection ftpClient = new FtpConnection(config.FTP_Server, config.FTP_Username, config.FTP_Password);

            ftpClient.Open(); 
            ftpClient.Login();      
            ftpClient.SetCurrentDirectory(dirName);

            foreach (FtpDirectoryInfo fd in ftpClient.GetDirectories())
                { 
                    if (visibleItem(fd.Name)) {
                                  Res.Add(new fsItem() {Name = fd.Name, isDirectory = true }); 
                    }
                }

            foreach (FtpFileInfo f in ftpClient.GetFiles())
                {
                    if (visibleItem(f.Name)) {
                        Res.Add(new fsItem() { Name = f.Name, isDirectory = false });
                    }
                }

            ftpClient.Close();
            return Res;
        }
    }



    public class fsItem
    {
        public string Name;
        public bool isDirectory;
    }
}