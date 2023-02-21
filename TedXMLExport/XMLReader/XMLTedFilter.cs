using System.Reflection;
using System.Xml;
using TedXMLExport.Menu;

namespace LeerTedXML.XMLReader
{
    internal class XMLTedFilter : IXMLTedFilter
    {
        public List<string> ReadXmlSingleFile()
        {
            List<string> xml_file_list = new List<string>();

            string dirPath = SetPath("> XML Path to read:");

            if(FilterXMLFiles(dirPath)) { xml_file_list.Add(dirPath); }

            return xml_file_list;

        }

        public List<string> ReadXmlFolder()
        {
            List<String> xml_file_list = new List<String>();

            List<string> folder_path_list = SetDirectory("> FOLDER Path to read:"); //esto antes era folder_path

            folder_path_list.ForEach(folder_path =>
            {
                List<string> file_list = Directory.EnumerateFiles(folder_path, "*.xml").ToList();

                file_list.ForEach(x =>
                {
                    if (FilterXMLFiles(x)) { xml_file_list.Add(x); }
                });
            });

            ConsoleUtils.WriteLineEventColor($"EVENT Folder read, {xml_file_list.Count} files added to parser");
            return xml_file_list;
        }

        private List<string> SetDirectory(string prompt)
        {
            ConsoleUtils.WriteLineMenuColor(prompt);
            String parent_folder_path = new DirectoryInfo(@"" + Console.ReadLine()).ToString();
            List<string> folder_path_list = new List<string>();

            while (!Directory.Exists(parent_folder_path))
            {
                ConsoleUtils.WriteLineErrorColor("ERROR wrong path");
                return SetDirectory(prompt);
            }

            folder_path_list.Add(parent_folder_path);
            Directory.GetDirectories(parent_folder_path).ToList().ForEach(dir =>
            {
                folder_path_list.Add(dir);
            });

            folder_path_list.ForEach(path => ConsoleUtils.WriteLineInfoColor($"INFO Filepath to read: {path}"));

            return folder_path_list;
        }
        private string SetPath(string prompt)
        {
            ConsoleUtils.WriteLineMenuColor(prompt);
            string path = @"" + Console.ReadLine().Replace("\"", "");
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
