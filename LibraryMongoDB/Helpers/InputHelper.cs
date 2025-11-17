using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMongoDB.Helpers
{
    public class InputHelper
    {
        public static string ReadString(string message, string messageError)
        {
            string? input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(messageError);
                    Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        public static int ReadInt(string message, string messageError)
        {
            int input;
            bool isValid;
            do
            {
                Console.Write(message);
                string? userInput = Console.ReadLine();
                isValid = int.TryParse(userInput, out input);
                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(messageError);
                    Console.ResetColor();
                }
            } while (!isValid);
            return input;
        }
    }
}
