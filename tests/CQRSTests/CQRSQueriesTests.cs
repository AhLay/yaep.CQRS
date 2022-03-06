using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using YAEP.CQRS.Abstractions.Queries;

namespace CQRSTests
{
    public class CQRSQueriesTests
    {

        [Fact]
        public void Registerqueries_given_an_assembly_with_QueryHandler_implementations_registres_them()
        {
            var services = new ServiceCollection();

            services.RegisterQueries(typeof(CQRSQueriesTests).Assembly);
            var serviceProvider = services.BuildServiceProvider();
            var handler = serviceProvider.GetRequiredService<IQueryHandler<FakeQuery,Person>>();

            handler.Should().NotBeNull();
            handler.GetType().Should().Be(typeof(FakeQueryHandler));
        }


        [Fact]
        public async Task ExecuteQuery_given_a_registred_QueryHandler_dipaches_it_and_invokes_Handle()
        {
            var services = new ServiceCollection();

            services.RegisterQueries(typeof(CQRSQueriesTests).Assembly);
            var serviceProvider = services.BuildServiceProvider();
            var dispatcher = serviceProvider.GetRequiredService<IQueryExecuter>();

            var result = await dispatcher.Fetch<FakeQuery,Person>(new FakeQuery("test"), CancellationToken.None);

            result.Name.Should().Be("test");
        }
    }
}
