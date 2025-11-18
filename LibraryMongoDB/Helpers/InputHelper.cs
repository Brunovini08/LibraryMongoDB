using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Helpers
{
    public class InputHelper
    {
        public static string ReadString(string message, string messageError, string defaultValue = "")
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();       
                if (string.IsNullOrEmpty(input) && defaultValue != "")
                    return defaultValue;
                else if (!string.IsNullOrWhiteSpace(input))
                    return input;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(messageError);
                    Console.ResetColor();
                }
            }
        }


        public static int ReadInt(string message, string messageError, int defaultValue = 0)
        {
            while (true)
            {
                Console.Write(message);
                string? userInput = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrWhiteSpace(userInput) && defaultValue != 0)
                    return defaultValue;
                else if (int.TryParse(userInput, out int input))
                    return input;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(messageError);
                    Console.ResetColor();
                }
            }
        }

    }
}
