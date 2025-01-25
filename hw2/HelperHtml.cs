using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;

namespace hw2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;

        public string[] HtmlTags { get; set; }
        public string[] VoidHtmlTags { get; set; }

        private HtmlHelper()
        {
            HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("files/HtmlTags.json")) ?? new string[0];
            VoidHtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("files/HtmlVoidTags.json")) ?? new string[0];
        }
    }
}