using System;

namespace SampleDomain.Core
{
    public interface IRepository<TAggregate>
            where TAggregate : IAggregate
    {
        TAggregate Load(Guid aggregateId);

        void Store(TAggregate aggregate);

        void DeleteAggregate(Guid id);
    }
}