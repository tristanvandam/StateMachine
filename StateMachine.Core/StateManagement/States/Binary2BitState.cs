using System;

namespace StateMachine.Core.StateManagement.States
{
    public sealed class Binary2BitState : State
    {
        public Binary2BitState(Guid stateId, int bit1, int bit2) : base(stateId)
        {
            Bit1 = bit1;
            Bit2 = bit2;
        }

        public int Bit1 { get; }
        public int Bit2 { get; }

    }
}