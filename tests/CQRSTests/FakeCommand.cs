using YAEP.CQRS.Abstractions.Commands;

namespace CQRSTests
{
    public record FakeCommand(string value) : ICommand;
}
