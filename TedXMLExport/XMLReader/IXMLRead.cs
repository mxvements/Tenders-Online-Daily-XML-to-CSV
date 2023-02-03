namespace LeerTedXML.XMLReader
{
    internal interface IXMLRead
    {
        List<string> XmlSingleFileReader();
        List<string> ReadXmlFolder();
    }
}
