using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

namespace Souvenir
{
    static class Ut
    {
        /// <summary>
        ///     Returns all fields contained in the specified type, including private fields inherited from base classes.</summary>
        /// <param name="type">
        ///     The type to return all fields of.</param>
        /// <returns>
        ///     An <see cref="IEnumerable&lt;FieldInfo&gt;"/> containing all fields contained in this type, including private
        ///     fields inherited from base classes.</returns>
        public static IEnumerable<FieldInfo> GetAllFields(this Type type)
        {
            IEnumerable<FieldInfo> fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var baseType = type.BaseType;
            return (baseType == null) ? fields : GetAllFields(baseType).Concat(fields);
        }

        /// <summary>Searches the specified object’s type for a field of the specified name and returns that field’s value.</summary>
        /// <typeparam name="T">Expected type of the field.</typeparam>
        /// <param name="instance">Instance from which to retrieve the field value.</param>
        /// <param name="fieldName">Name of the field to return the value of.</param>
        /// <returns>The value of the field.</returns>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item><description>The field is of a different type than specified.</description></item>
        /// <item><description>There is no field with the specified name.</description></item>
        /// </list>
        /// </exception>
        /// <remarks>This method is intended to be used only for debugging. Do not rely on it in production code.</remarks>
        public static T GetFieldValue<T>(this object instance, string fieldName)
        {
            var field = instance.GetType().GetAllFields().Single(f => f.Name == fieldName);
            if (!typeof(T).IsAssignableFrom(field.FieldType))
                throw new InvalidOperationException(string.Format("Field is of type {1}, but was expected to be of type {0} (or derived from it).", typeof(T).FullName, field.FieldType.FullName));
            return (T) field.GetValue(instance);
        }

        /// <summary>
        ///     Brings the elements of the given list into a random order.</summary>
        /// <typeparam name="T">
        ///     Type of elements in the list.</typeparam>
        /// <param name="list">
        ///     List to shuffle.</param>
        /// <returns>
        ///     The list operated on.</returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            for (int j = list.Count; j >= 1; j--)
            {
                int item = Rnd.Range(0, j);
                if (item < j - 1)
                {
                    var t = list[item];
                    list[item] = list[j - 1];
                    list[j - 1] = t;
                }
            }
            return list;
        }

        /// <summary>
        ///     Converts an <c>IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;</c> into a <c>Dictionary&lt;TKey,
        ///     TValue&gt;</c>.</summary>
        /// <param name="source">
        ///     Source collection to convert to a dictionary.</param>
        /// <param name="comparer">
        ///     An optional equality comparer to compare keys.</param>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer = null)
        {
            return source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value, comparer ?? EqualityComparer<TKey>.Default);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member, bool inherit = false) where TAttribute : class
        {
            var attrs = member.GetCustomAttributes(typeof(TAttribute), inherit);
            if (attrs.Length == 0)
                return null;
            return (TAttribute) attrs[0];
        }

        /// <summary>Allows the use of type inference when creating .NET’s KeyValuePair&lt;TK,TV&gt;.</summary>
        public static KeyValuePair<TKey, TValue> KeyValuePair<TKey, TValue>(TKey key, TValue value) { return new KeyValuePair<TKey, TValue>(key, value); }

        /// <summary>
        ///     Adds an element to a List&lt;V&gt; stored in the current IDictionary&lt;K, List&lt;V&gt;&gt;. If the specified
        ///     key does not exist in the current IDictionary, a new List is created.</summary>
        /// <typeparam name="K">
        ///     Type of the key of the IDictionary.</typeparam>
        /// <typeparam name="V">
        ///     Type of the values in the Lists.</typeparam>
        /// <param name="dic">
        ///     IDictionary to operate on.</param>
        /// <param name="key">
        ///     Key at which the list is located in the IDictionary.</param>
        /// <param name="value">
        ///     Value to add to the List located at the specified Key.</param>
        public static void AddSafe<K, V>(this IDictionary<K, List<V>> dic, K key, V value)
        {
            if (dic == null)
                throw new ArgumentNullException("dic");
            if (key == null)
                throw new ArgumentNullException("key", "Null values cannot be used for keys in dictionaries.");
            if (!dic.ContainsKey(key))
                dic[key] = new List<V>();
            dic[key].Add(value);
        }

        /// <summary>
        ///     Increments an integer in an <see cref="IDictionary&lt;K, V&gt;"/> by the specified amount. If the specified
        ///     key does not exist in the current dictionary, the value <paramref name="amount"/> is inserted.</summary>
        /// <typeparam name="K">
        ///     Type of the key of the dictionary.</typeparam>
        /// <param name="dic">
        ///     Dictionary to operate on.</param>
        /// <param name="key">
        ///     Key at which the list is located in the dictionary.</param>
        /// <param name="amount">
        ///     The amount by which to increment the integer.</param>
        /// <returns>
        ///     The new value at the specified key.</returns>
        public static int IncSafe<K>(this IDictionary<K, int> dic, K key, int amount = 1)
        {
            if (dic == null)
                throw new ArgumentNullException("dic");
            if (key == null)
                throw new ArgumentNullException("key", "Null values cannot be used for keys in dictionaries.");
            if (!dic.ContainsKey(key))
                return (dic[key] = amount);
            else
                return (dic[key] = dic[key] + amount);
        }

        /// <summary>
        ///     Gets a value from a dictionary by key. If the key does not exist in the dictionary, the default value is
        ///     returned instead.</summary>
        /// <param name="dict">
        ///     Dictionary to operate on.</param>
        /// <param name="key">
        ///     Key to look up.</param>
        /// <param name="defaultVal">
        ///     Value to return if key is not contained in the dictionary.</param>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultVal = default(TValue))
        {
            if (dict == null)
                throw new ArgumentNullException("dict");
            if (key == null)
                throw new ArgumentNullException("key", "Null values cannot be used for keys in dictionaries.");
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            else
                return defaultVal;
        }

        public static Vector3 SetX(this Vector3 orig, float x) { return new Vector3(x, orig.y, orig.z); }
    }
}
