namespace Duffet.Builders;

public static class BankBuilderExtensions
{
    public static PropertyBuilder Property(this IBankBuilder self)
    {
        var builder = new PropertyBuilder();
        self.Add(builder);
        return builder;
    }
}