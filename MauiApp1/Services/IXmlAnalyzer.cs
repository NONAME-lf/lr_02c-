namespace LibraryApp.Services;

// Інтерфейс (стратегія) для різних реалізацій аналізу
public interface IXmlAnalyzer
{
    void Analyze(string filePath, string keyword, List<string> results);
}