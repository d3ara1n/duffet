using System.Reflection;

namespace Duffet;

public static class BankBuilderExtensions
{
    public static BankBuilder AddProperty(this BankBuilder builder, object obj, Type type)
    {
        return builder.AddProperty(obj, type, string.Empty);
    }

    public static BankBuilder AddPropertyLazied(this BankBuilder builder, Func<object> lazy, Type type)
    {
        return builder.AddPropertyLazied(lazy, type, string.Empty);
    }

    public static BankBuilder AddFromSource(this BankBuilder builder, object obj)
    {
        var properties = obj.GetType().GetProperties(BindingFlags.Public);
        foreach(var property in properties)
        {
            builder.AddProperty(property.GetValue(obj), property.DeclaringType);
        }

        return builder;
    }
}