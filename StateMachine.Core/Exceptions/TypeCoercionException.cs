using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachine.Core.Exceptions
{
    public class TypeCoercionException: Exception
    {
        public Type FromType { get; }
        public Type ToType { get; }

        public TypeCoercionException(Type fromType, Type toType): base($"Cannot coerce value from type '{fromType.Name}' to type '{toType.Name}'")
        {
            FromType = fromType;
            ToType = toType;
        }
    }
}
