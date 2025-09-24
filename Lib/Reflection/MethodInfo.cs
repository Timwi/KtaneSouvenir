using System.Reflection;

namespace Souvenir.Reflection;

internal sealed class MethodInfo<T>
{
    private readonly object _target;
    public MethodInfo Method { get; private set; }

    public MethodInfo(object target, MethodInfo method)
    {
        _target = target;
        Method = method;
    }

    public T Invoke(params object[] arguments) => (T) Method.Invoke(_target, arguments);

    public T InvokeOn(object target, params object[] arguments) => (T) Method.Invoke(target, arguments);
}
