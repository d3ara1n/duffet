namespace Duffet.Builders;

public interface IBankBuilder: IBuilder.IBuilder<IBank>
{
    IBankBuilder Add(PropertyBuilder property);
}