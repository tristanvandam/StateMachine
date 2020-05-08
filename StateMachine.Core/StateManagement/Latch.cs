﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StateMachine.Core.StateManagement.States;

namespace StateMachine.Core.StateManagement
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


        public Func<State, object, Task> OnExit { get; set; } = (state, data) => Task.CompletedTask;
        public Func<State, object, Task> OnEnter { get; set; } = (state, data) => Task.CompletedTask;


        internal async Task Exit(object transportData = null)
        {
            //TODO do other internal stuff.. 
            await OnExit(State, transportData);
            //TODO do other internal stuff.. 
        }


        internal async Task Enter(object transportData = null)
        {
            //TODO do other internal stuff.. 
            await OnEnter(State, transportData);
            //TODO do other internal stuff.. 
        }

    }

    public interface ILatch
    {
        Func<State, object, Task> OnEnter { get; set; }
        Func<State, object, Task> OnExit { get; set; }
    }
}