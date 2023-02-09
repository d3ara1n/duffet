using System;
using System.Collections.Generic;

namespace Duffet;

public record Property
{
    public string Name { get; set; }
    public bool IsNamed => !string.IsNullOrWhiteSpace(Name);
    public Type Type { get; set; }
    public object Value { get; set; }
    public bool IsValueLazy { get; set; }

    public IEnumerable<(Type, Func<object, Type, object>)> AdaptedTypes { get; set; }

    public object GetValue()
    {
        return IsValueLazy ? ((Lazy<object>)Value).Value : Value;
    }

    public object AdaptValue(Type type)
    {
        if (Type == type) return GetValue();
        foreach (var casting in AdaptedTypes)
            if (casting.Item1.IsAssignableTo(type))
                return casting.Item2.Invoke(GetValue(), type);
        throw new Exception("type not suitable");
    }
}