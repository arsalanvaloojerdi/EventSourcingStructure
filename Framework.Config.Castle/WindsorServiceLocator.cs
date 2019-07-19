using Castle.Windsor;
using Framework.Core;
using System;
using System.Collections.Generic;

namespace Framework.Config.Castle
{
    public class WindsorServiceLocator : IServiceLocator
    {
        private readonly IWindsorContainer _container;

        public WindsorServiceLocator(IWindsorContainer container)
        {
            _container = container;
        }

        /// <inheritdoc />
        public T GetInstance<T>()
        {
            return _container.Resolve<T>();
        }

        /// <inheritdoc />
        public object GetInstance(Type type)
        {
            return _container.Resolve(type);
        }

        /// <inheritdoc />
        public Array ResolveAll(Type type)
        {
            return _container.ResolveAll(type);
        }
    }
}