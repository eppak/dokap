﻿/*
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
using System.IO;
using editors;
using core;

namespace editors
{
    public class htmleditor : editor, IEditor
    {
        public htmleditor(string remoteFile, string localFile) 
        {
            _remoteFile = remoteFile;
            _localFile = config.appTmp + localFile;
        }

        string IEditor.render(bool getFile)
        {
            string Res = "";
            if (getFile) { remoteLoad(_remoteFile,  _localFile); }

            Dictionary<string, editableItem> elements = htmlparser.parse(_localFile);
            foreach (string element in elements.Keys)
            {
                Res += css.editableTextArea(elements[element].name, elements[element].id, HttpUtility.HtmlDecode(elements[element].content));
            }

            return Res;
        }

        string IEditor.save(HttpRequest data)
        {
            Dictionary<string, editableItem> elements = htmlparser.parse(_localFile);

            foreach (string element in elements.Keys)
            {
                elements[element].content = data.Form[element];
            }

            htmlparser.save( _localFile, elements);
            remoteSave(_remoteFile, _localFile);
            return ((IEditor)this).render(false); ;
        }


        string IEditor.js()
        {
            return File.ReadAllText(config.appSnippet + "htmleditor.html");
        }
    }
}