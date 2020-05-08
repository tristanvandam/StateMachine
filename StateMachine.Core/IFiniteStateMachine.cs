using System.Threading.Tasks;

namespace StateMachine.Core
{
    public interface IFiniteStateMachine
    {
        Task TriggerAction(Actions actions);
        void ForceSate(Latch latch);
        string GetCurrentState();
    }
}