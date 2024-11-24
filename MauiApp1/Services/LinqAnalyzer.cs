using System.Xml.Linq;
using System.Collections.Generic;

namespace LibraryApp.Services;

public class LINQAnalyzer : IXmlAnalyzer
{
    public void Analyze(string filePath, string keyword, List<string> results)
    {
        var xDoc = XDocument.Load(filePath);

        var books = xDoc.Descendants("book")
            .Where(book =>
                book.DescendantsAndSelf().Any(e => 
                    e.Value.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
            .Select(book => new
            {
                Author = book.Element("author")?.Value,
                Title = book.Element("title")?.Value,
                AdditionalInfo = string.Join(", ", book.Descendants()
                    .Where(e => e.Name != "author" && e.Name != "title")
                    .Select(e => $"{e.Name}: {e.Value}")
                )
            });

        foreach (var book in books)
        {
            results.Add($"Book found: {book.Title} by {book.Author}. Details: {book.AdditionalInfo}");
        }
    }
}