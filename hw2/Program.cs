////using System.Collections.Generic;
////using System.Text.Json;
////using System.Text.RegularExpressions;
////using hw2;
////async Task<string> Load(string url)
////{
////    HttpClient client = new HttpClient();
////    var response = await client.GetAsync(url);
////    var html = await response.Content.ReadAsStringAsync();
////    return html;
////}
////var tempHtml = await Load("https://shvirega.co.il/");
////var splitHtml = new Regex("\\s+").Replace(tempHtml, " ");
////var cleanHtml = new Regex("<(.*?)>").Split(splitHtml).Where(h => h.Length > 0).ToArray();
////HtmlElement root = new HtmlElement { Name = "html", Attributes = new List<string>(), Classes = new List<string>(), Children = new List<HtmlElement>() };
////HtmlElement currentElement = root;
////var allTags = File.ReadAllLines("files/HtmlTags.json"); // רשימת תגיות HTML
////var selfClosingTags = File.ReadAllLines("files/HtmlVoidTags.json"); // תגיות שלא דורשות סגירה
////foreach (var c in cleanHtml)
////{
////    var trimmed = c.Trim();
////    if (trimmed.StartsWith("/")) // תגית סוגרת
////    {
////        if (currentElement.Parent != null)
////            currentElement = currentElement.Parent;  // עדכון לאלמנט האב
////    }
////    else if (allTags.Any(tag => trimmed.StartsWith(tag)))    // תגית פותחת
////    {
////        var parts = trimmed.Split(' ', 2);
////        var tagName = parts[0];
////        var newElement = new HtmlElement
////        {
////            Name = tagName,
////            Attributes = new List<string>(),
////            Classes = new List<string>(),
////            Children = new List<HtmlElement>(),
////            Parent = currentElement
////        };
////        if (parts.Length > 1)
////        {
////            var attributesPart = parts[1];
////            var attributes = Regex.Matches(attributesPart, @"(\w+)=[""']([^""']+)[""']") // פענוח Attributes
////                .Cast<Match>()
////                .Select(m => $"{m.Groups[1].Value}=\"{m.Groups[2].Value}\"")
////                .ToList();
////            newElement.Attributes.AddRange(attributes);
////            var classAttr = attributes.FirstOrDefault(attr => attr.StartsWith("class=")); // בדיקת מחלקות (class)
////            if (!string.IsNullOrEmpty(classAttr))
////            {
////                var classValue = classAttr.Split('=')[1].Trim('"');
////                newElement.Classes.AddRange(classValue.Split(' '));
////            }
////        }
////        currentElement.Children.Add(newElement);
////        if (!selfClosingTags.Contains(tagName) && !trimmed.EndsWith("/")) // אם התגית אינה self-closing, נכנסים לתוכה
////            currentElement = newElement;
////    }
////    else if (!string.IsNullOrWhiteSpace(trimmed))
////        currentElement.InnerHtml = (currentElement.InnerHtml ?? "") + trimmed;   // טקסט פנימי 
////}

////using System.Collections.Generic;
////using System.Text.RegularExpressions;
////using System.Linq;
////using System.Net.Http;
////using System.Threading.Tasks;
////using hw2;
////// פונקציה לטעינת HTML מכתובת URL
////async Task<string> Load(string url)
////{
////    HttpClient client = new HttpClient();
////    var response = await client.GetAsync(url);
////    var html = await response.Content.ReadAsStringAsync();
////    return html;
////}

////// טעינת ה-HTML
////var tempHtml = await Load("https://shvirega.co.il/");
////var splitHtml = new Regex("\\s+").Replace(tempHtml, " ");
////var cleanHtml = new Regex("<(.*?)>").Split(splitHtml).Where(h => h.Length > 0).ToArray();

////// יצירת אלמנט ראשי
////HtmlElement root = new HtmlElement { Name = "html", Attributes = new List<string>(), Classes = new List<string>(), Children = new List<HtmlElement>() };
////HtmlElement currentElement = root;

////// קריאה לרשימות תגים
////var allTags = HtmlHelper.Instance.TagsInHtml; // רשימת תגיות HTML
////var selfClosingTags = HtmlHelper.Instance.DoNotRequireClosingTags; // תגיות שלא דורשות סגירה

////// עיבוד ה-HTML
////foreach (var c in cleanHtml)
////{
////    var trimmed = c.Trim();
////    if (trimmed.StartsWith("/")) // תגית סוגרת
////    {
////        if (currentElement.Parent != null)
////            currentElement = currentElement.Parent;  // עדכון לאלמנט האב
////    }
////    else if (allTags.Any)
////    {
////        var parts = trimmed.Split(' ', 2);
////        var tagName = parts[0];
////        var newElement = new HtmlElement(tagName, parts.Length > 1 ? parts[1] : string.Empty, currentElement);

////        if (parts.Length > 1)
////        {
////            var attributesPart = parts[1];
////            var attributes = Regex.Matches(attributesPart, @"(\w+)=[""']([^""']+)[""']") // פענוח Attributes
////                .Cast<Match>()
////                .Select(m => $"{m.Groups[1].Value}=\"{m.Groups[2].Value}\"")
////                .ToList();
////            newElement.Attributes.AddRange(attributes);

