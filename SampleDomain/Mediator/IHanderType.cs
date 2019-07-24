using System;

namespace SampleDomain.Mediator
{
    public interface IHanderType
    {
        Type HandlerType { get; }

        object Handle<TCommand>(TCommand command, ICommandHandler<TCommand> handler) where TCommand : class;
    }
}