using System;
using System.Dynamic;

namespace StateMachine.Core
{
    public class FiniteStateMachine : IFiniteStateMachine
    {
        private Latch _currentLatch;
        private readonly Action<string> _internalLog;

        public FiniteStateMachine(Latch initialStateLatch, Action<string> internalLog = null)
        {

            _currentLatch = initialStateLatch;
            _internalLog = internalLog ?? delegate(string s) {  };

            // will be implementing this StateMachine: https://miro.medium.com/max/1520/1*z-cGUTYS40cSjcMWgiOLww.png
        }

        public void TriggerAction(Actions actions)
        {
            _internalLog($"Current latch ({_currentLatch.Name}) with action: '{actions.Name}'");
            var nextLatch  = _currentLatch.NextLatchFromAction(actions);
            _internalLog($"Next latch ({_currentLatch.Name}) from action: '{actions.Name}'");

            if (nextLatch.GetType() == _currentLatch.GetType() && _currentLatch.DoNothingOnReEnter)
            {
                //DO Nothing
                return;
            }

            _currentLatch.Exit();

            _currentLatch = nextLatch;
            _currentLatch.Enter();

            _internalLog($"Current latch is now {_currentLatch.Name}");
            _internalLog($"------------------------------------");

        }

        public void ForceSate(Latch latch)
        {
            _currentLatch = latch;
        }
    }
}