using System.Reflection;

namespace Duffet;

public class ConstantBank(object[] objects) : IBank
{
    public object[] Serve(MethodInfo method) => objects;
}