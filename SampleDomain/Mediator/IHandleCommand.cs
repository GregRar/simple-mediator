namespace SampleDomain.Mediator
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : class
    {
    }
}