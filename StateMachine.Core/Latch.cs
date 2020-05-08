using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine.Core
{
    public class Latch
    {
        private readonly IDictionary<Actions, Func<Latch>> _actions;

        public Latch(State maintainedState, string name = "default", bool doNothingOnReEnter = false)
        {
            _actions = new Dictionary<Actions, Func<Latch>>();
            State = maintainedState;
            Name = name;
            DoNothingOnReEnter = doNothingOnReEnter;
        }

        public State State { get; }
        public string Name { get; set; }
        public bool DoNothingOnReEnter { get; }


        public void AddActions(IDictionary<Actions, Func<Latch>> actions)
        {
            actions.ToList().ForEach(x => _actions[x.Key] = x.Value);   //Override if new function is defined with same key.
        }
        internal Latch NextLatchFromAction(Actions actions)
        {
            //If state not found we will return the current state
            return !_actions.TryGetValue(actions, out var latchFactory) ? this : latchFactory.Invoke();
        }

        internal void Exit()
        {
            //TODO make overridable onExit Method

            throw new NotImplementedException();
        }


        internal void Enter()
        {
            //TODO make overridable onEnter Method
            //throw new NotImplementedException();
        }
    }
}