using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

namespace Souvenir
{
    static class Ut
    {
        public static Vector3 SetX(this Vector3 orig, float x) { return new Vector3(x, orig.y, orig.z); }

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

        /// <summary>
        ///     Searches the specified object’s type for a field of the specified name and returns that field’s value.</summary>
        /// <typeparam name="T">
        ///     Expected type of the field.</typeparam>
        /// <param name="instance">
        ///     Instance from which to retrieve the field value.</param>
        /// <param name="fieldName">
        ///     Name of the field to return the value of.</param>
        /// <returns>
        ///     The value of the field.</returns>
        /// <exception cref="InvalidOperationException">
        ///     <list type="bullet">
        ///         <item><description>
        ///             The field is of a different type than specified.</description></item>
        ///         <item><description>
        ///             There is no field with the specified name.</description></item></list></exception>
        /// <remarks>
        ///     This method is intended to be used only for debugging. Do not rely on it in production code.</remarks>
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

        public static T[] NewArray<T>(params T[] array) { return array; }

        /// <summary>
        ///     Turns all elements in the enumerable to strings and joins them using the specified <paramref
        ///     name="separator"/> and the specified <paramref name="prefix"/> and <paramref name="suffix"/> for each string.</summary>
        /// <param name="values">
        ///     The sequence of elements to join into a string.</param>
        /// <param name="separator">
        ///     Optionally, a separator to insert between each element and the next.</param>
        /// <param name="prefix">
        ///     Optionally, a string to insert in front of each element.</param>
        /// <param name="suffix">
        ///     Optionally, a string to insert after each element.</param>
        /// <param name="lastSeparator">
        ///     Optionally, a separator to use between the second-to-last and the last element.</param>
        /// <example>
        ///     <code>
        ///         // Returns "[Paris], [London], [Tokyo]"
        ///         (new[] { "Paris", "London", "Tokyo" }).JoinString(", ", "[", "]")
        ///         
        ///         // Returns "[Paris], [London] and [Tokyo]"
        ///         (new[] { "Paris", "London", "Tokyo" }).JoinString(", ", "[", "]", " and ");</code></example>
        public static string JoinString<T>(this IEnumerable<T> values, string separator = null, string prefix = null, string suffix = null, string lastSeparator = null)
        {
            if (values == null)
                throw new ArgumentNullException("values");
            if (lastSeparator == null)
                lastSeparator = separator;

            using (var enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return "";

                // Optimise the case where there is only one element
                var one = enumerator.Current;
                if (!enumerator.MoveNext())
                    return prefix + one + suffix;

                // Optimise the case where there are only two elements
                var two = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    // Optimise the (common) case where there is no prefix/suffix; this prevents an array allocation when calling string.Concat()
                    if (prefix == null && suffix == null)
                        return one + lastSeparator + two;
                    return prefix + one + suffix + lastSeparator + prefix + two + suffix;
                }

                StringBuilder sb = new StringBuilder()
                    .Append(prefix).Append(one).Append(suffix).Append(separator)
                    .Append(prefix).Append(two).Append(suffix);
                var prev = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    sb.Append(separator).Append(prefix).Append(prev).Append(suffix);
                    prev = enumerator.Current;
                }
                sb.Append(lastSeparator).Append(prefix).Append(prev).Append(suffix);
                return sb.ToString();
            }
        }

        public static T PickRandom<T>(this IEnumerable<T> src)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            var arr = (src as IList<T>) ?? src.ToArray();
            if (arr.Count == 0)
                throw new InvalidOperationException("Cannot pick a random element from an empty set.");
            return arr[Rnd.Range(0, arr.Count)];
        }

        /// <summary>
        ///     Returns the set of enum values from the specified enum type.</summary>
        /// <typeparam name="T">
        ///     The enum type from which to retrieve the values.</typeparam>
        /// <returns>
        ///     A strongly-typed array containing the enum values from the specified type.</returns>
        public static T[] GetEnumValues<T>()
        {
            return (T[]) Enum.GetValues(typeof(T));
        }

        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Action Lambda(Action method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Action<T> Lambda<T>(Action<T> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Action<T1, T2> Lambda<T1, T2>(Action<T1, T2> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Action<T1, T2, T3> Lambda<T1, T2, T3>(Action<T1, T2, T3> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Action<T1, T2, T3, T4> Lambda<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Func<TResult> Lambda<TResult>(Func<TResult> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Func<T, TResult> Lambda<T, TResult>(Func<T, TResult> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Func<T1, T2, TResult> Lambda<T1, T2, TResult>(Func<T1, T2, TResult> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Func<T1, T2, T3, TResult> Lambda<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> method) { return method; }
        /// <summary>
        ///     Allows the use of C#’s powerful type inference when declaring local lambdas whose delegate type doesn't make
        ///     any difference.</summary>
        public static Func<T1, T2, T3, T4, TResult> Lambda<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> method) { return method; }

        public static IEnumerable<string> WordWrap(this string text, Func<int, double> wrapWidth, double widthOfASpace, Func<string, double> measure, bool allowBreakingWordsApart)
        {
            var curLine = 0;
            var atStartOfLine = true;
            var x = 0.0;
            var wordPieces = new List<string>();
            var wordPiecesWidths = new List<double>();
            var wordPiecesWidthsSum = 0.0;
            var actualWidth = 0.0;
            var numSpaces = 0;

            var sb = new StringBuilder();

            var renderSpaces = Lambda(() =>
            {
                sb.Append(' ', numSpaces);
                x += numSpaces * widthOfASpace;
                actualWidth = Math.Max(actualWidth, x);
            });

            var renderPieces = Lambda(() =>
            {
                // Add a space if we are not at the beginning of the line.
                if (!atStartOfLine)
                    renderSpaces();
                for (int j = 0; j < wordPieces.Count; j++)
                    sb.Append(wordPieces[j]);
                x += wordPiecesWidthsSum;
                actualWidth = Math.Max(actualWidth, x);
                wordPieces.Clear();
                wordPiecesWidths.Clear();
                wordPiecesWidthsSum = 0;
            });

            // The parameter is not used, but it may be useful in future
            var advanceToNextLine = Lambda((bool newParagraph) =>
            {
                var line = sb.ToString();
                sb = new StringBuilder();
                x = 0;
                atStartOfLine = true;
                curLine++;
                return line;
            });

            var i = 0;
            while (i < text.Length)
            {
                // Check whether we are looking at a whitespace character or not, and if not, find the end of the word.
                int lengthOfWord = 0;
                while (lengthOfWord + i < text.Length && !isWrappableAfter(text, lengthOfWord + i) && text[lengthOfWord + i] != '\n')
                    lengthOfWord++;

                if (lengthOfWord > 0)
                {
                    // We are looking at a word. (It doesn’t matter whether we’re at the beginning of the word or in the middle of one.)
                    retry1:
                    string fragment = text.Substring(i, lengthOfWord);
                    var fragmentWidth = measure(fragment);
                    retry2:

                    // If we are at the start of a line, and the word itself doesn’t fit on a line by itself, give up
                    if (atStartOfLine && x + wordPiecesWidthsSum + fragmentWidth > wrapWidth(curLine))
                    {
                        if (!allowBreakingWordsApart)
                        {
                            // Return null to signal that we encountered a word that doesn’t fit in a line.
                            yield return null;
                            yield break;
                        }

                        // We don’t know exactly where to break the word, so use binary search to discover where that is.
                        if (lengthOfWord > 1)
                        {
                            lengthOfWord /= 2;
                            goto retry1;
                        }

                        // If we get to here, ‘WordPieces’ contains as much of the word as fits into one line, and the next letter makes it too long.
                        // If ‘WordPieces’ is empty, we are at the beginning of a paragraph and the first letter already doesn’t fit.
                        if (wordPieces.Count > 0)
                        {
                            // Render the part of the word that fits on the line and then move to the next line.
                            renderPieces();
                            yield return advanceToNextLine(false);
                        }
                    }
                    else if (!atStartOfLine && x + numSpaces * widthOfASpace + wordPiecesWidthsSum + fragmentWidth > wrapWidth(curLine))
                    {
                        // We have already rendered some text on this line, but the word we’re looking at right now doesn’t
                        // fit into the rest of the line, so leave the rest of this line blank and advance to the next line.
                        yield return advanceToNextLine(false);

                        // In case the word also doesn’t fit on a line all by itself, go back to top (now that ‘AtStartOfLine’ is true)
                        // where it will check whether we need to break the word apart.
                        goto retry2;
                    }

                    // If we get to here, the current fragment fits on the current line (or it is a single character that overflows
                    // the line all by itself).
                    wordPieces.Add(fragment);
                    wordPiecesWidths.Add(fragmentWidth);
                    wordPiecesWidthsSum += fragmentWidth;
                    i += lengthOfWord;
                    continue;
                }

                // We encounter a whitespace character. All the word pieces fit on the current line, so render them.
                if (wordPieces.Count > 0)
                {
                    renderPieces();
                    atStartOfLine = false;
                }

                if (text[i] == '\n')
                {
                    // If the whitespace character is actually a newline, start a new paragraph.
                    yield return advanceToNextLine(true);
                    i++;
                }
                else
                {
                    // Discover the extent of the spaces.
                    numSpaces = 0;
                    while (numSpaces + i < text.Length && isWrappableAfter(text, numSpaces + i) && text[numSpaces + i] != '\n')
                        numSpaces++;
                    i += numSpaces;

                    if (atStartOfLine)
                    {
                        // If we are at the beginning of the line, treat these spaces as the paragraph’s indentation.
                        renderSpaces();
                    }
                }
            }

            renderPieces();
            if (sb.Length > 0)
                yield return sb.ToString();
        }

        private static bool isWrappableAfter(string txt, int index)
        {
            // Return false for all the whitespace characters that should NOT be wrappable
            switch (txt[index])
            {
                case '\u00a0':   // NO-BREAK SPACE
                case '\u202f':    // NARROW NO-BREAK SPACE
                    return false;
            }

            // Return true for all the NON-whitespace characters that SHOULD be wrappable
            switch (txt[index])
            {
                case '\u200b':   // ZERO WIDTH SPACE
                    return true;
            }

            // Apart from the above exceptions, wrap at whitespace characters.
            return char.IsWhiteSpace(txt, index);
        }

        public static int IndexOf(this IEnumerable source, Func<object, bool> predicate)
        {
            var i = 0;
            foreach (var obj in source)
            {
                if (predicate(obj))
                    return i;
                i++;
            }
            return -1;
        }
    }
}
