using System;
using System.Collections.Generic;
using IBuilder;

namespace Duffet.Builders;

public class PropertyBuilder : IBuilder<Property>
{
    private readonly List<(Type, Func<object, Type, object>)> adapters = new();
    private readonly Property inner = new();

    public Property Build()
    {
        inner.AdaptedTypes = adapters;
        return inner;
    }

    public PropertyBuilder Named(string name)
    {
        inner.Name = name;
        return this;
    }

    public PropertyBuilder Typed(Type type)
    {
        inner.Type = type;
        return this;
    }

    public PropertyBuilder WithObject(object obj)
    {
        inner.Value = obj;
        inner.IsValueLazy = false;
        return this;
    }

    public PropertyBuilder WithLazy(Lazy<object> lazy)
    {
        inner.Value = lazy;
        inner.IsValueLazy = true;
        return this;
    }

    public PropertyBuilder HasTypeAdapted(Type type, Func<object, Type, object> casting)
    {
        if (inner.Type != type)
            adapters.Add((type, casting));
        return this;
    }
}