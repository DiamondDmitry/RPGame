using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using RPGame.Model.Characters;
using static System.Net.Mime.MediaTypeNames;

namespace RPGame_Helpers
{
    public static class Helpers
    {
        public static void ColorWriteLine(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void ColorWrite(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
