using SampleDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleDomain.Tests.Fakes
{
    public class InMemoryDataStore
    {
        public List<object> Data { get; } = new List<object>();

        public TAggregate Load<TAggregate>(Guid id)
            where TAggregate : IAggregate
        {
            var aggregate = this.Data.SingleOrDefault(a => (a is TAggregate) && ((TAggregate)a).Id == id);
            return (TAggregate)aggregate;
        }

        public void Add<TAggregate>(TAggregate aggregate)
                where TAggregate : IAggregate
        {
            this.Data.Add(aggregate);
        }

        public void Delete<TAggregate>(Guid id)
                where TAggregate : IAggregate
        {
            var aggregate = this.Load<TAggregate>(id);
            this.Data.Remove(aggregate);
        }
    }
}