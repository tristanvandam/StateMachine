﻿using System;
using System.Threading.Tasks;
using StateMachine.Core.StateManagement;

namespace StateMachine.Core
{
    public sealed class FiniteStateMachine : IFiniteStateMachine
    {
        private Latch _currentLatch;
        private readonly Action<string> _internalLog;

        public FiniteStateMachine(Latch initialStateLatch, Action<string> internalLog = null)
        {
            _currentLatch = initialStateLatch;
            _internalLog = internalLog ?? delegate(string s) {  };
        }

        public async Task TriggerAction(Actions actions)
        {
            _internalLog($"Current latch ({_currentLatch.Name}) with action: '{actions.Name}'");
            var nextLatch  = _currentLatch.NextLatchFromAction(actions);
            _internalLog($"Next latch ({nextLatch.Name}) from action: '{actions.Name}'");

            if (ShouldDoNothingForCurrentLatch(nextLatch))
            {
                //DO NOTHING
                return ;
            }

            await _currentLatch.Exit();
            _currentLatch = nextLatch;      //swap latches
            _internalLog($"Current latch has been updated and is now {_currentLatch.Name}");
            await _currentLatch.Enter();
        }

        private bool ShouldDoNothingForCurrentLatch(Latch nextLatch)
        {
            // If the latch type doesn't change and the latch is configured to not re-enter itself if unchanged then return true (for do nothing)
            return nextLatch.GetType() == _currentLatch.GetType() && _currentLatch.DoNothingOnReEnter;
        }

        public void ForceSate(Latch latch)
        {
            _currentLatch = latch;
        }

        public string GetCurrentState()
        {
            return _currentLatch.Name;
        }
    }
}