using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine.Core
{
    public class Latch
    {
        private readonly IDictionary<Actions, Func<Latch>> _actions;

        public Latch(State maintainedState, string name = "default")
        {
            _actions = new Dictionary<Actions, Func<Latch>>();
            State = maintainedState;
            Name = name;
        }

        public void AddActions(IDictionary<Actions, Func<Latch>> actions)
        {
            actions.ToList().ForEach(x => _actions[x.Key] = x.Value);   //Override if new function is defined with same key.
        }

        public State State { get; }
        public string Name { get; set; }

        public Latch Trigger(Actions actions)
        {
            //If state not found we will return the current state
            return !_actions.TryGetValue(actions, out var latchFactory) ? this : latchFactory.Invoke();
        }
    }
}