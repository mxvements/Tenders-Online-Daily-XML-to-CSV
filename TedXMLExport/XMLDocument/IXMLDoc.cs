using TedXMLExport.XMLTedObject;

namespace LeerTedXML.XMLDocument
{
    internal interface IXMLDoc
    {
        List<TED_EXPORT> XmlSingleFileObject();
        List<TED_EXPORT> XmlFolderFilesObjects();

    }
}
