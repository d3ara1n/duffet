using System;
using System.Collections.Generic;
using System.Reflection;
using Duffet.Builders;

namespace Duffet;

public class Bank
{
    Dictionary<string, Property> namedProperties = new();
    List<Property> remainingProperties = new();

    public Bank(IEnumerable<Property> storage)
    {
        foreach(var property in storage)
        {
            if(property.IsNamed)
            {
                namedProperties.Add(property.Name, property);
            }else
            {
                remainingProperties.Add(property);
            }
        }
    }
    public static BankBuilder Builder() => new();

    public object[] Serve(MethodInfo method)
    {
        var parameters = method.GetParameters();
        var arguments = new object[parameters.Length];
        for (int i = 0; i < arguments.Length; i++)
        {
            if (namedProperties.ContainsKey(parameters[i].Name))
            {
                arguments[i] = namedProperties[parameters[i].Name].AdaptValue(parameters[i].ParameterType);
                break;
            }
            else
            {
                foreach (var property in remainingProperties)
                {
                    if (property.Type == parameters[i].ParameterType)
                    {
                        arguments[i] = property.GetValue();
                        break;
                    }
                }
            }
            if (arguments[i] == null)
            {
                throw new Exception("parameter has no target found");
            }
        }
        return arguments;
    }
}