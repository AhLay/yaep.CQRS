using System.Threading;
using System.Threading.Tasks;
using YAEP.CQRS.Abstractions;
using YAEP.CQRS.Abstractions.Commands;

namespace CQRSTests
{
    public class FakeCommandHandler : ICommandHandler<FakeCommand>
    {
        public Task<IResult> Handle(FakeCommand command, CancellationToken cancellationToken)
        {
            return new FakeResult().AsTaskFromResult<IResult>();
        }
    }
}
