using System;
using System.Reflection;

namespace Souvenir.Reflection
{
    internal sealed class PropertyInfo<T> : InfoBase<T>
    {
        public readonly PropertyInfo Property;
        protected override string LoggingString => $"Property {Property.DeclaringType.FullName}.{Property.Name}";
        public PropertyInfo(object target, PropertyInfo property) : base(target) { Property = property; }
        protected override T GetValue() => (T) Property.GetValue(_target, null);
        protected override T GetValue(object from) => (T) Property.GetValue(from, null);

        public T Get(object[] index, Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = (T) Property.GetValue(_target, index);
            if (!nullAllowed && value == null)
                throw new AbandonModuleException($"Property {Property.DeclaringType.FullName}.{Property.Name} is null.");
            string validatorFailMessage;
            return validator != null && (validatorFailMessage = validator(value)) != null
                ? throw new AbandonModuleException($"Property {Property.DeclaringType.FullName}.{Property.Name} with value {value.Stringify()} did not pass validity check: {validatorFailMessage}.")
                : value;
        }

        public T GetFrom(object obj, object[] index, bool nullAllowed = false)
        {
            var t = (T) Property.GetValue(obj, index);
            return !nullAllowed && t == null
                ? throw new AbandonModuleException($"Property {Property.DeclaringType.FullName}.{Property.Name} is null.")
                : t;
        }

        public void Set(T value, object[] index = null) => Property.SetValue(_target, value, index);

        public void SetTo(object target, T value, object[] index = null) => Property.SetValue(target, value, index);
    }
}
