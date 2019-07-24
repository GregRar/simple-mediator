using SampleDomain.Core;
using System;

namespace SampleDomain
{
    public class SampleAggregate : IAggregate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}