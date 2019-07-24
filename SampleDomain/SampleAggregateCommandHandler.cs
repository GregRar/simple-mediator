using SampleDomain.Commands;
using SampleDomain.Mediator.HandlerTypes;
using System;

namespace SampleDomain
{
    public class SampleAggregateCommandHandler :
        ICreateAggregateWithResponseCommandHandler<CreateSampleAggregateCommand, SampleAggregate, CreateSampleAggregateCommandResponse>,
        IUpdateAggregateCommandHandler<UpdateSampleAggregateCommand, SampleAggregate>
    {
        public CreateAggregateCommandResponse<SampleAggregate, CreateSampleAggregateCommandResponse> Handle(CreateSampleAggregateCommand command)
        {
            var sampleAggregate = new SampleAggregate()
            {
                Id = command.Id,
                Name = command.Name
            };

            return new CreateAggregateCommandResponse<SampleAggregate, CreateSampleAggregateCommandResponse>(sampleAggregate, new CreateSampleAggregateCommandResponse { Succeeded = true });
        }

        public SampleAggregate Handle(UpdateSampleAggregateCommand command, Func<Guid, SampleAggregate> loadAggregate)
        {
            var sampleAggregate = loadAggregate(command.Id);
            sampleAggregate.Name = command.Name;
            return sampleAggregate;
        }
    }
}