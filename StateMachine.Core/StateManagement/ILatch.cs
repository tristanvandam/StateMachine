using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StateMachine.Core.StateManagement.States;

namespace StateMachine.Core.StateManagement
{
    public interface ILatch
    {
        Func<State, object, Task> OnEnter { get; set; }
        Func<State, object, Task> OnExit { get; set; }
        void AddActions(IDictionary<Actions, Func<ILatch>> actions);
    }
}