namespace LibraryApp.Services;

public interface IXmlAnalyzer
{
    void Analyze(string filePath, string keyword, List<string> results);
}