using TedXMLExport.XMLTedObject;

namespace LeerTedXML.XMLDocument
{
    internal interface IXMLTedParser
    {
        List<TED_EXPORT> XmlSingleFileObject();
        List<TED_EXPORT> XmlFolderFilesObjects();

    }
}
