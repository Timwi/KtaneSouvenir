using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

using Rnd = UnityEngine.Random;

namespace Souvenir
{
    public static class Ut
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
                throw new InvalidOperationException($"Field is of type {field.FieldType.FullName}, but was expected to be of type {typeof(T).FullName} (or derived from it).");
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
        public static T Shuffle<T>(this T list) where T : IList
        {
            if (list == null)
                throw new ArgumentNullException("list");
            for (int j = list.Count; j >= 1; j--)
            {
                int item = Rnd.Range(0, j);
                if (item < j - 1)
                    (list[j - 1], list[item]) = (list[item], list[j - 1]);
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
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultVal = default)
        {
            if (dict == null)
                throw new ArgumentNullException("dict");
            if (key == null)
                throw new ArgumentNullException("key", "Null values cannot be used for keys in dictionaries.");
            if (dict.TryGetValue(key, out var value))
                return value;
            else
                return defaultVal;
        }

        public static T[] NewArray<T>(params T[] array) { return array; }
        public static List<T> NewList<T>(params T[] array) { return array.ToList(); }

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
            lastSeparator ??= separator;

            using var enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
                return "";

            // Optimize the case where there is only one element
            var one = enumerator.Current;
            if (!enumerator.MoveNext())
                return prefix + one + suffix;

            // Optimize the case where there are only two elements
            var two = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                // Optimize the (common) case where there is no prefix/suffix; this prevents an array allocation when calling string.Concat()
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

        private static readonly string[] _joshi = "でなければ|について|かしら|くらい|けれど|なのか|ばかり|ながら|ことよ|こそ|こと|さえ|しか|した|たり|だけ|だに|だの|つつ|ても|てよ|でも|とも|から|など|なり|ので|のに|ほど|まで|もの|やら|より|って|で|と|な|に|ね|の|も|は|ば|へ|や|わ|を|か|が|さ|し|ぞ|て".Split('|');
        private static readonly string _punctuation = ".,。、！!？?〉》」』｣)）]】〕〗〙〛}>)❩❫❭❯❱❳❵｝";
        private static readonly (char from, char to)[] _breakableRanges = new (char from, char to)[] {
            ('\u4E00', '\u9FA0'),   // CJK
            ('\u3041','\u30ff'),    // Hiragana + Katakana
        };

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

            void renderSpaces()
            {
                sb.Append(' ', numSpaces);
                x += numSpaces * widthOfASpace;
                actualWidth = Math.Max(actualWidth, x);
                numSpaces = 0;
            }

            void renderPieces()
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
            }

            // The parameter is not used, but it may be useful in future
            string advanceToNextLine(bool newParagraph)
            {
                var line = sb.ToString();
                sb = new StringBuilder();
                x = 0;
                atStartOfLine = true;
                curLine++;
                numSpaces = 0;
                return line;
            }

            var i = 0;
            while (i < text.Length)
            {
                // Check whether we are at the start of a word, and if so, how long the word is.
                int lengthOfWord = 0;
                // Japanese: joshi
                var p = _joshi.IndexOf(j => i + j.Length <= text.Length && text.Substring(i, j.Length) == j);
                if (p != -1)
                    lengthOfWord = _joshi[p].Length;
                else
                {
                    if ((p = _breakableRanges.IndexOf(range => text[i] >= range.from && text[i] <= range.to)) != -1)
                        lengthOfWord = 1;
                    else
                    {
                        while (lengthOfWord + i < text.Length && !isWrappableAfter(text, lengthOfWord + i) && text[lengthOfWord + i] != '\n' && !_breakableRanges.Any(range => text[lengthOfWord + i] >= range.from && text[lengthOfWord + i] <= range.to))
                            lengthOfWord++;
                    }

                    // If the word is followed by a joshi, don’t wrap it
                    if ((p = _joshi.IndexOf(j => i + lengthOfWord + j.Length <= text.Length && text.Substring(i + lengthOfWord, j.Length) == j)) != -1)
                        lengthOfWord += _joshi[p].Length;
                }
                // If the word is followed by a punctuation mark, don’t wrap it
                while (lengthOfWord + i < text.Length && _punctuation.Contains(text[lengthOfWord + i]))
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
                }

                // We encounter the end of a word. All the word pieces fit on the current line, so render them.
                if (wordPieces.Count > 0)
                {
                    renderPieces();
                    atStartOfLine = false;
                }

