using Duffet.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Duffet;

public class Bank
{
    private readonly Dictionary<string, Property> namedProperties = new();
    private readonly List<Property> remainingProperties = new();

    public Bank(IEnumerable<Property> storage)
    {
        foreach (var property in storage)
            if (property.IsNamed)
                namedProperties.Add(property.Name, property);
            else
                remainingProperties.Add(property);
    }

    public static BankBuilder Builder()
    {
        return new BankBuilder();
    }

    public object[] Serve(MethodInfo method)
    {
        var parameters = method.GetParameters();
        var arguments = new object[parameters.Length];
        for (var i = 0; i < arguments.Length; i++)
        {
            if (namedProperties.ContainsKey(parameters[i].Name))
            {
                arguments[i] = namedProperties[parameters[i].Name].AdaptValue(
                    parameters[i].ParameterType
                );
                continue;
            }

            foreach (var property in remainingProperties)
                if (property.Type.IsAssignableTo(parameters[i].ParameterType))
                {
                    arguments[i] = property.GetValue();
                    break;
                }
                else
                {
                    foreach (var adapted in property.AdaptedTypes)
                        if (adapted.Item1.IsAssignableTo(parameters[i].ParameterType))
                        {
                            arguments[i] = adapted.Item2(
                                property.GetValue(),
                                parameters[i].ParameterType
                            );
                            break;
                        }
                }

            if (arguments[i] == null)
            {
                if (parameters[i].HasDefaultValue)
                    arguments[i] = parameters[i].DefaultValue!;
                else
                    throw new Exception("parameter has no target found");
            }
        }

        return arguments;
    }
}
