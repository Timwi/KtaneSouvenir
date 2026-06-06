using System;
using System.Reflection;

namespace Souvenir.Reflection;

internal sealed class PropertyInfo<T>(object target, PropertyInfo property) : InfoBase<T>(target)
{
    protected override string LoggingString => $"Property {property.DeclaringType.FullName}.{property.Name}";

    public T Get(object[] index = null, Func<T, string> validator = null, bool nullAllowed = false)
        => validate((T) property.GetValue(_target, index), validator, nullAllowed);

    public T GetFrom(object from, object[] index = null, Func<T, string> validator = null, bool nullAllowed = false)
        => validate((T) property.GetValue(from, index), validator, nullAllowed);

    public void Set(T value, object[] index = null) => property.SetValue(_target, value, index);

    public void SetTo(object target, T value, object[] index = null) => property.SetValue(target, value, index);
}
