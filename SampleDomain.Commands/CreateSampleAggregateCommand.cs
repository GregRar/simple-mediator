using System;

namespace SampleDomain.Commands
{
    public class CreateSampleAggregateCommand : ICommandResponse<CreateSampleAggregateCommandResponse>
    {
        public CreateSampleAggregateCommand(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}