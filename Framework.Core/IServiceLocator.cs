using System;
using System.Collections.Generic;

namespace Framework.Core
{
    public interface IServiceLocator
    {
        T GetInstance<T>();

        object GetInstance(Type type);

        Array ResolveAll(Type type);
    }
}