using System.Reflection;

namespace Souvenir.Reflection
{
    sealed class FieldInfo<T>
    {
        private readonly object _target;
        public readonly FieldInfo Field;

        public FieldInfo(object target, FieldInfo field)
        {
            _target = target;
            Field = field;
        }

        public T Get(bool nullAllowed = false)
        {
            var t = (T) Field.GetValue(_target);
            if (!nullAllowed && t == null)
                throw new AbandonModuleException("Field {1}.{0} is null.", Field.Name, Field.DeclaringType.FullName);
            return t;
        }

        public T GetFrom(object obj, bool nullAllowed = false)
        {
            var t = (T) Field.GetValue(obj);
            if (!nullAllowed && t == null)
                throw new AbandonModuleException("Field {1}.{0} is null.", Field.Name, Field.DeclaringType.FullName);
            return t;
        }

        public void Set(T value) { Field.SetValue(_target, value); }
    }
}
