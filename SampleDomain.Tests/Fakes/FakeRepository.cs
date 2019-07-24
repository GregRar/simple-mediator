using SampleDomain.Core;
using System;

namespace SampleDomain.Tests.Fakes
{
    public class FakeRepository<TAggregate> : IRepository<TAggregate>
            where TAggregate : IAggregate
    {
        private readonly InMemoryDataStore dataStore;

        public FakeRepository(InMemoryDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public void DeleteAggregate(Guid id)
        {
            this.dataStore.Delete<TAggregate>(id);
        }

        public TAggregate Load(Guid id)
        {
            return this.dataStore.Load<TAggregate>(id);
        }

        public void Store(TAggregate aggregate)
        {
            var existingAggregate = this.dataStore.Load<TAggregate>(aggregate.Id);

            if (existingAggregate != null)
            {
                this.dataStore.Delete<TAggregate>(aggregate.Id);
            }

            this.dataStore.Add(aggregate);
        }
    }
}