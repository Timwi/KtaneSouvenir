using System;
using System.Reflection;

namespace Souvenir.Reflection
{
    class FieldInfo<T>
    {
        private readonly object _target;
        public readonly FieldInfo Field;

        public FieldInfo(object target, FieldInfo field)
        {
            _target = target;
            Field = field;
        }

        public T Get(Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = (T) Field.GetValue(_target);
            if (!nullAllowed && value == null)
                throw new AbandonModuleException("Field {0}.{1} is null.", Field.DeclaringType.FullName, Field.Name);
            string validatorFailMessage;
            if (validator != null && (validatorFailMessage = validator(value)) != null)
                throw new AbandonModuleException("Field {0}.{1} with value “{2}” did not pass validity check: {3}.", Field.DeclaringType.FullName, Field.Name, value, validatorFailMessage);
            return value;
        }

        public T GetFrom(object obj, Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = (T) Field.GetValue(obj);
            if (!nullAllowed && value == null)
                throw new AbandonModuleException("Field {0}.{1} is null.", Field.DeclaringType.FullName, Field.Name);
            string validatorFailMessage;
            if (validator != null && (validatorFailMessage = validator(value)) != null)
                throw new AbandonModuleException("Field {0}.{1} with value “{2}” did not pass validity check: {3}.", Field.DeclaringType.FullName, Field.Name, value, validatorFailMessage);
            return value;
        }

        public void Set(T value) { Field.SetValue(_target, value); }
    }

    sealed class IntFieldInfo : FieldInfo<int>
    {
        public IntFieldInfo(object target, FieldInfo field) : base(target, field) { }

        public int Get(int? expectedMinValue = null, int? expectedMaxValue = null)
        {
            var value = base.Get();
            if ((expectedMinValue != null && value < expectedMinValue.Value) || (expectedMaxValue != null && value > expectedMaxValue.Value))
                throw new AbandonModuleException("Int field {0}.{1} has value {2} (expected {3}-{4}).", Field.DeclaringType.FullName, Field.Name, value, expectedMinValue, expectedMaxValue);
            return value;
        }
    }

    sealed class ArrayFieldInfo<T> : FieldInfo<T[]>
    {
        public ArrayFieldInfo(object target, FieldInfo field) : base(target, field) { }

        public T[] Get(int expectedLength, bool nullArrayAllowed = false, bool nullContentAllowed = false)
        {
            var array = base.Get(nullAllowed: nullArrayAllowed);
            if (array == null)
                return null;
            int pos;
            if (!nullContentAllowed && (pos = array.IndexOf(v => v == null)) != -1)
                throw new AbandonModuleException("Array field {0}.{1} (length {2}) contained a null value at index {3}.", Field.DeclaringType.FullName, Field.Name, array.Length, pos);
            if (array.Length != expectedLength)
                throw new AbandonModuleException("Array field {0}.{1} has length {2} (expected {3}).", Field.DeclaringType.FullName, Field.Name, array.Length, expectedLength);
            return array;
        }

        public T[] Get(int expectedMinLength, int expectedMaxLength, bool nullArrayAllowed = false, bool nullContentAllowed = false)
        {
            var array = base.Get(nullAllowed: nullArrayAllowed);
            if (array == null)
                return null;
            int pos;
            if (!nullContentAllowed && (pos = array.IndexOf(v => v == null)) != -1)
                throw new AbandonModuleException("Array field {0}.{1} (length {2}) contained a null value at index {3}.", Field.DeclaringType.FullName, Field.Name, array.Length, pos);
            if (array.Length < expectedMinLength || array.Length > expectedMaxLength)
                throw new AbandonModuleException("Array field {0}.{1} has length {2} (expected {3}-{4}).", Field.DeclaringType.FullName, Field.Name, array.Length, expectedMinLength, expectedMaxLength);
            return array;
        }
    }
}
