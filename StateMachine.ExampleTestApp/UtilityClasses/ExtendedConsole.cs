using System;
using System.Threading;

namespace StateMachine.ExampleTestApp.UtilityClasses
{
    public static class ExtendedConsole
    {
        public static void PrintHelpText()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("To exit the application at any time. Press 'E' or `ESC`");
            Console.WriteLine("To Preform action 'WakeUp'. Press 'W'");
            Console.WriteLine("To Preform action 'TakeTrain'. Press 'T'");
            Console.WriteLine("To Preform action 'FallAsleep'. Press 'S'");
            Console.WriteLine("To List current state, press 'L'");
            Console.WriteLine("-------------------------------------");
        }

        public static void BlankLine()
        {
            Console.WriteLine(string.Empty);
        }
        public static void WaitForKey(ConsoleKey waitKey)
        {
            while (Console.ReadKey().Key != waitKey)
            {
                Thread.Sleep(0);
            }
        }
    }
}