using System.Threading.Tasks;

namespace Duffet.Builders;

// 多次构造只在堆上分配一次，除非对结构的写发生
public struct LazyBankBuilder : IBankBuilder
{
    private static readonly ConstantBank EMPTY = new ConstantBank([]);
    private BankBuilder? _actual;

    public IBank Build()
    {
        return _actual?.Build() ?? EMPTY;
    }

    public IBankBuilder Add(PropertyBuilder property)
    {
        _actual = new BankBuilder();
        _actual.Add(property);
        return this;
    }
}