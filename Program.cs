using System.Collections.Generic;
using System.Data;

namespace DTP_D17_hemtentamen
{
    internal class Program
    {

        public static List<TodoItem> todoList = new List<TodoItem>();
        public class TodoItem
        {
            public int prio; public string todo; public string status;

            public const int Active = 1;
            public const int Waiting = 2;
            public const int ready = 3;

            public static string StatusToString(int status)
            {
                switch ( status)
                {
                    case Active: return "active,";
                    case Waiting: return "waiting,";
                    case ready: return "ready,";
                    default: return "(invalid),";

                }
            }
            
            public TodoItem(string status, int prio, string todo)
            {
                this.status = status;
                this.prio = prio;
                this.todo = todo;
            }

            public TodoItem(string line) 
            {
                string[] field = line.Split('|');
                status = field[0];
                prio = Int32.Parse(field[1]);
                todo = field[2];
            }

            public static void Help()
            {
                Console.WriteLine("help               - display this help text");
                Console.WriteLine("list               - list all todo items");
                Console.WriteLine("list /status/      - list all todo items with that status");
                Console.WriteLine("set /num/ /status/ - changes the status of choosen task");
                Console.WriteLine("load /file/        - load a todo list");
                Console.WriteLine("save /file/        - save a file");
                Console.WriteLine("quit               - quit the program");
            }

            public static void List()
            {
                foreach (TodoItem item in todoList)
                {
                    Console.WriteLine($"{todoList.IndexOf(item) + 1, -1}. {item.status,-8} prio {item.prio} - {item.todo}");

                }
            }
            public static void ListReady()
            {
                foreach (TodoItem item in todoList)
                {
                    if (item.status == "ready")
                    {
                        Console.WriteLine($"{todoList.IndexOf(item) + 1 ,- 1}. {item.status,-8} prio {item.prio} - {item.todo}");
                    }
                }
            }
            public static void ListWaiting()
            {
                foreach (TodoItem item in todoList)
                {
                    if (item.status == "waiting")
                    {
                        Console.WriteLine($"{todoList.IndexOf(item) + 1,-1}. {item.status,-8} prio {item.prio} - {item.todo}");
                    }
                }
            }
            public static void ListActive()
            {
                foreach (TodoItem item in todoList)
                {
                    if (item.status == "active")
                    {
                        Console.WriteLine($"{todoList.IndexOf(item)+1, -1}. {item.status,-8} prio {item.prio} - {item.todo}");

                    }
                }
            }

            public static void LoadFromFile(string fileName)
            {
                StreamReader sr = new StreamReader(fileName);
                int numItems = 0;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    TodoItem item = new TodoItem(line);
                    todoList.Add(item);
                    numItems++;
                }
                sr.Close();
                Console.WriteLine($"Added {numItems} items");
            }

            public static void SaveFile(string fileName)
            {;
                foreach (TodoItem item in todoList)
                {
                    using StreamWriter file = new(fileName, append: true);
                    file.WriteLineAsync($"{item.status}|{item.prio}|{item.todo}");
                }
                Console.WriteLine($"{fileName} saved");
            }

            public static void SetStatus(int index, string status)
            {
                foreach (TodoItem item in todoList)
                {
                    int itemIndex = todoList.IndexOf(item)+1;
                    if (itemIndex == index)
                    {
                        item.status= status;
                        Console.WriteLine($"{itemIndex}. {item.todo} set to {status}");
                    }
                }
            }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the todo list, write 'help' for help!");

            bool quit = false;
            while (!quit)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                string[] command = input.Trim().Split(' ');

                switch (command[0])
                {
                    case "help":
                        TodoItem.Help(); break;
                    case "quit":
                        Console.WriteLine("Goodbye!");
                        quit = true;
                        break;
                    case "load":
                        if (command.Length == 2)
                        {
                            TodoItem.LoadFromFile($"{command[1]}");
                        }
                        else if (command.Length == 1)
                        {
                            Console.WriteLine("Usage: load /file/");
                        }
                        break;
                    case "list":
                        if (command.Length == 2 && command[1] == "ready")
                        {
                            TodoItem.ListReady();
                        }
                        else if (command.Length == 2 && command[1] == "waiting")
                        {
                            TodoItem.ListWaiting();
                        }
                        else if (command.Length == 2 && command[1] == "active")
                        {
                            TodoItem.ListActive(); 
                        }
                        else if (command.Length == 1)
                        {
                            TodoItem.List();
                        }
                        break;
                    case "set":
                        if (command.Length == 3)
                            if (command[2] == "ready")
                            {
                                TodoItem.SetStatus(Int32.Parse(command[1]), "ready");
                            }
                            else if (command[2] == "waiting")
                            {
                                TodoItem.SetStatus(Int32.Parse(command[1]), "waiting");
                            }
                            else if (command[2] == "active")
                            {
                                TodoItem.SetStatus(Int32.Parse(command[1]), "active");
                            }
                        break;
                    case "save":
                        if (command.Length == 2)
                        {
                            TodoItem.SaveFile(command[1]);
                        }
                        break;

                    default: Console.WriteLine($"Invalid input: {input}"); break;
                }
            }
        }
    }
}