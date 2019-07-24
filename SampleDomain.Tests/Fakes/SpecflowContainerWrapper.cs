using SampleDomain.Core;
using StructureMap;
using System;

namespace SampleDomain.Tests.Fakes
{
    public class SpecflowContainerWrapper : SampleDomain.Core.IContainter
    {
        private readonly IContainer container;

        public SpecflowContainerWrapper(IContainer container)
        {
            this.container = container;
        }

        TType IContainter.Create<TType>()
        {
            return container.GetInstance<TType>();
        }

        public object Create(Type type)
        {
            return container.GetInstance(type);
        }
    }
}