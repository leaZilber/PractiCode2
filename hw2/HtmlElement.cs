using System.Text.RegularExpressions;

public class HtmlElement
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> Attributes { get; set; }
    public List<string> Classes { get; set; }
    public string InnerHtml { get; set; }
    public HtmlElement Parent { get; set; }
    public List<HtmlElement> Children { get; set; }

    public HtmlElement(string name, string attributes, HtmlElement parent)
    {
        Name = name;
        Attributes = new List<string>();
        Classes = new List<string>();
        var attributesMatch = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(attributes).ToList();

        foreach (var attributeMatch in attributesMatch)
        {
            string attribute = attributeMatch.Value;
            string attributeName = attribute.Substring(0, attribute.IndexOf('='));
            string value = attribute.Substring(attribute.IndexOf("\"") + 1);
            value = value.Substring(0, value.LastIndexOf("\""));

            if (attributeName == "id")
                Id = value;
            else if (attributeName == "class")
                Classes = value.Split(" ").ToList();
            else
                Attributes.Add(attribute);
        }

        Parent = parent;
    }

    public IEnumerable<HtmlElement> Descendants()
    {
        Queue<HtmlElement> htmlElementsQueue = new Queue<HtmlElement>();

        if (this.Children != null)
            foreach (var child in this.Children)
                htmlElementsQueue.Enqueue(child);

        while (htmlElementsQueue.Count > 0)
        {
            HtmlElement htmlElement = htmlElementsQueue.Dequeue();
            yield return htmlElement;

            if (htmlElement.Children != null)
                foreach (var child in htmlElement.Children)
                    htmlElementsQueue.Enqueue(child);
        }
    }

    public IEnumerable<HtmlElement> Ancestors()
    {
        HtmlElement htmlElement = this.Parent;
        while (htmlElement != null)
        {
            yield return htmlElement;
            htmlElement = htmlElement.Parent;
        }
    }
}
