using System.Threading.Tasks;
using StateMachine.Core.StateManagement;

namespace StateMachine.Core
{
    public interface IFiniteStateMachine
    {
        Task TriggerAction(Actions actions);
        void ForceSate(Latch latch);
        string GetCurrentState();
    }
}