using System;

namespace StateMachine.Core
{
    public abstract class State
    {
        public Guid StateId { get; }
        public string Name { get; }

        protected State(Guid stateId, string name = null)
        {
            StateId = stateId;
            Name = name;
        }

    }
}