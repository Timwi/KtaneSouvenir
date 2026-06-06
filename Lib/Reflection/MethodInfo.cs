using System;
using System.Linq;
using System.Reflection;

namespace Souvenir.Reflection;

internal sealed class MethodInfo<T>(object target, MethodInfo method) : InfoBase<T>(target)
{
    public MethodInfo Method { get; private set; } = method;
    protected override string LoggingString => $"Method {Method.DeclaringType.FullName}.{Method.Name}({Method.GetParameters().Select(p => $"{p.GetType().FullName} {p.Name}").JoinString(", ")})";

    public T Invoke(object[] arguments, Func<T, string> validator = null, bool nullAllowed = false) =>
        validate((T) Method.Invoke(_target, arguments), validator, nullAllowed, "{0} returned null.", "{0} returned value {1} which did not pass validity check: {2}.");

    public T InvokeOn(object target, object[] arguments, Func<T, string> validator = null, bool nullAllowed = false) =>
        validate((T) Method.Invoke(target, arguments), validator, nullAllowed, "{0} returned null.", "{0} returned value {1} which did not pass validity check: {2}.");
}
