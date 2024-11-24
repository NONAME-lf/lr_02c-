using Microsoft.Maui.Controls;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Collections.Generic;
using LibraryApp.Services; // Простір імен аналізаторів

namespace LibraryApp;

public partial class MainPage : ContentPage
{
    private IXmlAnalyzer _analyzer;
    private string xmlFilePath;

    public MainPage()
    {
        InitializeComponent();
        analyzerPicker.SelectedIndexChanged += OnAnalyzerSelected;
    }

    private async void OnLoadXmlClicked(object sender, EventArgs e)
    {
        var file = await FilePicker.Default.PickAsync();
        if (!file.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Error", "Please, choose file of XML format.", "OK");
            return;
        }
        if (file != null)
        {
            xmlFilePath = file.FullPath;
            await DisplayAlert("File Loaded", $"Loaded file: {file.FileName}", "OK");
        }
    }

    private void OnAnalyzerSelected(object sender, EventArgs e)
    {
        switch (analyzerPicker.SelectedIndex)
        {
            case 0:
                _analyzer = new SAXAnalyzer();
                break;
            case 1:
                _analyzer = new DOMAnalyzer();
                break;
            case 2:
                _analyzer = new LINQAnalyzer();
                break;
        }
    }

    
    private async void AnalyzeButton_Clicked(object sender, EventArgs e)
    {
        if (_analyzer == null)
        {
            await DisplayAlert("Error", "Please select an analysis method.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(xmlFilePath))
        {
            await DisplayAlert("Error", "Please load an XML file first.", "OK");
            return;
        }

        string keyword = keywordEntry.Text?.Trim();
        if (string.IsNullOrEmpty(keyword))
        {
            await DisplayAlert("Error", "Please enter a keyword to search.", "OK");
            return;
        }

        var results = new List<string>();
        _analyzer.Analyze(xmlFilePath, keyword, results);

        resultsEditor.Text = results.Count > 0
            ? string.Join("\n", results)
            : "No results found for the given keyword.";
    }



    private void TransformButton_Clicked(object sender, EventArgs e)
        {
        string xmlPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Data", "library.xml");
        string xslPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Data", "library.xsl");
        string outputPath = Path.Combine(FileSystem.Current.AppDataDirectory, "output.html");

        XmlToHtmlTransformer transformer = new XmlToHtmlTransformer();
        transformer.TransformToHtml(xmlPath, xslPath, outputPath);
    }


    private void OnClearClicked(object sender, EventArgs e)
    {
        keywordEntry.Text = string.Empty;
        resultsEditor.Text = string.Empty;
    }
    
    private void OnExitButtonPressed(object sender, EventArgs e)
    {
        Device.BeginInvokeOnMainThread(async () =>
        {
            bool exit = await DisplayAlert("Exit", "Are you sure you want to exit?", "Yes", "No");
            if (exit)
            {
                Application.Current.Quit();
            }
        });
    }
}
