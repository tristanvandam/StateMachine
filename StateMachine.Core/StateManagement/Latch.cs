using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StateMachine.Core.StateManagement.States;

namespace StateMachine.Core.StateManagement
{
    public class Latch : ILatch
    {
        private readonly IDictionary<Actions, Func<ILatch>> _actions;

        public Latch(State maintainedState, IDictionary<Actions, Func<ILatch>> actionsToLatches, string name = "default", bool doNothingOnReEnter = false)
        {
            _actions = actionsToLatches;
            State = maintainedState;
            Name = name;
            DoNothingOnReEnter = doNothingOnReEnter;
        }

        public State State { get; }
        public string Name { get; set; }
        public bool DoNothingOnReEnter { get; }


        public void AddActions(IDictionary<Actions, Func<ILatch>> actions)
        {
            actions.ToList().ForEach(x => _actions[x.Key] = x.Value);   //Override if new function is defined with same key.
        }

        internal Latch NextLatchFromAction(Actions actions)
        {
            if (!_actions.TryGetValue(actions, out var latchFactory))
            {
                //If state not found we will return the current state
                return this;
            } 
            
            var newLatch = latchFactory.Invoke();
            if (!(newLatch is Latch latch))
            {
                //Can't do anything, so just return current latch.
                return this;
            }

            return latch;
        }


        public Func<State, object, Task> OnExit { get; set; } = (state, data) => Task.CompletedTask;
        public Func<State, object, Task> OnEnter { get; set; } = (state, data) => Task.CompletedTask;


        internal async Task Exit(object transportData = null)
        {
            await State.OnExit(transportData);
        }


        internal async Task Enter(object transportData = null)
        {
            await State.OnEnter(transportData);

        }
    }




    public interface ILatchFactory
    {
        Latch Resolve(string name);
    }
}