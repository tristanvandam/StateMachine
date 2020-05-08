using System;
using System.Threading.Tasks;

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
        public override Task<object> OnEnter(object transportData)
        {
            return Task.FromResult(transportData);
        }

        public override Task<object> OnExit(object transportData)
        {
            return Task.FromResult(transportData);
        }
    }
}