using System.Xml;

public class DomAnalyzer
{
    public string Analyze(string xmlPath)
    {
        try
        {
            var document = new XmlDocument();
            document.Load(xmlPath);

            string result = "DOM Analysis Result:\n";
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                result += $"Node: {node.Name}, Value: {node.InnerText}\n";
            }

            return result;
        }
        catch (Exception ex)
        {
            return $"Error in DOM Analysis: {ex.Message}";
        }
    }
}