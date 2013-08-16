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
using System.IO;
using HtmlAgilityPack;

namespace editors
{
    public class htmlparser
    {
        private const string TAG_Editable = "dokap-editable";
        private const string TAG_ItemID = "dokap-id-";
        private const string TAG_ItemType = "dokap-type-";
        private const string TAG_ItemName = "dokap-name-";

        private static string loadFile(string fileName) 
        {
            return File.ReadAllText(fileName);
        }

        private static void rawFixPHPTags(string fileName)
        {
            // HTML Agility Pack PHP TAG miss interpretation
            File.WriteAllText(fileName, File.ReadAllText(fileName).Replace("?=\"\"?>", "?>"));
        }
        
        public static Dictionary<string, editableItem> parse(string fileName) 
        {
            Dictionary<string, editableItem> Res = new Dictionary<string, editableItem>();                    
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(loadFile(fileName));
            HtmlNodeCollection hc =  doc.DocumentNode.SelectNodes("//*[contains(@class, '" + TAG_Editable + "')]");
            if (hc == null) {throw new Exception("No tag found, switch to source to fix.");}

           foreach (HtmlNode h in hc)
            {
                foreach(HtmlAttribute a in h.Attributes)
                {
                    if (a.Name.ToLower() == "class") {
                        string itemID = "";
                        string itemType = "";
                        string itemName = "";
                        foreach (string c in a.Value.Split(' '))
                            { 
                                if (c.ToLower().StartsWith(TAG_ItemID)) { itemID = c; }
                                if (c.ToLower().StartsWith(TAG_ItemType)) { itemType = c.ToLower().Replace(TAG_ItemType, ""); }
                                if (c.ToLower().StartsWith(TAG_ItemName)) { itemName = c.ToLower().Replace(TAG_ItemName, "").Replace("_", " "); }
                            }

                        if (h.InnerHtml.ToLower().Contains(TAG_Editable)) { throw new Exception("Nested editable tags");}
                        if (itemID == "") { throw new Exception("Unnamed element"); }
                        if (itemType == "") { itemType = "textarea"; }
                        if (itemName == "") { itemName = itemID; }
                        Res.Add(itemID, new editableItem() { id = itemID, name = itemName, type = itemType, classes = a.Value, content = HttpUtility.HtmlEncode(h.InnerHtml) });
                    }
                }
            }

            return Res;
        }

        public static void save(string fileName, Dictionary<string, editableItem> content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(loadFile(fileName));

            foreach (string k in content.Keys)
            {
                foreach (HtmlNode h in doc.DocumentNode.SelectNodes("//*[contains(@class, '" + content[k].id + "')]"))
                {
                    h.InnerHtml = content[k].content;
                }
            }

            doc.Save(fileName);
            rawFixPHPTags(fileName);
        }
    }
}
