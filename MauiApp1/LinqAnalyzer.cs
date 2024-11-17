using System.Xml.Linq;

public class LinqAnalyzer
{
    public string Analyze(string xmlPath)
    {
        try
        {
            var document = XDocument.Load(xmlPath);
            string result = "LINQ Analysis Result:\n";

            foreach (var element in document.Root.Elements())
            {
                result += $"Element: {element.Name}, Value: {element.Value}\n";
            }

            return result;
        }
        catch (Exception ex)
        {
            return $"Error in LINQ Analysis: {ex.Message}";
        }
    }
}