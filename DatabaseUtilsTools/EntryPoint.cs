using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;

namespace DatabaseUtilsTools
{
    public class EntryPoint
    {
        private const string programCode = "dust";
        public static void Main()
        {
            DisplayWelcomeScreen();
            while (true)
            {
                try
                {
                    string[] args = Utils.ReadCommand().Split(' ');
                    Utils.Assert(args != null && args.Length != 0, "", fatal: true);
                    if (args[0] == programCode)
                    {
                        Queue<string> commands = new Queue<string>(args);
                        commands.Dequeue(); // programCode
                        IConsoleRunnable consoleService = SelectConsoleRunnableService(commands);
                        consoleService.Run(commands);
                    }
                    else
                    {
                        if(args[0] == "clear")
                        {
                            Console.Clear();
                            System.Threading.Thread.Sleep(200);
                        }
                        else if(args[0] == "exit")
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            throw new Exception("Command not found");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }



        private static void DisplayWelcomeScreen()
        {
            Console.Write("Welcome to ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("dust");
            Console.Write("D");
            Console.ResetColor();
            Console.Write("atabase ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("U");
            Console.ResetColor();
            Console.Write("til");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("S T");
            Console.ResetColor();
            Console.Write("ools");

        }

        private static IConsoleRunnable SelectConsoleRunnableService(Queue<string> commands)
        {
            IEnumerable<Type> allTypes = Utils.FindAssignableClasses<IConsoleRunnable>();
            foreach (Type type in allTypes)
            {
                IConsoleRunnable consoleRunnable = Activator.CreateInstance(type) as IConsoleRunnable;
                if(consoleRunnable.GetCode() == commands.Peek())
                {
                    commands.Dequeue();
                    return consoleRunnable;
                }
            }
            throw new Exception("Could not found command");
        }
    }


}
