using System.Data;

namespace DTP_D17_hemtentamen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "file.lis";
            Console.WriteLine("Welcome to the todo list, write 'help' for help!");
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                string[] command = input.Trim().Split(' ');
                if (command[0] == "help") 
                {
                    Console.WriteLine("help        - display this help text");
                    Console.WriteLine("load /file/ - load a todo list");
                    Console.WriteLine("quit        - quit the program");
                }
                else if (command[0] == "quit") 
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command.Length >= 2 && command[0] == "load" && command[1] == filename )
                {
                    Console.WriteLine("NYI: load file.lis");
                }
                else if (command.Length >= 1 && command[0] == "load")
                {
                    Console.WriteLine("Usage: load /file/");


                }
            }
        }
    }
}