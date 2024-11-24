using System.Xml.Xsl;
using MauiApp1;

namespace LibraryApp;

using System;
using System.IO;
using System.Xml.Xsl;

public class XmlToHtmlTransformer
{
    public void TransformToHtml(string xmlFilePath, string xslFilePath, string outputHtmlPath)
    {
        try
        {
            // Перевірка наявності файлів
            if (!File.Exists(xmlFilePath))
            {
                throw new FileNotFoundException($"XML файл не знайдено: {xmlFilePath}");
            }

            if (!File.Exists(xslFilePath))
            {
                throw new FileNotFoundException($"XSL файл не знайдено: {xslFilePath}");
            }

            // Виконання трансформації
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            xslTransform.Load(xslFilePath); // Завантаження XSL-файлу
            xslTransform.Transform(xmlFilePath, outputHtmlPath); // Трансформація у HTML

            // Підтвердження завершення
            App.Current.MainPage.DisplayAlert("Успіх", $"Файл перетворено в HTML: {outputHtmlPath}", "OK");
        }
        catch (Exception ex)
        {
            // Відображення помилки
            App.Current.MainPage.DisplayAlert("Помилка", $"Під час трансформації виникла помилка: {ex.Message}", "OK");
        }
    }
}
