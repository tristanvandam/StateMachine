using System;

namespace StateMachine.ExampleTestApp.Logger
{
    public interface ILogger: IDisposable
    {
        void Log(string message);
    }
}