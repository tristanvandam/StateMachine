using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateMachine.Core
{
    public class Latch : ILatch
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


        //TODO - Decide which way you want to implement this
        public Func<State, object, Task> OnExit { get; set; } = (state, data) => Task.CompletedTask;
        public OnEnterDelegate OnEnter { get; set; } = (state, data) => Task.CompletedTask;
        public delegate Task OnEnterDelegate(State currentState, object transportData = null);


        internal async Task Exit(object transportData = null)
        {
            await OnExit(State, transportData);
            //TODO make overridable onExit Method
        }


        internal async Task Enter(object transportData = null)
        {
            await OnEnter(State, transportData);
            //TODO do other internal stuff.. 
        }

    }

    public interface ILatch
    {
        Func<State, object, Task> OnExit { get; set; }
        Latch.OnEnterDelegate OnEnter { get; set; }
    }
}