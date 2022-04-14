using System.Reflection;

namespace Duffet;

public class Bank
{

    IEnumerable<Property> namedProperties;
    IEnumerable<Property> unamedProperties;

    public BankBuilder Builder() =>
        new BankBuilder();

    public Bank(IEnumerable<Property> properties)
    {
        namedProperties = properties.Where(x => x.IsNamed);
        unamedProperties = properties.Except(namedProperties);
    }
    public object[] Serve(MethodInfo method)
    {
        var parameters = method.GetParameters();
        var arguments = new object[parameters.Length];
        for (int i = 0; i < arguments.Length; i++)
        {
            var parameter = parameters[i];
            foreach (var property in namedProperties)
            {
                if (property.DeclaredName == parameter.Name)
                {
                    if (property.DeclaredType == parameter.ParameterType)
                    {
                        arguments[i] = property.GetValue();
                        break;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("name found but not the type");
                    }
                }
            }
            foreach(var property in unamedProperties)
            {
                if(property.DeclaredType == parameter.ParameterType)
                {
                    arguments[i] = property.GetValue();
                    break;
                }
            }

            if(arguments[i] == null)
            {
                throw new ArgumentNullException("not found");
            }
        }
        return arguments;
    }
}