using System;
using StructureMap;

namespace Coupling.Domain.CQRS.Command
{
    public class InProcessBus : IBus
    {
        private readonly IContainer _container;

        public InProcessBus(IContainer container)
        {
            _container = container;
        }

        public void Send<T>(T command)
        {
            try
            {
                var handler = CreateCommandHandlerInstance(command);
                handler.Execute(command);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to process command.", ex);
            }
        }

        private ICommand<T> CreateCommandHandlerInstance<T>(T command)
        {
            var handler = _container.TryGetInstance<ICommand<T>>();
            if (handler == null)
            {
                throw new Exception("Failed to create Command Handler Interface Instance: " + command);
            }
            return handler;
        }
    }
}
