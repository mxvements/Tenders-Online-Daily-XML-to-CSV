using System.Diagnostics;

namespace TedXMLExport.Menu
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
        private string _welcomeText; 
        private string _watermark = "                                   R&D-Morph ©2023";

        public Menu(string prompt, string[] options, string separator, string welcome_text)
        {
            _separator = separator;
            _prompt = prompt;
            _options = options;
            _selectedIndex = 0;
            _welcomeText = welcome_text;
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
            Console.WriteLine(_watermark);
            ConsoleUtils.WriteLineMenuColor(_separator);
            ConsoleUtils.WriteLineMenuColor(_prompt);            
            ConsoleUtils.WriteLineMenuColor(_separator);
            Console.WriteLine(_welcomeText);
            ConsoleUtils.WriteLineMenuColor(_separator);
            ConsoleUtils.WriteMenuColor("To choose an option use [↑] & [↓] keys: \n\n");

            for (int i = 0; i < _options.Length; i++)
            {
                string currentOption = _options[i];
                string prefix;
                

                if (i == _selectedIndex)
                {
                    prefix = "*";
                    ConsoleUtils.WriteLineMenuSelectedColor($" {prefix} << {currentOption} >>");
                }
                else
                {
                    prefix = " ";
                    ConsoleUtils.WriteLineMenuColor($" {prefix} << {currentOption} >>");
                }
            }

            ConsoleUtils.WriteLineMenuColor(_separator);
        }
    }

    

}
