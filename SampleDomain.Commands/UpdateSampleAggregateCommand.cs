﻿using System;

namespace SampleDomain.Commands
{
    public class UpdateSampleAggregateCommand
    {
        public UpdateSampleAggregateCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}