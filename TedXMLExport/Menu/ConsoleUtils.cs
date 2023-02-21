using MyApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedXMLExport.Menu
{
    
    static class ConsoleUtils
    {
     
        public static void BackToMenu()
        {
            WriteLineEventColor("\n(Press any key get back to menu.)");
            Console.ReadKey(true);
            Program.RunMainMenu();
        }
        public static void ExitMenu()
        {
            WriteLineEventColor("\n(Press any key to exit.)");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public static void WriteLineMenuColor(string text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteMenuColor(string text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteLineMenuSelectedColor(string text)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineInfoColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineErrorColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineEventColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void StartTimer(Stopwatch watch)
        {
            watch.Start();
            WriteLineInfoColor("INFO Watch started");
        }
        public static void StopTimer(Stopwatch watch)
        {
            watch.Stop();
            string timeElapsed = watch.Elapsed.ToString();
            WriteLineInfoColor($"INFO Watch ended. Time elapsed:{timeElapsed}");
        }

    }
}
