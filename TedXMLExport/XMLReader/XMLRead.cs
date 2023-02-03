using System.Reflection;
using System.Xml;
using TedXMLExport.Menu;

namespace LeerTedXML.XMLReader
{
    internal class XMLRead : IXMLRead
    {
        //TODO Change class name to XmlTedFilter
        public List<string> XmlSingleFileReader()
        {
            List<string> xml_file_list = new List<string>();

            string dirPath = SetPath("XML Path to read:");

            if(FilterXMLFiles(dirPath)) { xml_file_list.Add(dirPath); }

            return xml_file_list;

        }

        public List<string> ReadXmlFolder()
        {
            List<String> xml_file_list = new List<String>();

            string folder_path = SetDirectory("FOLDER Path to read:");
            IEnumerable<string> folder_path_list = Directory.EnumerateFiles(folder_path, "*.xml");

            foreach (string file_path in Directory.EnumerateFiles(folder_path, "*.xml"))
            {
                if (FilterXMLFiles(file_path))
                {
                    xml_file_list.Add(file_path);
                }

            }

            ConsoleUtils.WriteLineEventColor($"EVENT Folder read, {xml_file_list.Count} files added to parser");
            return xml_file_list;
        }

        private string SetDirectory(string prompt)
        {
            ConsoleUtils.WriteLineMenuColor(prompt);
            String folder_path = new DirectoryInfo(@"" + Console.ReadLine()).ToString();

            while (!Directory.Exists(folder_path))
            {
                ConsoleUtils.WriteLineErrorColor("ERROR wrong path");
                return SetDirectory(prompt);
            }

            ConsoleUtils.WriteLineInfoColor($"INFO Filepath to read: {folder_path}");
            return folder_path;
        }
        private string SetPath(string prompt)
        {
            ConsoleUtils.WriteLineMenuColor(prompt);
            string path = @"" + Console.ReadLine();
            string dirPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);

            while (!File.Exists(path))
            {
                ConsoleUtils.WriteLineErrorColor("ERROR wrong path");
                return SetPath(prompt);
            }

            ConsoleUtils.WriteLineInfoColor($"INFO Filepath to read: {dirPath}");
            return dirPath;
        }
        private bool FilterXMLFiles(string file_path)
        {
            bool check_all_criteria = false;
            //criteria
            bool original_cpv_condition = false;
            bool form_condition = false;

            XmlReader xmlReader = XmlReader.Create(file_path);
            while (xmlReader.Read())
            {
                XmlNodeType nodetype = xmlReader.NodeType;
                string nodename = xmlReader.Name;

                if ((nodetype == XmlNodeType.Element) && (nodename == "ORIGINAL_CPV"))
                {
                    if (xmlReader.GetAttribute("CODE")!.StartsWith("71")) { original_cpv_condition = true; }
                }
                if (nodetype == XmlNodeType.Element && nodename == "F02_2014") { form_condition = true; }
                if (nodetype == XmlNodeType.Element && nodename == "F12_2014") { form_condition = true; }
            }

            if (original_cpv_condition && form_condition) { check_all_criteria = true; }

            return check_all_criteria;
        }



    }
}
