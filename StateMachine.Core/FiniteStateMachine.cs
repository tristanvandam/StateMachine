using System;

namespace StateMachine.Core
{
    public class FiniteStateMachine : IFiniteStateMachine
    {
        private Latch _currentLatch;

        public FiniteStateMachine(Latch initialStateLatch)
        {
            _currentLatch = initialStateLatch;

            // will be implementing this StateMachine: https://miro.medium.com/max/1520/1*z-cGUTYS40cSjcMWgiOLww.png

          

        }

        public void TriggerAction(Actions actions)
        {
            Console.WriteLine($"Current latch ({_currentLatch.Name}) with action: '{actions.Name}'");
            _currentLatch = _currentLatch.Trigger(actions);
            Console.WriteLine($"Current latch is now {_currentLatch.Name}");
            Console.WriteLine($"------------------------------------");

        }

        public void ForceSate(Latch latch)
        {
            _currentLatch = latch;
        }
    }
}