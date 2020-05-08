namespace StateMachine.Core
{
    public interface IFiniteStateMachine
    {
        void TriggerAction(Actions actions);
        void ForceSate(Latch latch);
    }
}