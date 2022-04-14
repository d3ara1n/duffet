using System.Security.AccessControl;
using IBuilder;

namespace Duffet;

public class BankBuilder : IBuilder<Bank>
{
    List<Property> properties = new();

    public Bank Build()
    {
        return new Bank(properties);
    }

    public BankBuilder AddProperty(object obj, Type type, string name)
    {
        properties.Add(Property.Objected(obj, type, name));
        return this;
    }

    public BankBuilder AddPropertyLazied(Func<object> lazy, Type type,string name)
    {
        properties.Add(Property.Lazied(new Lazy<object>(lazy), type, name));
        return this;
    }
}