using SampleDomain.Core;
using System;

namespace SampleDomain.Mediator.HandlerTypes
{
    public interface ICreateAggregateCommandHandler<TCommand, TAggregate> : ICommandHandler<TCommand>
     where TAggregate : class
     where TCommand : class
    {
        TAggregate Handle(TCommand command);
    }

    public class CreateAggregateHandlerType : IHanderType
    {
        private readonly IContainter containter;

        public CreateAggregateHandlerType(IContainter containter)
        {
            this.containter = containter;
        }

        public Type HandlerType => typeof(ICreateAggregateCommandHandler<,>);

        public object Handle<TCommand>(TCommand command, ICommandHandler<TCommand> handler) where TCommand : class
        {
            this.DispatchCreateAggregate(command, (dynamic)handler);
            return null;
        }

        private void DispatchCreateAggregate<TCommand, TAggregate>(TCommand command, ICreateAggregateCommandHandler<TCommand, TAggregate> commandHandler)
            where TCommand : class
            where TAggregate : class, IAggregate
        {
            var aggregate = commandHandler.Handle(command);
            this.containter.Create<IRepository<TAggregate>>().Store(aggregate);
        }
    }
}