using System;

namespace StateMachine.Core
{
    public sealed class BinaryState : State
    {
        public BinaryState(Guid stateId, int bit) : base(stateId)
        {
            Bit = bit;
        }

        public int Bit { get; }


        // Perfect example of Enumeration class pattern:
        public static BinaryState Zero = new BinaryState(Guid.Parse("f54ac134-7f2d-4312-bb57-632dc3a9d8b8"), 0);
        public static BinaryState One = new BinaryState(Guid.Parse("33c4e63c-ca49-4547-b796-a03a37d2db09"), 1);
    }
}