////            var classAttr = attributes.FirstOrDefault(attr => attr.StartsWith("class=")); // בדיקת מחלקות (class)
////            if (!string.IsNullOrEmpty(classAttr))
////            {
////                var classValue = classAttr.Split('=')[1].Trim('"');
////                newElement.Classes.AddRange(classValue.Split(' '));
////            }
////        }

////        currentElement.Children.Add(newElement);

////        // אם התגית אינה self-closing, נכנסים לתוכה
////        if (!selfClosingTags.Contains(tagName) && !trimmed.EndsWith("/"))
////            currentElement = newElement;
////    }
////    else if (!string.IsNullOrWhiteSpace(trimmed))
////    {
////        // טקסט פנימי
////        currentElement.InnerHtml = (currentElement.InnerHtml ?? "") + trimmed;
////    }
////}

////// חיפוש אלמנטים תואמים
////Selector selector = Selector.ConvertQueryToObject("div img");
////var result = root.SearchFitElements(selector);

////// הדפסת אבות האלמנט הראשון
////foreach (var ancestor in result.First().Ancestors())
////{
////    Console.WriteLine(ancestor.Name);
////}

////// פונקציה לטעינת HTML מכתובת URL
////static async Task<string> Load(string url)
////{
////    HttpClient client = new HttpClient();
////    var response = await client.GetAsync(url);
////    var html = await response.Content.ReadAsStringAsync();
////    return html;
////}
//using HtmlSerializer;
//using hw2;
//using System.Text.RegularExpressions;

//var html = await Load("https://learn.malkabruk.co.il/");

//var cleanHtml = new Regex("\\s").Replace(html, " ");

//var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(i => i.Length > 0 && i[0] != ' ').ToList();

//HtmlElement root = new HtmlElement("html", htmlLines[1].Substring(htmlLines[1].IndexOf(" ") + 1), null);
//HtmlElement currentElement = root;

//htmlLines = htmlLines.GetRange(2, htmlLines.Count() - 2);

//foreach (string line in htmlLines)
//{
//    if (line[0] == '/')
//        if (line.Substring(1) == currentElement.Name)
//            currentElement = currentElement.Parent;
//        else
//        {
//            if (currentElement.InnerHtml == null)
//                currentElement.InnerHtml = "";
//            currentElement.InnerHtml += line;
//        }
//    else
//    {
//        var spaceIndex = line.IndexOf(" ");
//        string tagName;
//        if (spaceIndex >= 0)
//            tagName = line.Substring(0, spaceIndex);
//        else
//            tagName = line.Substring(0, line.Length);
//        if (HtmlHelper.Instance.TagsInHtml.Contains(tagName))
//        {
//            HtmlElement newElement = new HtmlElement(tagName, line.Substring(line.IndexOf(" ") + 1), currentElement);
//            if (currentElement.Children == null)
//                currentElement.Children = new List<HtmlElement>();
//            currentElement.Children.Add(newElement);
//            if (!HtmlHelper.Instance.DoNotRequireClosingTags.Contains(tagName))
//                currentElement = newElement;
//        }
//        else
//        {
//            if (currentElement.InnerHtml == null)
//                currentElement.InnerHtml = "";
//            currentElement.InnerHtml += line;
//        }
//    }
//}

//Selector selector = Selector.ConvertQueryToObject("div img");
//var result = root.SearchFitElements(selector);

//static async Task<string> Load(string url)
//{
//    HttpClient client = new HttpClient();
//    var response = await client.GetAsync(url);
//    var html = await response.Content.ReadAsStringAsync();
//    return html;
//}

//foreach (var ancestor in result.First().Ancestors())
//{
//    Console.WriteLine(ancestor.Name);
//}

using hw2;
using System.Text.RegularExpressions;

var html = await Load("https://learn.malkabruk.co.il/");

var cleanHtml = new Regex("\\s").Replace(html, " ");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(i => i.Length > 0 && i[0] != ' ').ToList();

HtmlElement root = new HtmlElement("html", htmlLines[1].Substring(htmlLines[1].IndexOf(" ") + 1), null);
HtmlElement currentElement = root;

htmlLines = htmlLines.GetRange(2, htmlLines.Count() - 2);

foreach (string line in htmlLines)
{
    if (line[0] == '/')
        if (line.Substring(1) == currentElement.Name)
            currentElement = currentElement.Parent;
        else
        {
            if (currentElement.InnerHtml == null)
                currentElement.InnerHtml = "";
            currentElement.InnerHtml += line;
        }
    else
    {
        var spaceIndex = line.IndexOf(" ");
        string tagName;
        if (spaceIndex >= 0)
            tagName = line.Substring(0, spaceIndex);
        else
            tagName = line.Substring(0, line.Length);
        if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
        {
            HtmlElement newElement = new HtmlElement(tagName, line.Substring(line.IndexOf(" ") + 1), currentElement);
            if (currentElement.Children == null)
                currentElement.Children = new List<HtmlElement>();
            currentElement.Children.Add(newElement);
            if (!HtmlHelper.Instance.VoidHtmlTags.Contains(tagName))
                currentElement = newElement;
        }
        else
        {
            if (currentElement.InnerHtml == null)
                currentElement.InnerHtml = "";
            currentElement.InnerHtml += line;
        }
    }
}

Selector selector = Selector.ConvertQueryToObject("div img");
var result = root.SearchFitElements(selector);

static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

foreach (var ancestor in result.First().Ancestors())
{
    Console.WriteLine(ancestor.Name);
}
