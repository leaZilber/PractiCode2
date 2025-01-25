using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace hw2
{
    public class HelperHtml
    {
        private readonly static HelperHtml _instence;
        public static HelperHtml Instence => _instence;
        public string[] json1 { get; set; }
        public string[] json2 { get; set; }
        private HelperHtml()
        {
            json1 = JsonSerializer.Deserialize<string[]>(File.ReadAllText("files/HtmlTags.json")) ?? new string[0];
            json2 = JsonSerializer.Deserialize<string[]>(File.ReadAllText("files/HtmlVoidTags.json")) ?? new string[0];
        }
    }
}