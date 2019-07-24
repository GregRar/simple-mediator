using SampleDomain.Commands;
using SampleDomain.Core;
using SampleDomain.Mediator;
using SampleDomain.Tests.Fakes;
using StructureMap;
using System;
using Xunit;

namespace SampleDomain.Tests
{
    public class CRUDCommandsTests
    {
        private readonly ICommandMediator mediator;

        private readonly IRepository<SampleAggregate> sampleAggregateRepository;

        public CRUDCommandsTests()
        {
            var container = new Container();
            container.Configure(configure =>
            {
                configure.AddRegistry(new TestRegistry());
            });

            this.mediator = container.GetInstance<ICommandMediator>();
            this.sampleAggregateRepository = container.GetInstance<IRepository<SampleAggregate>>();
        }

        [Fact]
        public void CreateSampleAggregateCommand_NoResponse()
        {
            var aggregateId = Guid.NewGuid();
            mediator.Send(new CreateSampleAggregateCommand(aggregateId, "Some name"));

            var aggregate = sampleAggregateRepository.Load(aggregateId);
            Assert.Equal("Some name", aggregate.Name);
        }

        [Fact]
        public void CreateSampleAggregateCommand_WithResponse()
        {
            var aggregateId = Guid.NewGuid();
            var response = mediator.SendWithResponse(new CreateSampleAggregateCommand(aggregateId, "Some name"));

            Assert.True(response.Succeeded);

            var aggregate = sampleAggregateRepository.Load(aggregateId);
            Assert.Equal("Some name", aggregate.Name);
        }

        [Fact]
        public void CreateSampleAggregateCommand_UsingSendMethod_WhenIDontCareAboutResonse()
        {
            var aggregateId = Guid.NewGuid();
            mediator.Send(new CreateSampleAggregateCommand(aggregateId, "Some name"));

            var aggregate = sampleAggregateRepository.Load(aggregateId);
            Assert.Equal("Some name", aggregate.Name);
        }

        [Fact]
        public void UpdateSampleAggregateCommand()
        {
            var aggregateId = Guid.NewGuid();
            mediator.Send(new CreateSampleAggregateCommand(aggregateId, "Some name"));
            mediator.Send(new UpdateSampleAggregateCommand(aggregateId, "Updated name"));

            var aggregate = sampleAggregateRepository.Load(aggregateId);
            Assert.Equal("Updated name", aggregate.Name);
        }
    }
}