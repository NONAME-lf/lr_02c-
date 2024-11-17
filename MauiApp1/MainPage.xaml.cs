using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private string _xmlPath = "C:\\Users\\bigsh\\Documents\\.c#\\jettbrains\\MauiApp1\\MauiApp1\\library.xml";
    private string _xslPath = "C:\\Users\\bigsh\\Documents\\.c#\\jettbrains\\MauiApp1\\MauiApp1\\library.xsl";
    private string _outputHtmlPath = Path.Combine(AppContext.BaseDirectory, "library.html");
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnLoadXmlClicked(object sender, EventArgs e)
    {
        if (File.Exists(_xmlPath))
        {
            OutputLabel.Text = "XML file loaded successfully: " + _xmlPath;
        }
        else
        {
            OutputLabel.Text = "XML file not found. Please ensure it exists.";
        }
    }

    private void OnAnalyzeClicked(object sender, EventArgs e)
    {
        try
        {
            if (AnalysisPicker.SelectedItem == null)
            {
                OutputLabel.Text = "Please select an analysis method.";
                return;
            }

            if (!File.Exists(_xmlPath))
            {
                OutputLabel.Text = "XML file not found. Please load a valid file.";
                return;
            }

            string method = AnalysisPicker.SelectedItem.ToString();
            string result;

            switch (method)
            {
                case "SAX":
                    var saxAnalyzer = new SaxAnalyzer();
                    result = saxAnalyzer.Analyze(_xmlPath);
                    break;

                case "DOM":
                    var domAnalyzer = new DomAnalyzer();
                    result = domAnalyzer.Analyze(_xmlPath);
                    break;

                case "LINQ":
                    var linqAnalyzer = new LinqAnalyzer();
                    result = linqAnalyzer.Analyze(_xmlPath);
                    break;

                default:
                    result = "Unknown method selected.";
                    break;
            }

            OutputLabel.Text = result;
        }
        catch (Exception ex)
        {
            OutputLabel.Text = $"Error during analysis: {ex.Message}";
        }
    }

    private void OnTransformClicked(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(_xmlPath) || !File.Exists(_xslPath))
            {
                OutputLabel.Text = "Required files (XML or XSL) not found. Please check and try again.";
                return;
            }

            var transformer = new XsltTransformer();
            transformer.Transform(_xmlPath, _xslPath, _outputHtmlPath);
            OutputLabel.Text = "Transformation completed. HTML saved at " + _outputHtmlPath;
        }
        catch (Exception ex)
        {
            OutputLabel.Text = $"Error during transformation: {ex.Message}";
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        OutputLabel.Text = "Results cleared.";
        AnalysisPicker.SelectedIndex = -1;
    }
}