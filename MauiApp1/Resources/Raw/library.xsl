<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:template match="/">
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <title>Library Catalog</title>
                <style>
                    table { width: 100%; border-collapse: collapse; }
                    th, td { border: 1px solid black; padding: 8px; text-align: left; }
                    th { background-color: #f2f2f2; }
                </style>
            </head>
            <body>
                <h1>Library Books</h1>
                <table>
                    <tr>
                        <th>Author</th>
                        <th>Title</th>
                        <th>Annotation</th>
                        <th>Qualification</th>
                        <th>Reader Name</th>
                        <th>Faculty</th>
                        <th>Department</th>
                        <th>Position</th>
                    </tr>
                    <xsl:for-each select="library/book">
                        <tr>
                            <td><xsl:value-of select="author"/></td>
                            <td><xsl:value-of select="title"/></td>
                            <td><xsl:value-of select="annotation"/></td>
                            <td><xsl:value-of select="qualification"/></td>
                            <td><xsl:value-of select="reader/name"/></td>
                            <td><xsl:value-of select="reader/faculty"/></td>
                            <td><xsl:value-of select="reader/department"/></td>
                            <td><xsl:value-of select="reader/position"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
