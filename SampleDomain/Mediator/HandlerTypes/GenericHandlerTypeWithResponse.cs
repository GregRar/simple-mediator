using SampleDomain.Commands;
using System;

namespace SampleDomain.Mediator.HandlerTypes
{
    public interface IGenericCommandWithResponseHandler<TCommand, TResponse> : ICommandHandler<TCommand>
        where TCommand : class, ICommandResponse<TResponse>
    {
        TResponse Handle(TCommand command);
    }

    public class GenericWithResponseHandlerType : IHanderType
    {
        public Type HandlerType => typeof(IGenericCommandWithResponseHandler<,>);

        public object Handle<TCommand>(TCommand command, ICommandHandler<TCommand> handler) where TCommand : class
        {
            return this.DispatchGenericHandlerWithResponse(command, (dynamic)handler);
        }

        private TResponse DispatchGenericHandlerWithResponse<TCommand, TResponse>(TCommand command, IGenericCommandWithResponseHandler<TCommand, TResponse> commandHandler)
           where TCommand : class, ICommandResponse<TResponse>
        {
            return commandHandler.Handle(command);
        }
    }
}