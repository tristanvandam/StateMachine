using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachine.ExampleTestApp.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.UtcNow} - {message}");
        }

        public void Dispose()
        {
        }
    }
}
