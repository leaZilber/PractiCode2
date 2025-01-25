using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hw2
{
    public class HtmlElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {

        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> htmlElementsQueue = new Queue<HtmlElement>();
            HashSet<HtmlElement> visitedElements = new HashSet<HtmlElement>(); 
            if (this.Children != null)
            {
                foreach (var child in this.Children)
                {
                    htmlElementsQueue.Enqueue(child);
                    visitedElements.Add(child);  
                }
            }
            while (htmlElementsQueue.Count > 0)
            {
                HtmlElement htmlElement = htmlElementsQueue.Dequeue();
                yield return htmlElement;

                if (htmlElement.Children != null)
                {
                    foreach (var child in htmlElement.Children)
                    {
                        if (!visitedElements.Contains(child)) 
                        {
                            htmlElementsQueue.Enqueue(child);
                            visitedElements.Add(child); 
                        }
                    }
                }
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
}
