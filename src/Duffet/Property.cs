using System.Security.Cryptography.X509Certificates;

namespace Duffet;

public record Property
{
    public object Object { get; init; }

    public Lazy<object> Lazy { get; init; }

    public bool IsLazy { get; init; }

    public bool IsNamed { get; init; }

    public Type DeclaredType { get; init; }

    public string DeclaredName { get; init; }

    public static Property Lazied(Lazy<object> lazy, Type type,string name)
    {
        var property = new Property()
        {
            IsNamed = string.IsNullOrWhiteSpace(name) ? false : true,
            DeclaredName = name,
            IsLazy = true,
            Lazy = lazy,
            Object = default,
            DeclaredType = type
        };

        return property;
    }
    public static Property Lazied(Lazy<object> lazy, Type type) =>
        Lazied(lazy, type, string.Empty);

    public static Property Objected(object obj, Type type, string name)
    {
        var property = new Property()
        {
            IsNamed = string.IsNullOrWhiteSpace(name) ? false : true,
            DeclaredName = name,
            IsLazy = true,
            Lazy = default,
            Object = obj,
            DeclaredType = type
        };

        return property;
    }

    public static Property Objected(object obj, Type type) =>
        Objected(obj, type, string.Empty);

    public object GetValue() => IsLazy ? Lazy.Value : Object;
}