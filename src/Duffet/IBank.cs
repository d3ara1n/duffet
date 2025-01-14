using System.Reflection;

namespace Duffet;

public interface IBank
{
    object[] Serve(MethodInfo method);
}