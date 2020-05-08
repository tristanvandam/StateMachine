using System;
using System.Threading.Tasks;

namespace StateMachine.ExampleTestApp
{
    internal interface IProgram : IDisposable
    {
        Task Run();
    }
}