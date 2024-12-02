using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Souvenir
{
    /// <summary>The base class for answer generators.</summary>
    /// <remarks>
    /// <para>
    ///     Answer generators provide a means to easily specify common wrong answer patterns on Souvenir questions without having to write out the entire list.
    /// </para>
    /// <para>
    ///     To use an answer generator, add an attribute which is a subclass of <see cref="AnswerGeneratorAttribute"/> to the <see cref="Question"/> enum value.
    ///     <see cref="SouvenirQuestionAttribute.AllAnswers"/> should usually be null when using an answer generator.
    ///     When the question is generated, any answer generator will be used to effectively append as many randomly selected answers as needed to <see cref="SouvenirQuestionAttribute.AllAnswers"/>.
    ///     Preferred wrong answers may still be specified in addition to an answer generator, or instead of one as was done previously.
    /// </para>
    /// <para>
    ///     Answer generator implementations are provided as nested classes in the <see cref="AnswerGenerator"/> class.
    ///     See the documentation for these classes for more details.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    ///     [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
    ///     [AnswerGenerator.Integers(0, 99, "00")]
    ///     TwoBitsResponse
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public abstract class AnswerGeneratorAttribute : Attribute
    {
        public abstract IEnumerable<string> GetAnswers(SouvenirModule module);
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public abstract class SpriteAnswerGeneratorAttribute : Attribute
    {
        public abstract IEnumerable<Sprite> GetAnswers(SouvenirModule module);
    }

    public static class AnswerGenerator
    {
        /// <summary>An answer generator that generates numeric answers within a specified range.</summary>
        /// <remarks>
        /// <para>
        ///     This generator generates string representations of numbers within a range specified by inclusive bounds and a step size.
        ///     The numbers may optionally be formatted using a format string. For more details, see https://docs.microsoft.com/en-us/dotnet/api/system.int32.tostring?view=netframework-3.5.
        /// </para>
        /// <para>
        ///     If a step size is specified, the generated numbers will be multiples of the step size plus the minimum.
        ///     The maximum may not be included if the step size is greater than 1.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        ///     [AnswerGenerator.Integers(1, 12)]  // Generates integers between 1 and 12 inclusive.
        ///     [AnswerGenerator.Integers(0, 50, 5)]  // Generates multiples of 5 between 0 and 50 inclusive.
        ///     [AnswerGenerator.Integers(0, 999, "000")]  // Generates integers between 0 and 999 and adds leading zeros.
        /// </code>
        /// </example>
        public class Integers : AnswerGeneratorAttribute
        {
            public int Min { get; private set; }
            public int MaxSteps { get; private set; }
            public int Step { get; private set; }
            public string Format { get; private set; }

            /// <param name="min">The inclusive lower bound for generated numbers.</param>
            /// <param name="max">The inclusive upper bound for generated numbers.</param>
            public Integers(int min, int max) : this(min, max, 1, null) { }
            /// <param name="min">The inclusive lower bound for generated numbers.</param>
            /// <param name="max">The inclusive upper bound for generated numbers.</param>
            /// <param name="format">A format string used to format generated numbers.</param>
            public Integers(int min, int max, string format) : this(min, max, 1, format) { }
            /// <param name="min">The inclusive lower bound for generated numbers.</param>
            /// <param name="max">The inclusive upper bound for generated numbers.</param>
            /// <param name="step">The step size to use for generated numbers.</param>
            public Integers(int min, int max, int step) : this(min, max, step, null) { }
            /// <param name="min">The inclusive lower bound for generated numbers.</param>
            /// <param name="max">The inclusive upper bound for generated numbers.</param>
            /// <param name="step">The step size to use for generated numbers.</param>
            /// <param name="format">A format string used to format generated numbers.</param>
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
                for (int i = MaxSteps - 1; i >= 0; --i) values[i] = i * Step + Min;
                values.Shuffle();
                foreach (var i in values) yield return i.ToString(Format);
            }
        }

        /// <summary>An answer generator that generates answers consisting of randomly selected characters.</summary>
        /// <remarks>
        /// <para>
        ///     This generator builds answers using a specified number of random characters from one or more lists.
        ///     Each list specifies a combination of individual characters and ranges, and chooses a specified number
        ///     of characters from that list, with replacement.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        ///     [AnswerGenerator.Strings('A', 'Z')]  // Generates single uppercase letters.
        ///     [AnswerGenerator.Strings("A-Z-")]  // Generates answers with either a single uppercase letter or a hyphen.
        ///     [AnswerGenerator.Strings("A-F", "1-6")]  // Generates answers consisting of a letter A~F followed by a digit 1~6.
        ///     [AnswerGenerator.Strings("2*0-9A-Z")]  // Generates answers consisting of two alphanumeric characters.
        /// </code>
        /// </example>
        public class Strings : AnswerGeneratorAttribute
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
                    var n = Chars != null ? Chars.Length : 0;
                    if (Ranges != null)
                    {
                        for (int i = 0; i < Ranges.Length; i += 2) n += Ranges[i + 1] + 1 - Ranges[i];
                    }
                    n = Random.Range(0, n);
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
                    int count = 1;
                    var chars = new List<char>();
                    var ranges = new List<char>();

                    int i = expression.IndexOf('*');
                    if (i > 0 && int.TryParse(expression.Substring(0, i), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out count))
                    {
                        if (count == 0) throw new FormatException("Count in a character list expression must be positive.");
                        i++;
                    }
                    else
                        i = 0;
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
            }

            private readonly CharacterList[] characterLists;

            /// <param name="characterLists">A list of expressions specifying character lists.</param>
            /// <remarks>
            /// <para>
            ///     A character list expression specifies a list of characters and a number of characters to choose from that list.
            ///     A character followed by a hyphen ('-') and another character specifies a range, including all characters between the bounds inclusively.
            ///     Other characters are directly included in the list.
            ///     A hyphen may be included in the list by placing it at the start or end of the string, or immediately after a range.
            /// </para>
            /// <para>
            ///     The expression may be prefixed with a positive integer followed by a '*'. This specifies the number of characters to choose from the list.
            ///     If no count is specified, one character is chosen.
            /// </para>
            /// </remarks>
            public Strings(params string[] characterLists)
            {
                this.characterLists = new CharacterList[characterLists.Length];
                for (int i = characterLists.Length - 1; i >= 0; --i)
                    this.characterLists[i] = CharacterList.Parse(characterLists[i]);
            }
            /// <param name="count">The number of characters to choose.</param>
            /// <param name="chars">The list of characters to choose from.</param>
            public Strings(int count, params char[] chars) : this(new CharacterList(count, chars, null)) { }
            /// <param name="count">The number of characters to choose.</param>
            /// <param name="chars">The list of characters to choose from.</param>
            public Strings(int count, string chars) : this(new CharacterList(count, chars.ToCharArray(), null)) { }
            /// <param name="first">The inclusive lower bound of the range of characters to choose from.</param>
            /// <param name="last">The inclusive upper bound of the range of characters to choose from.</param>
            public Strings(char first, char last) : this(new CharacterList(1, null, new[] { first, last })) { }
            /// <param name="count">The number of characters to choose.</param>
            /// <param name="first">The inclusive lower bound of the range of characters to choose from.</param>
            /// <param name="last">The inclusive upper bound of the range of characters to choose from.</param>
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
                    {
                        for (int i = characterList.Count - 1; i >= 0; --i) builder.Append(characterList.Pick());
                    }
                    yield return builder.ToString();
                    builder.Length = 0;
                }
            }
        }

        /// <summary>An answer generator that generates answers consisting of randomly selected grid cells.</summary>
        public class Grid : SpriteAnswerGeneratorAttribute
        {
            private readonly int _width;
            private readonly int _height;
            private readonly float _size;

            private int Count => _width * _height;

            public Grid(int width, int height, float size = 1f)
            {
                _width = width;
                _height = height;
                _size = size;
            }

            public override IEnumerable<Sprite> GetAnswers(SouvenirModule module)
            {
                for (int ix = 0; ix < Count; ++ix)
                    yield return Souvenir.Sprites.GenerateGridSprite(_width, _height, ix, _size);
            }
        }

        /// <summary>
        /// An answer generator that generates answers based on ordinal.
        /// <example>
        /// <code>
        ///     [AnswerGenerator.Ordinal(3)] //Generate the ordinals for "first", "second", and "third" in the target language
        ///     [AnswerGenerator.Ordinal(1, 50, 10)] //Generates ordinals that are multiples of 10 between 1 and 50 inclusive in the target language.
        ///     [AnswerGenerator.Ordinal(2, 5)] //Generates ordinals "second", "third", "fourth", and "fifth" in the target language
        /// </code>
        /// </example>
        /// </summary>
        public class Ordinal : AnswerGeneratorAttribute
        {
            public int Min { get; private set; }
            public int MaxSteps { get; private set; }
            public int Step { get; private set; }

            /// <param name="max">The inclusive upper bound for generated ordinal.</param>
            public Ordinal(int max) : this(1, max, 1) { }
            /// <param name="min">The inclusive lower bound for generated ordinal.</param>
            /// <param name="max">The inclusive upper bound for generated ordinal.</param>
            public Ordinal(int min, int max) : this(min, max, 1) { }
            /// <param name="min">The inclusive lower bound for generated ordinal.</param>
            /// <param name="max">The inclusive upper bound for generated ordinal.</param>
            /// <param name="step">The step size to use for generated ordinal.</param>
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
                for (int i = MaxSteps - 1; i >= 0; --i) values[i] = i * Step + Min;
                values.Shuffle();
                foreach (var i in values) yield return module.Ordinal(i);
            }
        }
    }
}
