using System.Xml;
using System.Collections.Generic;

namespace LibraryApp.Services;

public class DOMAnalyzer : IXmlAnalyzer
{
    public void Analyze(string filePath, string keyword, List<string> results)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        var books = xmlDoc.SelectNodes("//book");
        if (books != null)
        {
            foreach (XmlNode book in books)
            {
                var allText = GetAllText(book);

                if (allText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    string author = book["author"]?.InnerText ?? "Unknown";
                    string title = book["title"]?.InnerText ?? "Unknown";

                    results.Add($"Book found: {title} by {author}. Details: {allText}");
                }
            }
        }
    }

    private string GetAllText(XmlNode node)
    {
        var texts = new List<string>();
        foreach (XmlNode child in node.ChildNodes)
        {
            texts.Add($"{child.Name}: {child.InnerText}");
        }
        return string.Join(", ", texts);
    }
}