                if (i < text.Length && text[i] == '\n')
                {
                    // If the whitespace character is actually a newline, start a new paragraph.
                    yield return advanceToNextLine(true);
                    i++;
                }
                else if (i < text.Length && char.IsWhiteSpace(text, i))
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

        private static bool isWrappableAfter(string txt, int index) => txt[index] switch
        {
            // Return false for all the whitespace characters that should NOT be wrappable
            // NO-BREAK SPACE and NARROW NO-BREAK SPACE
            '\u00a0' or '\u202f' => false,
            // Return true for all the NON-whitespace characters that SHOULD be wrappable
            // ZERO WIDTH SPACE
            '\u200b' => true,
            // Apart from the above exceptions, wrap at whitespace characters.
            _ => char.IsWhiteSpace(txt, index),
        };

        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
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

        public static T[] NewArray<T>(int length, Func<int, T> fnc)
        {
            var arr = new T[length];
            for (int i = 0; i < length; i++)
                arr[i] = fnc(i);
            return arr;
        }

        /// <summary>Allows the deconstruction of KeyValuePairs into separate variables.</summary>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> source, out TKey key, out TValue value)
        {
            key = source.Key;
            value = source.Value;
        }

        static Ut()
        {
            Attributes = typeof(Question).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => Ut.KeyValuePair((Question) f.GetValue(null), GetQuestionAttribute(f)))
                .Where(kvp => kvp.Value != null)
                .ToDictionary();
        }

        private static SouvenirQuestionAttribute GetQuestionAttribute(FieldInfo field)
        {
            var attribute = field.GetCustomAttribute<SouvenirQuestionAttribute>();
            if (attribute != null)
                attribute.AnswerGenerator = field.GetCustomAttribute<AnswerGeneratorAttribute>();
            return attribute;
        }

        public static Dictionary<Question, SouvenirQuestionAttribute> Attributes;
        public static bool TryGetAttribute(this Question question, out SouvenirQuestionAttribute attr) => Attributes.TryGetValue(question, out attr);
        public static SouvenirQuestionAttribute GetAttribute(this Question question) => Attributes[question];

        public static string Stringify(this object value)
        {
            return value switch
            {
                null => "null",
                IList list => $"[{list.Cast<object>().Select(Stringify).JoinString(", ")}]",
                int i => i.ToString(),
                double d => d.ToString(),
                float f => f.ToString(),
                bool b => b ? "true" : "false",
                string s => $"“{s}”",
                _ => $"{{{value.GetType().FullName}|{value}}}"
            };
        }

        public static void UpdateChildren(this KMSelectable selectable, int childRowLength, params KMSelectable[] children)
        {
            selectable.Children = children;
            selectable.ChildRowLength = childRowLength;
            selectable.UpdateChildrenProperly();
        }

        private static Type ModSelectableType;
        private static MethodInfo CopySettingsFromProxyMethod;
        public static void UpdateChildrenProperly(this KMSelectable selectable)
        {
            if (selectable == null)
                return;
            if (Application.isEditor)
            {
                selectable.UpdateChildren();
                return;
            }
            if (ModSelectableType == null)
                InitializeUpdateSettings();
            selectable.UpdateSettings();
            selectable.UpdateChildren();
        }
        private static void UpdateSettings(this KMSelectable selectable)
        {
            if (selectable != null && CopySettingsFromProxyMethod != null)
                CopySettingsFromProxyMethod.Invoke(
                    selectable.GetComponent(ModSelectableType) ?? selectable.gameObject.AddComponent(ModSelectableType),
                    new object[0]);
        }
        private static void InitializeUpdateSettings()
        {
            static IEnumerable<Type> GetSafeTypes(Assembly assembly)
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    return e.Types.Where(x => x != null);
                }
                catch (Exception)
                {
                    return new List<Type>();
                }
            }
            ModSelectableType = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => GetSafeTypes(a))
                .FirstOrDefault(t => t.FullName != null
                    && t.FullName.Equals("ModSelectable")
                    && t.Assembly.GetName().Name.Equals("Assembly-CSharp"));
            if (ModSelectableType != null)
                CopySettingsFromProxyMethod =
                    ModSelectableType.GetMethod("CopySettingsFromProxy", BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Creates a sequence of random elements chosen from <paramref name="collection"/> without repetition.
        /// </summary>
        public static IEnumerable<T> OrderRandomly<T>(this IList<T> collection)
        {
            var available = Enumerable.Range(0, collection.Count).ToList();
            while (collection.Count > 0)
            {
                int choice = available.PickRandom();
                yield return collection[choice];
                available.Remove(choice);
            }
        }
    }
}
