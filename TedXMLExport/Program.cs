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
            Console.WindowHeight= 50;
            Console.WindowWidth= 55;
            RunMainMenu();

        }


        //methods
        public static void RunMainMenu()
        {
            string separator     = "****************************************************";
            string prompt        = "******** READ Tenders Online Daily XML files *******";
            string welcome_text = "\n    > Hello World,\n\n" +
                                  "\t> This program allows you to read an \n" +
                                  "\t.xml file from the Tenders Electronic  \n" +
                                  "\tDaily and export it in .txt format.\n\n" +
                                  "\t> Before running any of these commands, \n" +
                                  "\tyou should've downloaded some files \n" +
                                  "\tto parse. \n\n" +
                                  "\t> Check the 'XML bulk downloads' \n" +
                                  "\tsection from the official webpage: \n" +
                                  "\tted.europa.eu/TED/main/HomePage.do\n";
            string[] options = {"Read .xml file",
                                "Read .xml files in folder",
                                "Credits",
                                "Exit" };

            Menu mainMenu = new Menu(prompt, options, separator, welcome_text);
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
            string welcome_text = "\n\t> Please, select your preferred \n" +
                                  "\texport option.\n\n" +
                                  "\t> At all events, the program will ask\n" +
                                  "\tfirst for the former .txt file or \n" +
                                  "\tfolder to save the current update and \n" +
                                  "\tthen the .xml folder or single file to \n" +
                                  "\tparse.\n";
            string separator    = "****************************************************";
            string prompt       = "****************** EXPORT options ******************";
            string[] txtoptions = { "Append to former .txt file",
                                    "Create new .txt file",
                                    "Only print on console",
                                    "Go back" };

            Menu csvMenu = new Menu(prompt, txtoptions, separator, welcome_text);
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
            Console.WriteLine("\tConsole applictation to filter and parse " +
                              "\tTED documents." +
                              "\n\tFiltering conditions:" +
                              "\n\t - CPV starting w/ '71' -archiecture " +
                              "\n\t and engineering services- " +
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



