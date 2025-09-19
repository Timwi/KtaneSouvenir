using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

using Rnd = UnityEngine.Random;

namespace Souvenir;

public static class Ut
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
        return !typeof(T).IsAssignableFrom(field.FieldType)
            ? throw new InvalidOperationException($"Field is of type {field.FieldType.FullName}, but was expected to be of type {typeof(T).FullName} (or derived from it).")
            : (T) field.GetValue(instance);
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
        for (var j = list.Count; j >= 1; j--)
        {
            var item = Rnd.Range(0, j);
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
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer = null) => source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value, comparer ?? EqualityComparer<TKey>.Default);

    public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member, bool inherit = false) where TAttribute : class
    {
        var attrs = member.GetCustomAttributes(typeof(TAttribute), inherit);
        return attrs.Length == 0 ? null : (TAttribute) attrs[0];
    }

    /// <summary>Allows the use of type inference when creating .NET’s KeyValuePair&lt;TK,TV&gt;.</summary>
    public static KeyValuePair<TKey, TValue> KeyValuePair<TKey, TValue>(TKey key, TValue value) => new(key, value);

    /// <summary>
    ///     Adds an element to a List&lt;V&gt; stored in the current IDictionary&lt;K, List&lt;V&gt;&gt;. If the specified key
    ///     does not exist in the current IDictionary, a new List is created.</summary>
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
            dic[key] = [];
        dic[key].Add(value);
    }

    /// <summary>
    ///     Adds an element to a two-level Dictionary&lt;,&gt;. If the specified key does not exist in the outer Dictionary, a
    ///     new Dictionary is created.</summary>
    /// <typeparam name="K1">
    ///     Type of the key of the outer Dictionary.</typeparam>
    /// <typeparam name="K2">
    ///     Type of the key of the inner Dictionary.</typeparam>
    /// <typeparam name="V">
    ///     Type of the values in the inner Dictionary.</typeparam>
    /// <param name="dic">
    ///     Dictionary to operate on.</param>
    /// <param name="key1">
    ///     Key at which the inner Dictionary is located in the outer Dictionary.</param>
    /// <param name="key2">
    ///     Key at which the value is located in the inner Dictionary.</param>
    /// <param name="value">
    ///     Value to add to the inner Dictionary.</param>
    /// <param name="comparer">
    ///     Optional equality comparer to pass into the inner dictionary if a new one is created.</param>
    public static void AddSafe<K1, K2, V>(this IDictionary<K1, Dictionary<K2, V>> dic, K1 key1, K2 key2, V value, IEqualityComparer<K2> comparer = null)
    {
        if (dic == null)
            throw new ArgumentNullException(nameof(dic));
        if (key1 == null)
            throw new ArgumentNullException(nameof(key1), "Null values cannot be used for keys in dictionaries.");
        if (key2 == null)
            throw new ArgumentNullException(nameof(key2), "Null values cannot be used for keys in dictionaries.");
        if (!dic.ContainsKey(key1))
            dic[key1] = new Dictionary<K2, V>(comparer);
        dic[key1].Add(key2, value);
    }

    /// <summary>
    ///     Increments an integer in an <see cref="IDictionary&lt;K, V&gt;"/> by the specified amount. If the specified key
    ///     does not exist in the current dictionary, the value <paramref name="amount"/> is inserted.</summary>
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
    public static int IncSafe<K>(this IDictionary<K, int> dic, K key, int amount = 1) =>
        dic == null ? throw new ArgumentNullException("dic") :
        key == null ? throw new ArgumentNullException("key", "Null values cannot be used for keys in dictionaries.") :
        !dic.ContainsKey(key) ? (dic[key] = amount) : (dic[key] = dic[key] + amount);

    /// <summary>
    ///     Gets a value from a dictionary by key. If the key does not exist in the dictionary, the default value is returned
    ///     instead.</summary>
    /// <param name="dict">
    ///     Dictionary to operate on.</param>
    /// <param name="key">
    ///     Key to look up.</param>
    /// <param name="defaultVal">
    ///     Value to return if key is not contained in the dictionary.</param>
    public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultVal = default) =>
        dict == null ? throw new ArgumentNullException("dict") :
        key == null ? throw new ArgumentNullException("key", "Null values cannot be used for keys in dictionaries.") :
        dict.TryGetValue(key, out var value) ? value : defaultVal;

    public static T[] NewArray<T>(params T[] array) => array;
    public static List<T> NewList<T>(params T[] array) => array.ToList();

    /// <summary>
    ///     Turns all elements in the enumerable to strings and joins them using the specified <paramref name="separator"/>
    ///     and the specified <paramref name="prefix"/> and <paramref name="suffix"/> for each string.</summary>
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
            return prefix == null && suffix == null ? one + lastSeparator + two : prefix + one + suffix + lastSeparator + prefix + two + suffix;
        }

        var sb = new StringBuilder()
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
        return arr.Count == 0
            ? throw new InvalidOperationException("Cannot pick a random element from an empty set.")
            : arr[Rnd.Range(0, arr.Count)];
    }

    /// <summary>
    ///     Returns the set of enum values from the specified enum type.</summary>
    /// <typeparam name="T">
    ///     The enum type from which to retrieve the values.</typeparam>
    /// <returns>
    ///     A strongly-typed array containing the enum values from the specified type.</returns>
    public static T[] GetEnumValues<T>() => (T[]) Enum.GetValues(typeof(T));

    private static readonly string[] _joshi = "でなければ|について|かしら|くらい|けれど|なのか|ばかり|ながら|ことよ|こそ|こと|さえ|しか|した|たり|だけ|だに|だの|つつ|ても|てよ|でも|とも|から|など|なり|ので|のに|ほど|まで|もの|やら|より|って|で|と|な|に|ね|の|も|は|ば|へ|や|わ|を|か|が|さ|し|ぞ|て".Split('|');
    private static readonly string _punctuation = ".,。、！!？?〉》」』｣)）]】〕〗〙〛}>)❩❫❭❯❱❳❵｝";
    private static readonly (char from, char to)[] _breakableRanges = [
        ('\u4E00', '\u9FA0'),   // CJK
        ('\u3041','\u30ff'),    // Hiragana + Katakana
    ];

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
            for (var j = 0; j < wordPieces.Count; j++)
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
            var lengthOfWord = 0;
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
                var fragment = text.Substring(i, lengthOfWord);
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
        for (var i = 0; i < length; i++)
            arr[i] = fnc(i);
        return arr;
    }

    /// <summary>Allows the deconstruction of KeyValuePairs into separate variables.</summary>
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> source, out TKey key, out TValue value)
    {
        key = source.Key;
        value = source.Value;
    }

    public static Dictionary<string, SouvenirHandlerAttribute> ModuleHandlers = [];
    public static Dictionary<Enum, (SouvenirHandlerAttribute h, SouvenirQuestionAttribute q, SouvenirDiscriminatorAttribute d)> Attributes = new(EnumEqualityComparer.Default);
    public static (SouvenirHandlerAttribute h, SouvenirQuestionAttribute q, SouvenirDiscriminatorAttribute d) GetAttributes(this Enum value) => Attributes.Get(value, default);
    public static SouvenirHandlerAttribute GetHandlerAttribute(this Enum questionOrDiscriminator) => GetAttributes(questionOrDiscriminator).h;
    public static SouvenirQuestionAttribute GetQuestionAttribute(this Enum question) => GetAttributes(question).q;
    public static SouvenirDiscriminatorAttribute GetDiscriminatorAttribute(this Enum discriminator) => GetAttributes(discriminator).d;

    private static Dictionary<Type, SouvenirHandlerAttribute> _handlerAttributes = [];
    public static SouvenirHandlerAttribute GetHandlerAttribute(this Type moduleEnumType) => _handlerAttributes.Get(moduleEnumType);

    static Ut()
    {
        foreach (var method in typeof(SouvenirModule).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
            if (method.GetCustomAttribute<SouvenirHandlerAttribute>() is { } hAttr)
            {
                hAttr.Method = method;
                ModuleHandlers[hAttr.ModuleId] = hAttr;
                _handlerAttributes[hAttr.EnumType] = hAttr;
                foreach (var field in hAttr.EnumType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    if (field.GetCustomAttribute<SouvenirQuestionAttribute>() is { } qAttr)
                    {
                        if (field.GetCustomAttributes(typeof(AnswerGeneratorAttribute), false) is AnswerGeneratorAttribute[] agAttrs)
                            qAttr.AnswerGenerators = agAttrs.Length == 0 ? null : agAttrs;
                        qAttr.EnumValue = (Enum) field.GetValue(null);
                        qAttr.Handler = hAttr;
                        Attributes.Add((Enum) field.GetValue(null), (hAttr, qAttr, null));
                    }
                    else if (field.GetCustomAttribute<SouvenirDiscriminatorAttribute>() is { } dAttr)
                    {
                        dAttr.EnumValue = (Enum) field.GetValue(null);
                        dAttr.Handler = hAttr;
                        Attributes.Add((Enum) field.GetValue(null), (hAttr, null, dAttr));
                    }
                }
            }
    }

    public static string Stringify(this object value) => value switch
    {
        null => "null",
        ICollection list => $"[{list.Cast<object>().Select(Stringify).JoinString(", ")}]",
        int i => i.ToString(),
        double d => d.ToString(),
        float f => f.ToString(),
        bool b => b ? "true" : "false",
        string s => $"“{s}”",
        Sprite spr => $"Sprite ({spr.name})",
        object o when o.GetType().IsGenericType && o.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>) => $"[{o.GetFieldValue<object>("key").Stringify()}] = {o.GetFieldValue<object>("value").Stringify()}",
        _ => $"{{{value.GetType().FullName}|{value}}}"
    };

    public static void UpdateChildren(this KMSelectable selectable, int childRowLength, params KMSelectable[] children)
    {
        selectable.Children = children;
        selectable.ChildRowLength = childRowLength;
        selectable.UpdateChildrenProperly();
    }

    private static Type _modSelectableType;
    private static MethodInfo _copySettingsFromProxyMethod;
    public static void UpdateChildrenProperly(this KMSelectable selectable)
    {
        if (selectable == null)
            return;
        if (Application.isEditor)
        {
            selectable.UpdateChildren();
            return;
        }
        if (_modSelectableType == null)
            InitializeUpdateSettings();
        selectable.UpdateSettings();
        selectable.UpdateChildren();
    }
    private static void UpdateSettings(this KMSelectable selectable)
    {
        if (selectable != null && _copySettingsFromProxyMethod != null)
            _copySettingsFromProxyMethod.Invoke(
                selectable.GetComponent(_modSelectableType) ?? selectable.gameObject.AddComponent(_modSelectableType),
                []);
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
        _modSelectableType = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(GetSafeTypes)
            .FirstOrDefault(t => t.FullName != null
                && t.FullName.Equals("ModSelectable")
                && t.Assembly.GetName().Name.Equals("Assembly-CSharp"));
        if (_modSelectableType != null)
            _copySettingsFromProxyMethod =
                _modSelectableType.GetMethod("CopySettingsFromProxy", BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary>Creates a sequence of random elements chosen from <paramref name="collection"/> without repetition.</summary>
    public static IEnumerable<T> OrderRandomly<T>(this IList<T> collection)
    {
        var used = new List<int>(); // Remains sorted (ascending)
        while (collection.Count > used.Count)
        {
            var choice = Rnd.Range(0, collection.Count - used.Count);
            var lastGreaterThan = 0;
            for (var i = 0; i < used.Count && choice < used[i]; i++)
            {
                choice++;
                lastGreaterThan = i;
            }
            used.Insert(lastGreaterThan, choice);
            yield return collection[choice];
        }
    }

    public static string[] GetAnswers(this Enum question) => GetQuestionAttribute(question) is not { } attr
        ? throw new InvalidOperationException($"Question {question.GetType().Name}.{question} is missing from the Attributes dictionary.")
        : attr.AllAnswers;

    public static string[] GetExampleAnswers(this Enum question) => GetQuestionAttribute(question) is not { } attr
        ? throw new InvalidOperationException($"Question {question.GetType().Name}.{question} is missing from the Attributes dictionary.")
        : attr.ExampleAnswers;

    public static Sprite[] GetAllSprites(this Enum question, SouvenirModule souv)
    {
        var attr = question.GetQuestionAttribute();
        return attr.Type != AnswerType.Sprites
            ? throw new AbandonModuleException("GetAllSprites() was called on a question that doesn’t use sprites or doesn’t have an associated sprites field.")
            : attr.SpriteFieldName == null ? null : (Sprite[]) attr.SpriteField.GetValue(souv);
    }

    public static AudioClip[] GetAllSounds(this Enum question, SouvenirModule souv)
    {
        var attr = question.GetQuestionAttribute();
        return attr.Type != AnswerType.Audio || attr.AudioFieldName == null
            ? throw new AbandonModuleException("GetAllSounds() was called on a question that doesn’t use sounds or doesn’t have an associated sounds field.")
            : (AudioClip[]) attr.AudioField.GetValue(souv);
    }

    public static IEnumerable<T> GetAnswers<T>(this IEnumerable<AnswerGeneratorAttribute<T>> generators, SouvenirModule souv) =>
        generators.ToArray() is var arr ? arr.Length is 1 ? arr[0].GetAnswers(souv) : arr.UnionAnswers(souv) : null;
    private static IEnumerable<T> UnionAnswers<T>(this IEnumerable<AnswerGeneratorAttribute<T>> generators, SouvenirModule souv)
    {
        var iterators = generators.Select(g => g.GetAnswers(souv).GetEnumerator()).ToList();
        var weights = generators.Select(g => g.Count).ToList();
        var totalWeight = weights.Sum();
        while (iterators.Count > 0)
        {
            var c = Rnd.Range(0, totalWeight);
            var i = -1;
            while (c >= 0)
                c -= weights[++i];
            if (iterators[i].MoveNext())
                yield return iterators[i].Current;
            else
            {
                iterators.RemoveAt(i);
                totalWeight -= weights[i];
                weights.RemoveAt(i);
            }
        }
    }

    /// <summary>Generates a hexdump of the UTF-8 encoding of the string representation of the specified object.</summary>
    public static string DebugExamine(this object input)
    {
        var inf = $"{input} ({input.GetHashCode()}";
        if (input is string s)
        {
            var utf8 = Encoding.UTF8.GetBytes(input.ToString());

            var charArr = new char[utf8.Length * 2];
            var j = 0;
            for (var i = 0; i < utf8.Length; i++)
            {
                var b = (byte) (utf8[i] >> 4);
                charArr[j] = (char) (b < 10 ? '0' + b : 'W' + b);   // 'a'-10 = 'W'
                j++;
                b = (byte) (utf8[i] & 0xf);
                charArr[j] = (char) (b < 10 ? '0' + b : 'W' + b);
                j++;
            }
            inf += $"; str: {s.GetHashCode()}, {new string(charArr)}";
        }
        return inf + ")";
    }

    /// <summary>
    ///     Returns the first element of a sequence, or <c>null</c> if the sequence contains no elements.</summary>
    /// <typeparam name="T">
    ///     The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">
    ///     The <see cref="IEnumerable&lt;T&gt;"/> to return the first element of.</param>
    /// <returns>
    ///     <c>null</c> if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.</returns>
    public static T? FirstOrNull<T>(this IEnumerable<T> source) where T : struct
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        using var e = source.GetEnumerator();
        return e.MoveNext() ? e.Current : null;
    }

    /// <summary>
    ///     Returns the first element of a sequence that satisfies a given predicate, or <c>null</c> if the sequence contains
    ///     no elements.</summary>
    /// <typeparam name="T">
    ///     The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">
    ///     The <see cref="IEnumerable&lt;T&gt;"/> to return the first element of.</param>
    /// <param name="predicate">
    ///     Only consider elements that satisfy this predicate.</param>
    /// <returns>
    ///     <c>null</c> if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.</returns>
    public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : struct
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
        using var e = source.GetEnumerator();
        while (e.MoveNext())
            if (predicate(e.Current))
                return e.Current;
        return null;
    }

    /// <summary>
    ///     Adds an entry to the dictionary if one isn't already there.</summary>
    /// <returns>
    ///     <see langword="true"/> if something was added, otherwise <see langword="false">.</returns>
    public static bool TryAdd<T, U>(this Dictionary<T, U> dict, T key, U value)
    {
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, value);
            return true;
        }
        return false;
    }

    /// <summary>
    ///     Determines whether the <paramref name="input"/> string matches the specified regular expression <paramref
    ///     name="pattern"/>.</summary>
    /// <param name="input">
    ///     The string to search for a match.</param>
    /// <param name="pattern">
    ///     The regular expression pattern to match.</param>
    /// <param name="match">
    ///     Receives an object that contains information about the match.</param>
    /// <returns>
    ///     A boolean indicating whether a match was found or not.</returns>
    public static bool RegexMatch(this string input, string pattern, out Match match)
    {
        match = Regex.Match(input, pattern);
        return match.Success;
    }

    /// <summary>
    ///     Determines whether the <paramref name="input"/> string matches the specified regular expression <paramref
    ///     name="pattern"/>.</summary>
    /// <param name="input">
    ///     The string to search for a match.</param>
    /// <param name="pattern">
    ///     The regular expression pattern to match.</param>
    /// <param name="options">
    ///     A bitwise combination of the enumeration values that provide options for matching.</param>
    /// <param name="match">
    ///     Receives an object that contains information about the match.</param>
    /// <returns>
    ///     A boolean indicating whether a match was found or not.</returns>
    public static bool RegexMatch(this string input, string pattern, RegexOptions options, out Match match)
    {
        match = Regex.Match(input, pattern, options);
        return match.Success;
    }
}
