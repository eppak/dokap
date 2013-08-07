using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace core
{
    public class security
    {
        public static string sha1sum(string data) {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] hBytes = sha1.ComputeHash(bytes);
            return BytesToString(hBytes);
        }


        public static string sha1SumLoop(string data) {
            string res = data;
            for (int i = 1; i <= config.Security_Loops; i++) {
                res = sha1sum(res + config.Security_Salt);
            }
            return res;
        }


        private static string BytesToString(byte[] bytes)
        {
            var stringb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                stringb.Append(hex);
            }
            return stringb.ToString();
        }
    }
}