using SampleDomain.Commands;
using SampleDomain.Core;
using SampleDomain.Mediator.HandlerTypes;
using System;
using System.Linq;

namespace SampleDomain.Mediator
{
    public interface ICommandMediator
    {
        void Send<TCreateUpdateCommand>(TCreateUpdateCommand command)
            where TCreateUpdateCommand : class;

        TResponse SendWithResponse<TResponse>(ICommandResponse<TResponse> command);
    }

    public class CommandMediator : ICommandMediator
    {
        private readonly Type[] noResponseHandlerTypes = new[]
           {
                        typeof(GenericHandlerType),
                        typeof(GenericWithResponseHandlerType),
                        typeof(CreateAggregateHandlerType),
                        typeof(CreateAggregateWithResponseHandlerType),
                        typeof(UpdateAggregateHandlerType),
                   };

        private readonly Type[] withResponseHandlerTypes = new[]
                   {
                        typeof(GenericWithResponseHandlerType),
                        typeof(CreateAggregateWithResponseHandlerType),
                   };

        private readonly ICommandHandler[] commandHandlers;
        private readonly IContainter containter;

        public CommandMediator(ICommandHandler[] commandHandlers, IContainter containter)
        {
            this.containter = containter;
            this.commandHandlers = commandHandlers;
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : class
        {
            var handlerTypes = this.noResponseHandlerTypes.Select(type => (IHanderType)this.containter.Create(type)).ToArray();

            Dispatch(command, handlerTypes);
        }

        public TResponse SendWithResponse<TResponse>(ICommandResponse<TResponse> command)
        {
            var handlerTypes = this.withResponseHandlerTypes.Select(type => (IHanderType)this.containter.Create(type)).ToArray();

            return (TResponse)Dispatch(command, handlerTypes);
        }

        private object Dispatch<TCommand>(TCommand command, IHanderType[] handlerTypes)
            where TCommand : class
        {
            var handler = this.FindHandler((dynamic)command);
            var strategy = handlerTypes.SingleOrDefault(x => IsOfType(x.HandlerType, (dynamic)command, (dynamic)handler));

            //Todo: throw informative exception if more than one handler found

            if (strategy == null)
            {
                throw new Exception("Don't know how to dispatch handler of type " + handler.GetType());
            }

            return strategy.Handle((dynamic)command, (dynamic)handler);
        }

        private ICommandHandler<TCommand> FindHandler<TCommand>(TCommand command) where TCommand : class
        {
            var handlerType = CreateHandlerGenericType<TCommand>();
            var handler = this.commandHandlers.SingleOrDefault(h => h.GetType().GetInterfaces().Any(x => handlerType.IsAssignableFrom(x)));

            if (handler == null)
            {
                throw new Exception("There's no handler for command " + typeof(TCommand));
            }

            return (ICommandHandler<TCommand>)handler;
        }

        private static bool IsOfType<TCommand>(Type specificType, TCommand command, ICommandHandler<TCommand> handler)
           where TCommand : class
        {
            var handlerType = CreateHandlerGenericType<TCommand>();
            var specificHandler = handler.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == specificType && handlerType.IsAssignableFrom(x));
            return specificHandler;
        }

        private static Type CreateHandlerGenericType<TCommand>() where TCommand : class
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            return handlerType;
        }
    }
}