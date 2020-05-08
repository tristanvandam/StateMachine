using System;

namespace StateMachine.Core.StateManagement.States
{
    public abstract class State : IState
    {
        public Guid StateId { get; }
        public string Name { get; }

        protected State(Guid stateId, string name = null)
        {
            StateId = stateId;
            Name = name;
        }

    }

    public interface IState
    {
    }
}