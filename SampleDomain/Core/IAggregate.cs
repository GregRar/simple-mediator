using System;

namespace SampleDomain.Core
{
    public interface IAggregate
    {
        Guid Id { get; set; }
    }
}