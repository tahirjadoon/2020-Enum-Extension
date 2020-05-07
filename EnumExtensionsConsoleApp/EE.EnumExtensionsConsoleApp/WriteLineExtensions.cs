using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE.EnumExtensionsConsoleApp
{
    public static class WriteLine
    {
        public static ConsoleColor ColorBlue = ConsoleColor.Blue;
        public static ConsoleColor ColorCyan = ConsoleColor.Cyan;
        public static ConsoleColor ColorGreen = ConsoleColor.Green;
        public static ConsoleColor ColorRed = ConsoleColor.Red;
        public static ConsoleColor ColorWhite = ConsoleColor.White;
        public static ConsoleColor ColorYellow = ConsoleColor.Yellow;

        public static void WriteNewLine(this string message, ConsoleColor? color = null)
        {
            if (color != null)
                Console.ForegroundColor = color.GetValueOrDefault();
            if (!string.IsNullOrWhiteSpace(message))
                Console.WriteLine(message);
        }

        public static void WriteSameLine(this string message, ConsoleColor? color = null)
        {
            if (color != null)
                Console.ForegroundColor = color.GetValueOrDefault();
            if (!string.IsNullOrWhiteSpace(message))
                Console.Write(message);
        }

        public static void RevertColor(this string message)
        {
            Console.ForegroundColor = ColorWhite;
        }

        public static void EmptyLine(this string message)
        {
            Console.WriteLine(string.Empty);
        }

        public static void EmptySameLine(this string message)
        {
            Console.Write(string.Empty);
        }
    }
}
