namespace LeerTedXML.XMLReader
{
    internal interface IXMLTedFilter
    {
        List<string> XmlSingleFileReader();
        List<string> ReadXmlFolder();
    }
}
