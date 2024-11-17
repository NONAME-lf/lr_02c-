using System.Xml;

public class SaxAnalyzer
{
    public string Analyze(string xmlPath)
    {
        try
        {
            using var reader = XmlReader.Create(xmlPath);
            string result = "SAX Analysis Result:\n";

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    result += $"Element: {reader.Name}\n";
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            return $"Error in SAX Analysis: {ex.Message}";
        }
    }
}