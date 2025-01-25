using System;
using System.Collections.Generic;
using System.Linq;

namespace hw2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        public Selector()
        {
            Classes = new List<string>();
        }
        public static Selector CastingQuery(string query)
        {
            string[] splitQuery = query.Split(" "); 
            Selector root = new Selector();
            Selector currentSelector = root; 
            foreach (string sq in splitQuery)
            {
                Selector newSelector = new Selector();
                if (currentSelector != null)
                {
                    currentSelector.Child = newSelector;
                    newSelector.Parent = currentSelector;
                }
                string[] segments = sq.Split(new[] { '#', '.' }, StringSplitOptions.None);
                if (segments[0].Length > 0 && IsValidHtmlTag(segments[0]))
                    newSelector.TagName = segments[0]; 
                if (sq.Contains('#'))
                    newSelector.Id = segments.FirstOrDefault(segment => segment.StartsWith("#"))?.Substring(1);
                if (sq.Contains('.')) 
                    newSelector.Classes = segments.Skip(1).Select(cls => cls).ToList(); 
                currentSelector = newSelector;
            }
            return root;
        }
        private static bool IsValidHtmlTag(string tag)
        {
            string[] validTags = File.ReadAllLines("files/HtmlTags");
            return validTags.Contains(tag.ToLower());
        }
    }
}
