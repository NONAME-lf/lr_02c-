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
                throw new FileNotFoundException($"XML file not found: {xmlFilePath}");
            }

            if (!File.Exists(xslFilePath))
            {
                throw new FileNotFoundException($"XSL file not found: {xslFilePath}");
            }

            // Виконання трансформації
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            xslTransform.Load(xslFilePath); // Завантаження XSL-файлу
            xslTransform.Transform(xmlFilePath, outputHtmlPath); // Трансформація у HTML

            // Підтвердження завершення
            App.Current.MainPage.DisplayAlert("Success", $"File transformed into HTML: {outputHtmlPath}", "OK");
        }
        catch (Exception ex)
        {
            // Відображення помилки
            App.Current.MainPage.DisplayAlert("Error", $"During the transformation occured error: {ex.Message}", "OK");
        }
    }
}
