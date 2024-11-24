using System.Xml;
using System.Collections.Generic;

namespace LibraryApp.Services;

public class SAXAnalyzer : IXmlAnalyzer
{
    public void Analyze(string filePath, string keyword, List<string> results)
    {
        using (var reader = XmlReader.Create(filePath))
        {
            string? currentAuthor = null;
            string? currentTitle = null;
            string currentDetails = "";
            bool isBookStarted = false;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "book")
                    {
                        // Початок книги: скидаємо всі дані
                        isBookStarted = true;
                        currentAuthor = null;
                        currentTitle = null;
                        currentDetails = "";
                    }
                    else if (reader.Name == "author")
                    {
                        currentAuthor = reader.ReadInnerXml();
                    }
                    else if (reader.Name == "title")
                    {
                        currentTitle = reader.ReadInnerXml();
                    }
                    else if (isBookStarted)
                    {
                        // Накопичуємо деталі книги й переходимо до вмісту
                        string elementName = reader.Name;
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            currentDetails += $"{elementName}: {reader.Value}, ";
                        }
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "book")
                {
                    // Завершення книги: шукаємо ключове слово
                    if (!string.IsNullOrEmpty(currentAuthor) && currentAuthor.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        !string.IsNullOrEmpty(currentTitle) && currentTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        currentDetails.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add($"Book found: {currentTitle} by {currentAuthor}. Details: {currentDetails.TrimEnd(',', ' ')}");
                    }

                    // Скидаємо стан книги
                    isBookStarted = false;
                }
            }
        }
    }
}
