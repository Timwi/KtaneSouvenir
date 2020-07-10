using System.Reflection;

namespace Souvenir.Reflection
{
    sealed class PropertyInfo<T>
    {
        private readonly object _target;
        public readonly PropertyInfo Property;

        public PropertyInfo(object target, PropertyInfo property)
        {
            _target = target;
            Property = property;
        }

        public T Get(bool nullAllowed = false)
        {
            // “This value should be null for non-indexed properties.” (MSDN)
            return Get(null, nullAllowed);
        }

        public T Get(object[] index, bool nullAllowed = false)
        {
            var t = (T) Property.GetValue(_target, index);
            if (!nullAllowed && t == null)
                throw new AbandonModuleException("Property {1}.{0} is null.", Property.Name, Property.DeclaringType.FullName);
            return t;
        }

        public T GetFrom(object obj, bool nullAllowed = false)
        {
            return GetFrom(obj, null, nullAllowed);
        }

        public T GetFrom(object obj, object[] index, bool nullAllowed = false)
        {
            var t = (T) Property.GetValue(obj, index);
            if (!nullAllowed && t == null)
                throw new AbandonModuleException("Property {1}.{0} is null.", Property.Name, Property.DeclaringType.FullName);
            return t;
        }

        public void Set(T value, object[] index = null)
        {
            Property.SetValue(_target, value, index);
        }
    }
}
