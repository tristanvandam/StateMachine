using StateMachine.Core;

namespace StateMachine.ExampleTestApp
{
    public class TransportActions : Actions
    {
        public string OtherMeta { get; }

        private TransportActions(int id, string name, string otherMeta) : base(id, name)
        {
            OtherMeta = otherMeta;
        }

        public static TransportActions TakeTrain = new TransportActions(1, nameof(TakeTrain), "At nearest train station.");
        public static TransportActions WakeUp = new TransportActions(2, nameof(WakeUp), "Always at 5am");
        public static TransportActions FallAsleep = new TransportActions(3, nameof(FallAsleep), "In Queen size bed");
    }
}