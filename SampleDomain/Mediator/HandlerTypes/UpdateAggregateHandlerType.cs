using SampleDomain.Core;
using System;

namespace SampleDomain.Mediator.HandlerTypes
{
    public interface IUpdateAggregateCommandHandler<TCommand, TAggregate> : ICommandHandler<TCommand>
     where TAggregate : class
     where TCommand : class
    {
        TAggregate Handle(TCommand command, Func<Guid, TAggregate> loadAggregate);
    }

    public class UpdateAggregateHandlerType : IHanderType
    {
        private readonly IContainter containter;

        public UpdateAggregateHandlerType(IContainter containter)
        {
            this.containter = containter;
        }

        public Type HandlerType => typeof(IUpdateAggregateCommandHandler<,>);

        public object Handle<TCommand>(TCommand command, ICommandHandler<TCommand> handler) where TCommand : class
        {
            this.DispatchCreateAggregate(command, (dynamic)handler);
            return null;
        }

        private void DispatchCreateAggregate<TCommand, TAggregate>(TCommand command, IUpdateAggregateCommandHandler<TCommand, TAggregate> commandHandler)
            where TCommand : class
            where TAggregate : class, IAggregate
        {
            var repository = this.containter.Create<IRepository<TAggregate>>();
            var aggregate = commandHandler.Handle(command, id => repository.Load(id));
            repository.Store(aggregate);
        }
    }
}