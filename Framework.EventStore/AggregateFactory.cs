using System;
using System.Reflection;
using Framework.DomainModel.Core;

namespace Framework.Persistence.EventStore
{
    public static class AggregateFactory
    {
        public static IAggregateRoot Build(Type type)
        {
            ConstructorInfo constructor = type.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);

            return constructor.Invoke(new object[] { }) as IAggregateRoot;
        }
    }
}