using System;
using System.Collections.Generic;
using System.Reflection;

namespace Souvenir.Reflection;

internal class FieldInfo<T>(object target, FieldInfo field) : InfoBase<T>(target)
{
    public readonly FieldInfo Field = field;
    protected override string LoggingString => $"Field {Field.DeclaringType.FullName}.{Field.Name}";

    public T Get(Func<T, string> validator = null, bool nullAllowed = false)
        => validate((T) Field.GetValue(_target), validator, nullAllowed);

    public T GetFrom(object from, Func<T, string> validator = null, bool nullAllowed = false)
        => validate((T) Field.GetValue(from), validator, nullAllowed);

    public void Set(T value) => Field.SetValue(_target, value);
    public void SetTo(object target, T value) => Field.SetValue(target, value);
}

internal sealed class IntFieldInfo(object target, FieldInfo field) : FieldInfo<int>(target, field)
{
    public int Get(int? min = null, int? max = null) =>
        Get(v => (min != null && v < min.Value) || (max != null && v > max.Value) ? $"expected {min}–{max}" : null);

    public int GetFrom(object obj, int? min = null, int? max = null) =>
        GetFrom(obj, v => (min != null && v < min.Value) || (max != null && v > max.Value) ? $"expected {min}–{max}" : null);
}

internal abstract class CollectionFieldInfo<TCollection, TElement>(object target, FieldInfo field) : FieldInfo<TCollection>(target, field) where TCollection : IList<TElement>
{
    public TCollection Get(int expectedLength, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<TElement, string> validator = null) =>
        GetFrom(_target, expectedLength, expectedLength, nullArrayAllowed, nullContentAllowed, validator);

    public TCollection Get(int minLength, int? maxLength = null, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<TElement, string> validator = null) =>
        GetFrom(_target, minLength, maxLength, nullArrayAllowed, nullContentAllowed, validator);

    public TCollection GetFrom(object target, int expectedLength, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<TElement, string> validator = null) =>
        GetFrom(target, expectedLength, expectedLength, nullArrayAllowed, nullContentAllowed, validator);

    public TCollection GetFrom(object target, int minLength, int? maxLength = null, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<TElement, string> validator = null)
    {
        var collection = base.GetFrom(target, nullAllowed: nullArrayAllowed);
        if (collection == null)
            return collection;
        if (collection.Count < minLength || (maxLength != null && collection.Count > maxLength.Value))
            throw new AbandonModuleException($"Collection field {Field.DeclaringType.FullName}.{Field.Name} has length {collection.Count} (expected {(maxLength == null ? "at least " : minLength.ToString())}{(maxLength == null ? minLength.ToString() : maxLength.Value != minLength ? "–" + maxLength.Value : "")}).");
        int pos;
        if (!nullContentAllowed && (pos = collection.IndexOf(v => v == null)) != -1)
            throw new AbandonModuleException($"Collection field {Field.DeclaringType.FullName}.{Field.Name} (length {collection.Count}) contained a null value at index {pos}.");
        string validatorFailMessage;
        if (validator != null)
            for (var ix = 0; ix < collection.Count; ix++)
                if ((validatorFailMessage = validator(collection[ix])) != null)
                    throw new AbandonModuleException($"Collection field {Field.DeclaringType.FullName}.{Field.Name} (length {collection.Count}) contained value {collection[ix].Stringify()} at index {ix} that failed the validator: {validatorFailMessage}.");
        return collection;
    }

    public new TCollection Get(Func<TCollection, string> validator = null, bool nullAllowed = false)
    {
        var collection = base.Get(validator, nullAllowed);
        if (collection == null)
            return collection;
        var pos = collection.IndexOf(v => v == null);
        return pos != -1
            ? throw new AbandonModuleException($"Collection field {Field.DeclaringType.FullName}.{Field.Name} (length {collection.Count}) contained a null value at index {pos}.")
            : collection;
    }

    public new TCollection GetFrom(object obj, Func<TCollection, string> validator = null, bool nullAllowed = false)
    {
        var collection = base.GetFrom(obj, validator, nullAllowed);
        if (collection == null)
            return collection;
        var pos = collection.IndexOf(v => v == null);
        return pos != -1
            ? throw new AbandonModuleException($"Collection field {Field.DeclaringType.FullName}.{Field.Name} (length {collection.Count}) contained a null value at index {pos}.")
            : collection;
    }
}

internal sealed class ArrayFieldInfo<T>(object target, FieldInfo field) : CollectionFieldInfo<T[], T>(target, field)
{
}

internal sealed class ListFieldInfo<T>(object target, FieldInfo field) : CollectionFieldInfo<List<T>, T>(target, field)
{
}
