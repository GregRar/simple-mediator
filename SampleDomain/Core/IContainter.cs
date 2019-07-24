using System;

namespace SampleDomain.Core
{
    public interface IContainter
    {
        TType Create<TType>();

        object Create(Type type);
    }
}