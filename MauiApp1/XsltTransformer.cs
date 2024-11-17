using System.Xml.Xsl;

public class XsltTransformer
{
    public void Transform(string xmlPath, string xslPath, string outputHtmlPath)
    {
        var xslt = new XslCompiledTransform();
        xslt.Load(xslPath);
        xslt.Transform(xmlPath, outputHtmlPath);
    }
}