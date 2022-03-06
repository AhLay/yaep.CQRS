namespace YAEP.CQRS.Abstractions.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task<IResult> Handle(TCommand command,CancellationToken cancellationToken);
    }
}
