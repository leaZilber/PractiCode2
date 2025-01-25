
using hw2;
namespace hw2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Children { get; set; }

        public Selector(string htmlQuery, Selector parent)
        {
            Classes = new List<string>();

            int indexId = htmlQuery.IndexOf('#');
            int indexClass = htmlQuery.IndexOf('.');

            if (htmlQuery[0] != '#' && htmlQuery[0] != '.')
            {
                string tagName;
                if (indexId >= 0)
                    tagName = htmlQuery.Substring(0, indexId);
                else if (indexClass > 0)
                    tagName = htmlQuery.Substring(0, indexClass);
                else
                    tagName = htmlQuery;

                if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
                    this.TagName = tagName;
            }

            if (indexId >= 0)
            {
                if (indexClass >= 0)
                    Id = htmlQuery.Substring(indexId + 1, (indexClass - (indexId + 1)));
                else
                    Id = htmlQuery.Substring(indexId + 1, htmlQuery.Length - indexId);
            }

            if (indexClass >= 0)
                Classes = htmlQuery.Substring(indexClass + 1).Split('.').ToList();

            this.Parent = parent;
        }

        public static Selector ConvertQueryToObject(string htmlQuery)
        {
            string[] queryParts = htmlQuery.Split(' ');
            Selector rootSelector = new Selector(queryParts[0], null);
            Selector currentSelector = rootSelector;

            queryParts = queryParts.Skip(1).ToArray();
            foreach (string part in queryParts)
            {
                currentSelector.Children = new Selector(part, currentSelector);
                currentSelector = currentSelector.Children;
            }

            return rootSelector;
        }

        public override bool Equals(object? obj)
        {
            if (obj is HtmlElement element)
            {
                if ((this.Id == null || element.Id == this.Id) && (this.TagName == null || element.Name == this.TagName))
                {
                    if (this.Classes != null)
                    {
                        if (element.Classes == null)
                            return false;
                        foreach (var c in this.Classes)
                        {
                            if (!element.Classes.Contains(c))
                                return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }

}
