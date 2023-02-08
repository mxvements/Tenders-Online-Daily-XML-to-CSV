using LeerTedXML.CSVCreate;
using LeerTedXML.XMLDocument;
using LeerTedXML.XMLReader;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using TedXMLExport.Menu;
using TedXMLExport.XMLTedObject;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Read Tenders Online Daily";
            Console.CursorVisible = true;

            RunMainMenu();

        }


        //methods
        public static void RunMainMenu()
        {
            string separator     = "****************************************************";
            string prompt        = "******** READ Tenders Online Daily XML files *******";
            string[] options = {"Read .xml file",
                                "Read .xml files in folder",
                                "Credits",
                                "Exit" };

            Menu mainMenu = new Menu(prompt, options, separator);
            int selectedIndex = mainMenu.ChooseOptions();

            int readOption; int fileOption;

            switch (selectedIndex)
            {
                case 0:
                    readOption = 2;
                    fileOption = RunExportMenu();
                    DispatchOptions(fileOption, readOption);
                    RunMainMenu();
                    break;
                case 1:
                    readOption = 1;
                    fileOption = RunExportMenu();
                    DispatchOptions(fileOption, readOption);
                    RunMainMenu();
                    break;
                case 2:
                    ShowCredits();
                    break;
                case 3:
                    ConsoleUtils.ExitMenu();
                    break;
            }

        }

        public static int RunExportMenu()
        {
            string separator    = "****************************************************";
            string prompt       = "****************** EXPORT options ******************";
            string[] txtoptions = { "Append to former .txt file",
                                    "Create new .txt file",
                                    "Only print on console",
                                    "Go back" };

            Menu csvMenu = new Menu(prompt, txtoptions, separator);
            int selectedIndex = csvMenu.ChooseOptions();

            int txt_options = 0;
            switch (selectedIndex)
            {
                case 0:
                    txt_options += 1;
                    break;
                case 1:
                    txt_options += 2;
                    break;
                case 2:
                    txt_options += 3;
                    break;
                case 3:
                    Console.Clear();
                    RunMainMenu();
                    break;
            }

            return txt_options;

        }

        static void DispatchOptions(int fileOption, int readOption)
        {
            XMLTedFilter xmlRead = new XMLTedFilter();
            XMLTedParser xmlDoc = new XMLTedParser(xmlRead);
            TXTCreate csvCreate = new TXTCreate(xmlDoc);

            if (fileOption == 1)
            {
                Stopwatch watch = new Stopwatch();
                ConsoleUtils.StartTimer(watch);

                //"Append to former .csv file"
                csvCreate.AppendTxt(readOption);

                ConsoleUtils.StopTimer(watch);
                ConsoleUtils.BackToMenu();
            }
            else if (fileOption == 2)
            {
                Stopwatch watch = new Stopwatch();
                ConsoleUtils.StartTimer(watch);

                //"Create new .csv file" and read xml
                csvCreate.CreateNewTxt(readOption);

                ConsoleUtils.StopTimer(watch);
                ConsoleUtils.BackToMenu();
            }
            else if (fileOption == 3)
            {
                Stopwatch watch = new Stopwatch();
                ConsoleUtils.StartTimer(watch);

                //print on console, contolText
                csvCreate.ReadXml(readOption);

                ConsoleUtils.StopTimer(watch);
                ConsoleUtils.BackToMenu();
            }
        }


        private static void ShowCredits()
        {
            ConsoleUtils.WriteLineMenuColor("\t> Name of project");
            Console.WriteLine("\t" + Assembly.GetExecutingAssembly().GetName().Name);

            ConsoleUtils.WriteLineMenuColor("\t> Description:");
            Console.WriteLine("\tConsole applictation to filter and parse TED documents." +
                "\n\tFiltering conditions:" +
                "\n\t - CPV starting w/ '71' -archiecture and engineering services- " +
                "\n\t - F02 or F12 forms -contract notices and design contest notices-.");

            ConsoleUtils.WriteLineMenuColor("\t> Version");
            Console.WriteLine("\t" + Assembly.GetExecutingAssembly().GetName().Version);

            ConsoleUtils.WriteLineMenuColor("\t> Target runtime environment");
            Console.WriteLine("\tPortable");

            string publish_folder = @"" +
                "G:\\Unidades compartidas\\P10380 I+D\\P10380 I+D Tenders Online Daily\\dev\\TendersOnlineDaily\\TedXMLExport\\bin\\Publish\\TedXMLExport.exe";
            string exe_last_modified = System.IO.File.GetLastWriteTime(publish_folder).ToShortDateString();
            ConsoleUtils.WriteLineMenuColor("\t> Project last published on:");
            Console.WriteLine("\t" + exe_last_modified);

            string project_last_modified = System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToShortDateString();
            ConsoleUtils.WriteLineMenuColor("\t> Project last edited on:");
            Console.WriteLine("\t" + project_last_modified);

            ConsoleUtils.WriteLineMenuColor("\t> Authorship");
            Console.WriteLine("\tApplied R&D team. MorphEstudio. ©2023");

            ConsoleUtils.BackToMenu();
        }


    }
}



