using YAEP.CQRS.Abstractions;
using YAEP.CQRS.Abstractions.Commands;

namespace YAEP.CQRS
{
    public sealed class CommandExecuter : ICommandExecuter
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandExecuter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.GuardAgainstNull(nameof(serviceProvider));
        }
        public Task<IResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            handler.GuardAgainst(v => v.IsNull(), $"No Handler found for the command : [{typeof(TCommand)}]. make sure you registred the CommandHandler");

            return handler.Handle(command, cancellationToken);
        }
    }
}
