using IBuilder;
using System.Collections.Generic;
using System.Linq;

namespace Duffet.Builders;

public class BankBuilder : IBankBuilder
{
    private readonly List<PropertyBuilder> builders = new();

    public IBank Build()
    {
        var bank = new Bank(builders.Select(x => x.Build()));
        return bank;
    }

    public IBankBuilder Add(PropertyBuilder property)
    {
        builders.Add(property);
        return this;
    }
}
