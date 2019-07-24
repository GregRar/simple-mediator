using System;

namespace SampleDomain.Mediator.HandlerTypes
{
    public interface IGenericCommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        void Handle(TCommand command);
    }

    public class GenericHandlerType : IHanderType
    {
        public Type HandlerType => typeof(IGenericCommandHandler<>);

        public object Handle<TCommand>(TCommand command, ICommandHandler<TCommand> handler) where TCommand : class
        {
            this.DispatchGenericHandler(command, (dynamic)handler);
            return null;
        }

        private void DispatchGenericHandler<TCommand>(TCommand command, IGenericCommandHandler<TCommand> commandHandler)
           where TCommand : class
        {
            commandHandler.Handle(command);
        }
    }
}