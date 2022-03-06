using YAEP.CQRS.Abstractions.Queries;

namespace CQRSTests
{
    public record FakeQuery(string Value): IQuery<Person>;
}
