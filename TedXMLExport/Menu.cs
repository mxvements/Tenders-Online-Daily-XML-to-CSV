using System.Diagnostics;

namespace LeerTedXML
{
    public class Menu
    {
        /*
         * The menu needs as attributes:
         *      promtp: title and description
         *      options: list of strings to select
         *      selected index: to know which index of the list of options we are selecting
         */
        private int _selectedIndex;
        private string[] _options;
        private string _prompt;
        private string _separator;

        public Menu(string prompt, string[] options, string separator)
        {
            this._separator = separator;
            this._prompt = prompt;
            this._options = options;
            _selectedIndex = 0;
        }

        //METHODS
        public int ChooseOptions()
        {
            ConsoleKey keyPressed;

            do
            {
                //everytime we press any key we refresh console
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                //update the selected index based on the arrow keys
                //avoid bugs, cicle back to the start and the end
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    _selectedIndex--;
                    if (_selectedIndex == -1) _selectedIndex = _options.Length - 1;
                }
                if (keyPressed == ConsoleKey.DownArrow)
                {
                    _selectedIndex++;
                    if (_selectedIndex == _options.Length) _selectedIndex = 0;
                }

            } while (keyPressed != ConsoleKey.Enter); //when pressed Enter, we exit console

            return _selectedIndex;
        }

        private void DisplayOptions()
        {
            //render the menu on the screen            
            ConsoleUtils.WriteLineMenuColor(_separator);
            ConsoleUtils.WriteLineMenuColor(_prompt);
            ConsoleUtils.WriteLineMenuColor(_separator);

            for (int i = 0; i < _options.Length; i++)
            {
                string currentOption = _options[i];
                string prefix;

                if (i == _selectedIndex)
                {
                    prefix = "*";
                    ConsoleUtils.WriteLineMenuSelectedColor($"{prefix} << {currentOption} >>");
                }
                else
                {
                    prefix = " ";
                    ConsoleUtils.WriteLineMenuColor($"{prefix} << {currentOption} >>");
                }
            }

            ConsoleUtils.WriteLineMenuColor(_separator);
        }
    }

    static class ConsoleUtils
    {
        public static void BackToMenu()
        {
            WriteLineEventColor("(Press any key get back to menu.)");
            Console.ReadKey(true);
        }
        public static void ExitMenu()
        {
            WriteLineEventColor("(Press any key to exit.)");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public static void WriteLineMenuColor(String text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineMenuSelectedColor(String text)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineInfoColor(String text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineErrorColor(String text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLineEventColor(String text)
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
            String timeElapsed = watch.Elapsed.ToString();
            WriteLineInfoColor($"INFO Watch ended. Time elapsed:{timeElapsed}");
        }

    }

}
