using SampleDomain.Commands;
using SampleDomain.Core;
using System;

namespace SampleDomain.Mediator.HandlerTypes
{
    public interface ICreateAggregateWithResponseCommandHandler<TCommand, TAggregate, TResponse> : ICommandHandler<TCommand>
        where TAggregate : class
        where TCommand : class, ICommandResponse<TResponse>
    {
        CreateAggregateCommandResponse<TAggregate, TResponse> Handle(TCommand command);
    }

    public class CreateAggregateCommandResponse<TAggregate, TResponse>
    {
        public CreateAggregateCommandResponse(TAggregate enity, TResponse response)
        {
            Aggregate = enity;
            Response = response;
        }

        public TAggregate Aggregate { get; private set; }

        public TResponse Response { get; private set; }
    }

    public class CreateAggregateWithResponseHandlerType : IHanderType
    {
        private readonly IContainter containter;

        public CreateAggregateWithResponseHandlerType(IContainter containter)
        {
            this.containter = containter;
        }

        public Type HandlerType => typeof(ICreateAggregateWithResponseCommandHandler<,,>);

        public object Handle<TCommand>(TCommand command, ICommandHandler<TCommand> handler) where TCommand : class
        {
            return this.DispatchCreateCommandWithResponse((dynamic)command, (dynamic)handler);
        }

        private TResponse DispatchCreateCommandWithResponse<TCommand, TAggregate, TResponse>(TCommand command, ICreateAggregateWithResponseCommandHandler<TCommand, TAggregate, TResponse> commandHandler)
           where TCommand : class, ICommandResponse<TResponse>
           where TAggregate : class, IAggregate
        {
            var result = commandHandler.Handle(command);
            this.containter.Create<IRepository<TAggregate>>().Store(result.Aggregate);
            return result.Response;
        }
    }
}