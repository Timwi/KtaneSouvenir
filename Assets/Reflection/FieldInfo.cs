using System;
using System.Collections;
using System.Linq;
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
                throw new AbandonModuleException("Field {0}.{1} with value {2} did not pass validity check: {3}.", Field.DeclaringType.FullName, Field.Name, stringify(value), validatorFailMessage);
            return value;
        }

        public T GetFrom(object obj, Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = (T) Field.GetValue(obj);
            if (!nullAllowed && value == null)
                throw new AbandonModuleException("Field {0}.{1} is null.", Field.DeclaringType.FullName, Field.Name);
            string validatorFailMessage;
            if (validator != null && (validatorFailMessage = validator(value)) != null)
                throw new AbandonModuleException("Field {0}.{1} with value {2} did not pass validity check: {3}.", Field.DeclaringType.FullName, Field.Name, stringify(value), validatorFailMessage);
            return value;
        }

        public void Set(T value) { Field.SetValue(_target, value); }

        protected string stringify(object value)
        {
            if (value == null)
                return "<null>";
            var list = value as IList;
            if (list != null)
                return string.Format("[{0}]", list.Cast<object>().Select(obj => obj == null ? "<null>" : obj.ToString()).JoinString(", "));
            return string.Format("“{0}”", value);
        }
    }

    sealed class IntFieldInfo : FieldInfo<int>
    {
        public IntFieldInfo(object target, FieldInfo field) : base(target, field) { }

        public int Get(int? expectedMinValue = null, int? expectedMaxValue = null)
        {
            return Get(v => (expectedMinValue != null && v < expectedMinValue.Value) || (expectedMaxValue != null && v > expectedMaxValue.Value) ? string.Format("expected {0}–{1}", expectedMinValue, expectedMaxValue) : null);
        }
    }

    sealed class ArrayFieldInfo<T> : FieldInfo<T[]>
    {
        public ArrayFieldInfo(object target, FieldInfo field) : base(target, field) { }

        public T[] Get(int expectedLength, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<T, string> elementValidator = null)
        {
            return Get(expectedLength, expectedLength, nullArrayAllowed, nullContentAllowed, elementValidator);
        }

        public T[] Get(int expectedMinLength, int expectedMaxLength, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<T, string> elementValidator = null)
        {
            var array = Get(nullAllowed: nullArrayAllowed);
            if (array == null)
                return null;
            if (array.Length < expectedMinLength || array.Length > expectedMaxLength)
                throw new AbandonModuleException("Array field {0}.{1} has length {2} (expected {3}{4}).", Field.DeclaringType.FullName, Field.Name, array.Length,
                    expectedMinLength, expectedMaxLength != expectedMinLength ? "–" + expectedMaxLength : "");
            int pos;
            if (!nullContentAllowed && (pos = array.IndexOf(v => v == null)) != -1)
                throw new AbandonModuleException("Array field {0}.{1} (length {2}) contained a null value at index {3}.", Field.DeclaringType.FullName, Field.Name, array.Length, pos);
            string validatorFailMessage;
            if (elementValidator != null)
                for (var ix = 0; ix < array.Length; ix++)
                    if ((validatorFailMessage = elementValidator(array[ix])) != null)
                        throw new AbandonModuleException("Array field {0}.{1} (length {2}) contained value “{3}” at index {4} that failed the validator: {5}.",
                            Field.DeclaringType.FullName, Field.Name, array.Length, stringify(array[ix]), ix, validatorFailMessage);
            return array;
        }
    }
}
