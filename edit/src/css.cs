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

namespace core
{
    public class css
    {
        public static string browserItem(string link, string name, string date, string icon)
        {
            return "<a href=\"" + link + "\" class=\"item\"><div class=\"name\">" + name + "</div><div class=\"date\"><div class=\"icon glyphicon " + icon + "\"></div>" + date + "</div></a>";
        }

        public static string bcItem(string b)
        {
            return "<li>" + b + "<span class=\"divider\">/</span></li>";
        }

        public static string appTitle()
        {
            return config.STR_APP_NAME + " - " + config.STR_APP_PAYOFF;
        }

        public static string appName() 
        {
            return config.STR_APP_NAME + " <span>" + config.STR_APP_PAYOFF + "<span>";
        }

        public static string appCopy()
        {
            return "<a href=\"" + config.STR_APP_LINK + "\" target=\"_blank\">" + config.STR_APP_NAME + " " + config.STR_APP_COPY + " [ver. " + config.STR_APP_VER + "." + config.STR_APP_BUILD + "]</a>";
        }

        public static string userMessageOK(string m) 
        {
            return "<div class=\"alert alert-success\">" + m +  "</div>";
        }

        public static string userMessageKO(string m)
        {
            return "<div class=\"alert alert-error\">" + m + "</div>";
        }


        public static string editableTextArea(string name, string id, string content)
        {

            return  "<div class=\"control-group\">" +
                        "<label class=\"control-label\" for=\"" + id + "\">" + name + "</label>" +
                        "<div class=\"controls\">" +
                           "<textarea name=\"" + id + "\" id=\"" + id + "\">" + content + "</textarea>" +
                        "</div>" +
                    "</div>";
        }


        public static string editablePre(string name, string id, string content)
        {

            return  "<div class=\"control-group\">" +
                        "<label class=\"control-label\" for=\"" + id + "\">" + name + "</label>" +
                        "<div class=\"controls\">" +
                           "<pre id=\"" + id + "\" style=\"height: 500px;\">" + content + "</pre>" +
                        "</div><input type=\"hidden\" name=\"source_value\" id=\"source_value\" value=\"\">" +
                    "</div>";
        }
    }
}