using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using YAEP.CQRS.Abstractions.Commands;

namespace CQRSTests
{

    public class CQRSCommandsTests
    {

        [Fact]
        public void RegisterCommands_given_an_assembly_with_CommandHandler_implementations_registres_them()
        {
            var services = new ServiceCollection();

            services.RegisterCommands(typeof(FakeCommand).Assembly);
            var serviceProvider = services.BuildServiceProvider();
            var handler = serviceProvider.GetRequiredService<ICommandHandler<FakeCommand>>();

            handler.Should().NotBeNull();
            handler.GetType().Should().Be(typeof(FakeCommandHandler));
        }


        [Fact]
        public async Task ExecuteCommand_given_a_registred_CommandHandler_dipaches_it_and_invokes_Handle()
        {
            var services = new ServiceCollection();
            
            services.RegisterCommands(typeof(FakeCommand).Assembly);
            var serviceProvider = services.BuildServiceProvider();
            var dispatcher = serviceProvider.GetRequiredService<ICommandExecuter>();

            var result = await dispatcher.Execute(new FakeCommand("test"), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
        }
    }
}
