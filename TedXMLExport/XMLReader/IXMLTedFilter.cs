namespace LeerTedXML.XMLReader
{
    internal interface IXMLTedFilter
    {
        List<string> ReadXmlSingleFile();
        List<string> ReadXmlFolder();
    }
}
