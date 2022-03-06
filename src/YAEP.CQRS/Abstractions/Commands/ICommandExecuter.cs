namespace YAEP.CQRS.Abstractions.Commands
{
    public interface ICommandExecuter
    {
        Task<IResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand;
    }
}
