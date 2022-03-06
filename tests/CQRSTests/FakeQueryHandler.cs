using System.Threading;
using System.Threading.Tasks;
using YAEP.CQRS.Abstractions.Queries;

namespace CQRSTests
{
    public class FakeQueryHandler : IQueryHandler<FakeQuery, Person>
    {
        public Task<Person> Handle(FakeQuery query, CancellationToken cancellationToken)
        {
            return new Person(1, query.Value).AsTaskFromResult();
        }
    }
}
