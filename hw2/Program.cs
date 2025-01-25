using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using hw2;
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
var tempHtml = await Load("https://shvirega.co.il/");
var splitHtml = new Regex("\\s+").Replace(tempHtml, " ");
var cleanHtml = new Regex("<(.*?)>").Split(splitHtml).Where(h => h.Length > 0).ToArray();
HtmlElement root = new HtmlElement { Name = "html", Attributes = new List<string>(), Classes = new List<string>(), Children = new List<HtmlElement>() };
HtmlElement currentElement = root;
var allTags = File.ReadAllLines("files/HtmlTags.json"); // רשימת תגיות HTML
var selfClosingTags = File.ReadAllLines("files/HtmlVoidTags.json"); // תגיות שלא דורשות סגירה
foreach (var c in cleanHtml)
{
    var trimmed = c.Trim();
    if (trimmed.StartsWith("/")) // תגית סוגרת
    {
        if (currentElement.Parent != null)
            currentElement = currentElement.Parent;  // עדכון לאלמנט האב
    }
    else if (allTags.Any(tag => trimmed.StartsWith(tag)))    // תגית פותחת
    {
        var parts = trimmed.Split(' ', 2);
        var tagName = parts[0];
        var newElement = new HtmlElement
        {
            Name = tagName,
            Attributes = new List<string>(),
            Classes = new List<string>(),
            Children = new List<HtmlElement>(),
            Parent = currentElement
        };
        if (parts.Length > 1)
        {
            var attributesPart = parts[1];
            var attributes = Regex.Matches(attributesPart, @"(\w+)=[""']([^""']+)[""']") // פענוח Attributes
                .Cast<Match>()
                .Select(m => $"{m.Groups[1].Value}=\"{m.Groups[2].Value}\"")
                .ToList();
            newElement.Attributes.AddRange(attributes);
            var classAttr = attributes.FirstOrDefault(attr => attr.StartsWith("class=")); // בדיקת מחלקות (class)
            if (!string.IsNullOrEmpty(classAttr))
            {
                var classValue = classAttr.Split('=')[1].Trim('"');
                newElement.Classes.AddRange(classValue.Split(' '));
            }
        }
        currentElement.Children.Add(newElement);
        if (!selfClosingTags.Contains(tagName) && !trimmed.EndsWith("/")) // אם התגית אינה self-closing, נכנסים לתוכה
            currentElement = newElement;
    }
    else if (!string.IsNullOrWhiteSpace(trimmed))
        currentElement.InnerHtml = (currentElement.InnerHtml ?? "") + trimmed;   // טקסט פנימי 
}