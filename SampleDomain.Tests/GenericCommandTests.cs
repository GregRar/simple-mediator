using SampleDomain.Commands;
using SampleDomain.Core;
using SampleDomain.Mediator;
using SampleDomain.Tests.Fakes;
using StructureMap;
using Xunit;

namespace SampleDomain.Tests
{
    public class GenericCommandTests
    {
        private readonly ICommandMediator mediator;

        private readonly EmailSender emailSender;

        public GenericCommandTests()
        {
            var container = new Container();
            container.Configure(configure =>
            {
                configure.AddRegistry(new TestRegistry());
            });

            this.mediator = container.GetInstance<ICommandMediator>();
            this.emailSender = container.GetInstance<EmailSender>();
        }

        [Fact]
        public void SendEmailGenericCommand()
        {
            mediator.Send(new SendEmailGenericCommand("Hello world!"));

            var lastSentMessage = this.emailSender.GetLastSentMessage(); ;
            Assert.Equal("Hello world!", lastSentMessage);
        }

        [Fact]
        public void SendEmailGenericCommandWithResponse()
        {
            var response = mediator.SendWithResponse(new SendEmailGenericCommandWithResponse("Hello world!"));

            Assert.True(response.Succeeded);

            var lastSentMessage = this.emailSender.GetLastSentMessage(); ;
            Assert.Equal("Hello world!", lastSentMessage);
        }

        [Fact]
        public void SendEmailGenericCommandWithResponse_UsingSendMethod_WhenIDontCareAboutResonse()
        {
            mediator.Send(new SendEmailGenericCommandWithResponse("Hello world!"));

            var lastSentMessage = this.emailSender.GetLastSentMessage(); ;
            Assert.Equal("Hello world!", lastSentMessage);
        }
    }
}