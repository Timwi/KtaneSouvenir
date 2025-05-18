using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Souvenir
{
    /// <summary>
    ///     The base class for answer generators.</summary>
    /// <remarks>
    ///     <para>
    ///         Answer generators provide a means to easily specify common wrong answer patterns on Souvenir questions without
    ///         having to write out the entire list.</para>
    ///     <para>
    ///         To use an answer generator, add an attribute which is a subclass of <see cref="AnswerGeneratorAttribute"/> to
    ///         the <see cref="Question"/> enum value. <see cref="SouvenirQuestionAttribute.AllAnswers"/> should be null when
    ///         using an answer generator. When the question is generated, any answer generator will be used to effectively
    ///         append as many randomly selected answers as needed to <see cref="SouvenirQuestionAttribute.AllAnswers"/>.
    ///         Preferred wrong answers may still be specified in addition to an answer generator, or instead of one as was
    ///         done previously.</para>
    ///     <para>
    ///         Answer generator implementations are provided as nested classes in the <see cref="AnswerGenerator"/> class.
    ///         See the documentation for these classes for more details.</para></remarks>
    /// <example>
    ///     <code>
    ///         [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
    ///         [AnswerGenerator.Integers(0, 99, "00")]
    ///         TwoBitsResponse</code></example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public abstract class AnswerGeneratorAttribute : Attribute
    {
        public abstract Type ElementType { get; }
        public abstract int Count { get; }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public abstract class AnswerGeneratorAttribute<T> : AnswerGeneratorAttribute
    {
        public abstract IEnumerable<T> GetAnswers(SouvenirModule module);
        public sealed override Type ElementType => typeof(T);
    }

    public static class AnswerGenerator
    {
        /// <summary>
        ///     An answer generator that generates numeric answers within a specified range.</summary>
        /// <remarks>
        ///     <para>
        ///         This generator generates string representations of numbers within a range specified by inclusive bounds
        ///         and a step size. The numbers may optionally be formatted using a format string. For more details, see
        ///         https://docs.microsoft.com/en-us/dotnet/api/system.int32.tostring?view=netframework-3.5.</para>
        ///     <para>
        ///         If a step size is specified, the generated numbers will be multiples of the step size plus the minimum.
        ///         The maximum may not be included if the step size is greater than 1.</para></remarks>
        /// <example>
        ///     <code>
        ///         [AnswerGenerator.Integers(1, 12)]  // Generates integers between 1 and 12 inclusive.
        ///         [AnswerGenerator.Integers(0, 50, 5)]  // Generates multiples of 5 between 0 and 50 inclusive.
        ///         [AnswerGenerator.Integers(0, 999, "000")]  // Generates integers between 0 and 999 and adds leading zeros.</code></example>
        public class Integers : AnswerGeneratorAttribute<string>
        {
            public int Min { get; private set; }
            public int MaxSteps { get; private set; }
            public int Step { get; private set; }
            public string Format { get; private set; }

            /// <param name="min">
            ///     The inclusive lower bound for generated numbers.</param>
            /// <param name="max">
            ///     The inclusive upper bound for generated numbers.</param>
            public Integers(int min, int max) : this(min, max, 1, null) { }
            /// <param name="min">
            ///     The inclusive lower bound for generated numbers.</param>
            /// <param name="max">
            ///     The inclusive upper bound for generated numbers.</param>
            /// <param name="format">
            ///     A format string used to format generated numbers.</param>
            public Integers(int min, int max, string format) : this(min, max, 1, format) { }
            /// <param name="min">
            ///     The inclusive lower bound for generated numbers.</param>
            /// <param name="max">
            ///     The inclusive upper bound for generated numbers.</param>
            /// <param name="step">
            ///     The step size to use for generated numbers.</param>
            public Integers(int min, int max, int step) : this(min, max, step, null) { }
            /// <param name="min">
            ///     The inclusive lower bound for generated numbers.</param>
            /// <param name="max">
            ///     The inclusive upper bound for generated numbers.</param>
            /// <param name="step">
            ///     The step size to use for generated numbers.</param>
            /// <param name="format">
            ///     A format string used to format generated numbers.</param>
            public Integers(int min, int max, int step, string format)
            {
                if (step <= 0) throw new ArgumentOutOfRangeException("step", "step must be positive.");
                if (max < min) throw new ArgumentOutOfRangeException("max", "max must be greater than min.");
                Min = min;
                MaxSteps = (max + 1 - min) / step;
                Step = step;
                Format = format;
            }

            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                if (MaxSteps >= 10)
                    while (true)
                        yield return (Random.Range(0, MaxSteps) * Step + Min).ToString(Format);

                // With no more than 6 possible values, the above case may go into an infinite loop trying to generate 5 distinct values.
                // In this case, we will return all possible values in a random order and then halt.
                var values = new int[MaxSteps];
                for (int i = MaxSteps - 1; i >= 0; i--)
                    values[i] = i * Step + Min;
                values.Shuffle();
                foreach (var i in values)
                    yield return i.ToString(Format);
            }

            public override int Count => MaxSteps;
        }

        /// <summary>
        ///     An answer generator that generates answers consisting of randomly selected characters.</summary>
        /// <remarks>
        ///     <para>
        ///         This generator builds answers using a specified number of random characters from one or more lists. Each
        ///         list specifies a combination of individual characters and ranges, and chooses a specified number of
        ///         characters from that list, with replacement.</para></remarks>
        /// <example>
        ///     <code>
        ///         [AnswerGenerator.Strings('A', 'Z')]  // Generates single uppercase letters.
        ///         [AnswerGenerator.Strings("A-Z-")]  // Generates answers with either a single uppercase letter or a hyphen.
        ///         [AnswerGenerator.Strings("A-F", "1-6")]  // Generates answers consisting of a letter A~F followed by a digit 1~6.
        ///         [AnswerGenerator.Strings("2*0-9A-Z")]  // Generates answers consisting of two alphanumeric characters.</code></example>
        public class Strings : AnswerGeneratorAttribute<string>
        {
            public struct CharacterList
            {
                public int Count;
                public char[] Chars;
                public char[] Ranges;

                public CharacterList(int count, char[] chars, char[] ranges)
                {
                    if (count <= 0) throw new ArgumentOutOfRangeException("count");
                    if ((chars == null || chars.Length == 0) && (ranges == null || ranges.Length == 0))
                        throw new ArgumentException("chars", "Either chars or ranges must be non-empty.");
                    if (ranges != null && ranges.Length % 2 != 0) throw new ArgumentOutOfRangeException("ranges", "Non-null ranges array must have an even length.");
                    Count = count;
                    Chars = chars;
                    Ranges = ranges;
                }

                public readonly char Pick()
                {
                    var n = Random.Range(0, OptionCount);
                    if (Chars != null)
                    {
                        if (n < Chars.Length) return Chars[n];
                        n -= Chars.Length;
                    }
                    if (Ranges != null)
                    {
                        for (int i = 0; i < Ranges.Length; i += 2)
                        {
                            if (n < Ranges[i + 1] + 1 - Ranges[i]) return (char) (Ranges[i] + n);
                            n -= Ranges[i + 1] + 1 - Ranges[i];
                        }
                        // We shouldn't reach here.
                        return Ranges[0];
                    }
                    // We shouldn't reach here either.
                    return Chars[0];
                }

                public static CharacterList Parse(string expression)
                {
                    var chars = new List<char>();
                    var ranges = new List<char>();

                    int i = expression.IndexOf('*');
                    if (i > 0 && int.TryParse(expression.Substring(0, i), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out int count))
                    {
                        if (count == 0) throw new FormatException("Count in a character list expression must be positive.");
                        i++;
                    }
                    else
                    {
                        i = 0;
                        count = 1;
                    }
                    if (i >= expression.Length) throw new FormatException("A character list expression must contain characters.");
                    while (i < expression.Length)
                    {
                        if (i + 2 < expression.Length && expression[i + 1] == '-')
                        {
                            ranges.Add(expression[i]);
                            ranges.Add(expression[i + 2]);
                            i += 3;
                        }
                        else
                        {
                            chars.Add(expression[i]);
                            i++;
                        }
                    }
                    return new CharacterList(count, chars.Count > 0 ? chars.ToArray() : null, ranges.Count > 0 ? ranges.ToArray() : null);
                }

                public readonly int OptionCount
                {
                    get
                    {
                        var n = Chars != null ? Chars.Length : 0;
                        if (Ranges != null)
                        {
                            for (int i = 0; i < Ranges.Length; i += 2) n += Ranges[i + 1] + 1 - Ranges[i];
                        }
                        return n;
                    }
                }
            }

            private readonly CharacterList[] characterLists;

            /// <param name="characterLists">
            ///     A list of expressions specifying character lists.</param>
            /// <remarks>
            ///     <para>
            ///         A character list expression specifies a list of characters and a number of characters to choose from
            ///         that list. A character followed by a hyphen ('-') and another character specifies a range, including
            ///         all characters between the bounds inclusively. Other characters are directly included in the list. A
            ///         hyphen may be included in the list by placing it at the start or end of the string, or immediately
            ///         after a range.</para>
            ///     <para>
            ///         The expression may be prefixed with a positive integer followed by a '*'. This specifies the number of
            ///         characters to choose from the list. If no count is specified, one character is chosen.</para></remarks>
            public Strings(params string[] characterLists)
            {
                this.characterLists = new CharacterList[characterLists.Length];
                for (int i = characterLists.Length - 1; i >= 0; i--)
                    this.characterLists[i] = CharacterList.Parse(characterLists[i]);
            }
            /// <param name="count">
            ///     The number of characters to choose.</param>
            /// <param name="chars">
            ///     The list of characters to choose from.</param>
            public Strings(int count, params char[] chars) : this(new CharacterList(count, chars, null)) { }
            /// <param name="count">
            ///     The number of characters to choose.</param>
            /// <param name="chars">
            ///     The list of characters to choose from.</param>
            public Strings(int count, string chars) : this(new CharacterList(count, chars.ToCharArray(), null)) { }
            /// <param name="first">
            ///     The inclusive lower bound of the range of characters to choose from.</param>
            /// <param name="last">
            ///     The inclusive upper bound of the range of characters to choose from.</param>
            public Strings(char first, char last) : this(new CharacterList(1, null, new[] { first, last })) { }
            /// <param name="count">
            ///     The number of characters to choose.</param>
            /// <param name="first">
            ///     The inclusive lower bound of the range of characters to choose from.</param>
            /// <param name="last">
            ///     The inclusive upper bound of the range of characters to choose from.</param>
            public Strings(int count, char first, char last) : this(new CharacterList(count, null, new[] { first, last })) { }
            // This constructor cannot be used in an attribute.
            private Strings(params CharacterList[] characterLists)
            {
                if (characterLists == null || characterLists.Length == 0) throw new ArgumentException("characterLists");
                this.characterLists = characterLists;
            }

            public static Strings FromCharacterLists(params CharacterList[] characterLists)
            {
                return new Strings(characterLists);
            }

            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                var builder = new StringBuilder();
                while (true)
                {
                    foreach (var characterList in characterLists)
                        for (int i = characterList.Count - 1; i >= 0; i--)
                            builder.Append(characterList.Pick());
                    yield return builder.ToString();
                    builder.Length = 0;
                }
            }

            public override int Count => characterLists.Select(c => (int) Math.Pow(c.OptionCount, c.Count)).Aggregate((a, b) => a * b);
        }

        /// <summary>An answer generator that generates answers consisting of randomly selected grid cells.</summary>
        public class Grid : AnswerGeneratorAttribute<Sprite>
        {
            private readonly int _width;
            private readonly int _height;

            public override int Count => _width * _height;

            public Grid(int width, int height)
            {
                _width = width;
                _height = height;
            }

            public override IEnumerable<Sprite> GetAnswers(SouvenirModule module)
            {
                var count = _width * _height;
                if (count >= 10)
                    while (true)
                        yield return Sprites.GenerateGridSprite(_width, _height, Random.Range(0, count));

                var positions = Enumerable.Range(0, count).ToArray().Shuffle();
                foreach (var position in positions)
                    yield return Sprites.GenerateGridSprite(_width, _height, position);
            }
        }

        public class Circles : AnswerGeneratorAttribute<Sprite>
        {
            private readonly int _width;
            private readonly int _height;
            private readonly int _radius;
            private readonly int _gap;

            /// <summary>Indicates that the pattern in which all circles are “off” should not be generated.</summary>
            public bool SuppressEmpty { get; set; }

            /// <summary>
            ///     Indicates whether “off” circles should be represented by a circle outline (as opposed to be missing
            ///     entirely).</summary>
            public bool DrawOutline { get; set; }

            /// <summary>
            ///     Generates sprites in which circles are arranged in a rectilinear grid.</summary>
            /// <param name="width">
            ///     Specifies the number of circles per row.</param>
            /// <param name="height">
            ///     Specifies the number of circles per column.</param>
            /// <param name="radius">
            ///     Specifies the radius of each circle, in pixels.</param>
            /// <param name="gap">
            ///     Specifies the gap between circles, in pixels.</param>
            public Circles(int width, int height, int radius, int gap)
            {
                _width = width;
                _height = height;
                _radius = radius;
                _gap = gap;
            }

            public override IEnumerable<Sprite> GetAnswers(SouvenirModule module)
            {
                var maxDots = _width * _height;
                if (maxDots >= 10)
                    while (true)
                        yield return Sprites.GenerateCirclesSprite(_width, _height, Random.Range(SuppressEmpty ? 1 : 0, 1 << maxDots), _radius, _gap, DrawOutline);

                // With no more than 6 possible values, the above case may go into an infinite loop trying to generate 5 distinct values.
                // In this case, we will return all possible values in a random order and then halt.
                var dotPatterns = Enumerable.Range(SuppressEmpty ? 1 : 0, (1 << maxDots) - (SuppressEmpty ? 1 : 0)).ToArray().Shuffle();
                foreach (var dotPattern in dotPatterns)
                    yield return Sprites.GenerateCirclesSprite(_width, _height, dotPattern, _radius, _gap, DrawOutline);
            }

            public override int Count => _width * _height;
        }

        /// <summary>
        ///     An answer generator that generates answers based on ordinal.</summary>
        /// <example>
        ///     <code>
        ///         [AnswerGenerator.Ordinal(3)] //Generate the ordinals for "first", "second", and "third" in the target language
        ///         [AnswerGenerator.Ordinal(1, 50, 10)] //Generates ordinals that are multiples of 10 between 1 and 50 inclusive in the target language.
        ///         [AnswerGenerator.Ordinal(2, 5)] //Generates ordinals "second", "third", "fourth", and "fifth" in the target language</code></example>
        public class Ordinal : AnswerGeneratorAttribute<string>
        {
            public int Min { get; private set; }
            public int MaxSteps { get; private set; }
            public int Step { get; private set; }

            /// <param name="max">
            ///     The inclusive upper bound for generated ordinal.</param>
            public Ordinal(int max) : this(1, max, 1) { }
            /// <param name="min">
            ///     The inclusive lower bound for generated ordinal.</param>
            /// <param name="max">
            ///     The inclusive upper bound for generated ordinal.</param>
            public Ordinal(int min, int max) : this(min, max, 1) { }
            /// <param name="min">
            ///     The inclusive lower bound for generated ordinal.</param>
            /// <param name="max">
            ///     The inclusive upper bound for generated ordinal.</param>
            /// <param name="step">
            ///     The step size to use for generated ordinal.</param>
            public Ordinal(int min, int max, int step)
            {
                if (step <= 0) throw new ArgumentOutOfRangeException("step", "step must be positive.");
                if (min <= 0) throw new ArgumentOutOfRangeException("min", "min must be positive");
                if (max < min) throw new ArgumentOutOfRangeException("max", "max must be greater than min.");

                Min = min;
                MaxSteps = (max + 1 - min) / step;
                Step = step;
            }
            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                if (MaxSteps >= 10)
                    while (true)
                        yield return module.Ordinal(Random.Range(0, MaxSteps) * Step + Min);

                // With no more than 6 possible values, the above case may go into an infinite loop trying to generate 5 distinct values.
                // In this case, we will return all possible values in a random order and then halt.
                var values = new int[MaxSteps];
                for (int i = MaxSteps - 1; i >= 0; i--)
                    values[i] = i * Step + Min;
                values.Shuffle();
                foreach (var i in values)
                    yield return module.Ordinal(i);
            }

            public override int Count => MaxSteps;
        }

        /// <summary>
        ///     An answer generator that concatenates the output from other answer generators, i.e. it generates their cross
        ///     product.</summary>
        /// <example>
        ///     <code>
        ///         [AnswerGenerator.Combo(typeof(AnswerGenerator.Strings), new object[] { 'A', 'L' }, typeof(AnswerGenerator.Integers), new object[] { 1, 12 })]
        ///         // Generates grid coordinates from A1 to L12.</code></example>
        /// <remarks>
        ///     This generator might enter an infinite loop if it can't generate enough answers.</remarks>
        public class Concatenate : AnswerGeneratorAttribute<string>
        {
            private readonly AnswerGeneratorAttribute<string>[] _generators;
            // Attribute parameters are unfortunately quite restricted
            public Concatenate(Type g1type, object[] g1args, Type g2type, object[] g2args)
            {
                _generators = new[] {
                    (AnswerGeneratorAttribute<string>) Activator.CreateInstance(g1type, g1args),
                    (AnswerGeneratorAttribute<string>) Activator.CreateInstance(g2type, g2args)
                };
            }

            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                var parts = _generators.Select(g => g.GetAnswers(module).GetEnumerator()).ToArray();
                while (true)
                {
                    StringBuilder sb = new();
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (!parts[i].MoveNext())
                            (parts[i] = _generators[i].GetAnswers(module).GetEnumerator()).MoveNext();
                        sb.Append(parts[i].Current);
                    }
                    yield return sb.ToString();
                }
            }

            public override int Count => _generators.Select(g => g.Count).Aggregate((a, b) => a * b);
        }

        /// <summary>
        /// An answer generator used for <see cref="Question.WumboNumber"/>.
        /// </summary>
        public sealed class Wumbo : AnswerGeneratorAttribute<string>
        {
            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                ulong[] primes = new[] { 2uL, 3uL, 5uL, 7uL, 11uL, 13uL };
                while (true)
                {
                    ulong n = 1;
                    for (int i = 0; i < primes.Length; i++)
                        for (int j = 0, c = Random.Range(0, 5); j < c; j++)
                            n *= primes[i];
                    yield return n.ToString();
                }
            }
            public override int Count => 15625;
        }

        /// <summary>
        /// An answer generator used for <see cref="Question.ChineseRemainderTheoremEquations"/>.
        /// </summary>
        public sealed class ChineseRemainderTheorem : AnswerGeneratorAttribute<string>
        {
            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                while (true)
                {
                    var m = Random.Range(2, 51);
                    yield return $"N % {m} = {Random.Range(0, m)}";
                }
            }
            public override int Count => 1225;
        }

        public sealed class AMessage : AnswerGeneratorAttribute<string>
        {
            const string Message = "\ue910\ue90E\ue91B\ue90F\ue91E\ue902\ue91B\ue90F\ue90C\ue914\ue903\ue912\ue91F\ue911\ue908\ue904\ue909\ue90A\ue902\ue91B\ue913\ue906\ue912\ue91B\ue913\ue914\ue90F\ue902\ue90F\ue90D\ue901\ue91F\ue900\ue916\ue903\ue919\ue90E\ue912\ue91C\ue909\ue91F\ue915\ue915\ue904\ue906\ue90C\ue903\ue915\ue918\ue91B\ue90C\ue90C\ue916\ue908\ue90D\ue91A\ue91F\ue914\ue911\ue913\ue905\ue91D\ue906\ue904\ue91A\ue91A\ue91A\ue906\ue911\ue91B\ue908\ue90E\ue919\ue918\ue906\ue90A\ue90F\ue910\ue903\ue919\ue902\ue915\ue904\ue91E\ue901\ue919\ue90A\ue910\ue909\ue90C\ue90A\ue901\ue913\ue915\ue90A\ue91E\ue91C\ue91E\ue90E\ue912\ue907\ue91D\ue90B\ue918\ue900\ue914\ue900\ue916\ue91C\ue91A\ue90A\ue905\ue91D\ue90C\ue91C\ue906\ue910\ue905\ue909\ue905\ue912\ue918\ue913\ue908\ue91E\ue91A\ue912\ue917\ue90E\ue900\ue910\ue909\ue911\ue91A\ue90C\ue918\ue911\ue903\ue919\ue90F\ue90A\ue908\ue90F\ue901\ue90F\ue90A\ue91D\ue91B\ue908\ue906\ue905\ue91A\ue902\ue91A\ue917\ue917\ue911\ue908\ue91F\ue90C\ue908\ue905\ue916\ue903\ue91F\ue916\ue91E\ue909\ue911\ue918\ue917\ue91E\ue91D\ue90A\ue90C\ue916\ue901\ue919\ue914\ue916\ue90C\ue912\ue916\ue907\ue901\ue91E\ue900\ue913\ue91C\ue918\ue913\ue902\ue918\ue918\ue910\ue91A\ue91D\ue90C\ue917\ue905\ue912\ue900\ue90C\ue90F\ue91B\ue902\ue915\ue912\ue91C\ue91B\ue905\ue90A\ue904\ue91B\ue901\ue91E\ue91E\ue90D\ue900\ue91D\ue90C\ue915\ue90C\ue910\ue91D\ue906\ue919\ue907\ue90F\ue904\ue90E\ue90B\ue919\ue91D\ue912\ue90A\ue914\ue911\ue90E\ue90F\ue916\ue915\ue911\ue91C\ue919\ue90E\ue91F\ue910\ue91E\ue912\ue914\ue914\ue91B\ue910\ue917\ue91D\ue911\ue918\ue909\ue905\ue916\ue908\ue91D\ue90C\ue908\ue910\ue900\ue914\ue911\ue919\ue918\ue90A\ue90D\ue90C\ue907\ue909\ue90E\ue901\ue91C\ue901\ue910\ue911\ue908\ue91E\ue901\ue91E\ue909\ue91B\ue908\ue918\ue902\ue91B\ue90C\ue90F\ue906\ue911\ue90B\ue901\ue91B\ue908\ue913\ue90F\ue90C\ue91E\ue91A\ue91E\ue90B\ue919\ue913\ue903\ue909\ue90A\ue91D\ue914\ue90A\ue919\ue900\ue906\ue909\ue914\ue90C\ue90C\ue90F\ue91D\ue917\ue906\ue91B\ue912\ue908\ue908\ue90F\ue90B\ue906\ue914\ue919\ue903\ue916\ue904\ue904\ue919\ue900\ue91A\ue90A\ue90B\ue917\ue910\ue90D\ue90C\ue908\ue912\ue912\ue90C\ue914\ue901\ue917\ue90E\ue90B\ue91E\ue90D\ue919\ue917\ue902\ue904\ue91D\ue90C\ue911\ue911\ue912\ue919\ue90A\ue91F\ue903\ue904\ue914\ue910\ue907\ue916\ue90A\ue91A\ue90E\ue912\ue909\ue901\ue91A\ue910\ue917\ue906\ue916\ue903\ue91F\ue908\ue90F\ue917\ue91E\ue919\ue904\ue91E\ue918\ue908\ue900\ue91E\ue914\ue90A\ue911\ue907\ue90F\ue91C\ue909\ue904\ue90D\ue911\ue904\ue90B\ue91B\ue912\ue902\ue90B\ue91D\ue91D\ue912\ue916\ue90A\ue903\ue914\ue903\ue905\ue90B\ue908\ue913\ue91B\ue90E\ue90A\ue910\ue90A\ue909\ue91F\ue910\ue909\ue91D\ue91F\ue900\ue914\ue90F\ue918\ue91C\ue913\ue90F\ue902\ue90A\ue900\ue90E\ue90D\ue900\ue917\ue908\ue904\ue902\ue91D\ue901\ue91A\ue91F\ue905\ue908\ue900\ue91E\ue91A\ue919\ue919\ue90E\ue90E\ue919\ue907\ue90D\ue91C\ue913\ue906\ue905\ue902\ue905\ue902\ue90C\ue91F\ue91B\ue911\ue912\ue917\ue90C\ue905\ue908\ue908\ue90F\ue90A\ue909\ue91D\ue906\ue90E\ue904\ue91B\ue90D\ue916\ue919\ue900\ue911\ue903\ue917\ue913\ue916\ue919\ue91D\ue90E\ue912\ue90F\ue918\ue919\ue90C\ue916\ue917\ue903\ue906\ue91B\ue900\ue90E\ue90F\ue90B\ue913\ue91F\ue911\ue90A\ue904\ue903\ue907\ue905\ue907\ue919\ue903\ue905\ue912\ue90D\ue91B\ue91D\ue91D\ue907\ue90B\ue91A\ue91B\ue910\ue918\ue913\ue903\ue91F\ue908\ue91F\ue910\ue90F\ue900\ue90B\ue91F\ue916\ue901\ue902\ue90C\ue90E\ue90F\ue909\ue904\ue901\ue91D\ue907\ue916\ue917\ue91F\ue90D\ue908\ue91C\ue909\ue914\ue906\ue90B\ue909\ue909\ue90E\ue905\ue90F\ue904\ue90B\ue916\ue914\ue903\ue91D\ue91A\ue905\ue901\ue90F\ue90C\ue904\ue90F\ue91C\ue914\ue917\ue914\ue91B\ue900\ue907\ue911\ue91A\ue908\ue90B\ue90D\ue911\ue91E\ue918\ue91F\ue901\ue91D\ue901\ue900\ue909\ue916\ue91B\ue905\ue905\ue918\ue91A\ue917\ue91D\ue916\ue91E\ue90A\ue912\ue91C";
            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                while (true)
                {
                    var m = Random.Range(0, Message.Length - 5);
                    yield return Random.Range(0, 2) == 0 ? Message.Substring(m, 5) : new string(Message.Substring(m, 5).Reverse().ToArray());
                }
            }
            public override int Count => 2 * (Message.Length - 4);
        }
    }
}
