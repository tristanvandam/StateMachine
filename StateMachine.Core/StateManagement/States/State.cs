using System;
using System.Threading.Tasks;
using StateMachine.Core.Exceptions;

namespace StateMachine.Core.StateManagement.States
{
    public abstract class State : IState
    {
        protected State(Guid stateId, string name = null)
        {
        }
        public Guid StateId { get; }
        public string Name { get; }

        public abstract Task<object> OnEnter(object transportData);
        public abstract Task<object> OnExit(object transportData);
    }

    public abstract class State<T> : State, IState<T>
    {
 
        protected State(Guid stateId, string name = null) : base(stateId, name)
        {
        }

        public abstract Task<T> OnEnter(T transportData);
        public abstract Task<T> OnExit(T transportData);
        public override async Task<object> OnEnter(object transportData)
        {
            if (!(transportData is T coercedValue))
            {
                throw new TypeCoercionException(transportData.GetType(), typeof(T));
            }

            return await OnEnter(coercedValue);
        }

        public override async Task<object> OnExit(object transportData)
        {
            if (!(transportData is T coercedValue))
            {
                throw new TypeCoercionException(transportData.GetType(), typeof(T));
            }

            return await OnExit(coercedValue);
        }
    }

    public interface IState 
    {
        Task<object> OnEnter(object transportData);
        Task<object> OnExit(object transportData);
    }

    public interface IState<T> : IState
    {
        Task<T> OnEnter(T transportData);
        Task<T> OnExit(T transportData);
    }

}