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
using System.Web.Security;
using core;

namespace core
{
    public class auth
    {

        public static bool login(string username, string password, bool remember = true) 
        {
            if ((config.ADMIN_USERNAME == username && config.ADMIN_PASSWORD == password) ||
                (config.USERNAME == username && config.PASSWORD == password)) 
            {
                FormsAuthentication.SetAuthCookie(username, remember);
                return true;            
            }
            else
            {
                return false;            
            }
        }

        public static string user() { return HttpContext.Current.User.Identity.Name ;}

        public static bool isAdmin() { return config.ADMIN_USERNAME == user(); }

        public static bool isLogged() { return HttpContext.Current.User.Identity.IsAuthenticated; }

        public static void logOff()  { FormsAuthentication.SignOut(); }
    }
}