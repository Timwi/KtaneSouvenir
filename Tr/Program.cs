using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SouvenirTr
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFrom(args[0]);
            var questionsType = assembly.GetType("Souvenir.Question");
            var attributeType = assembly.GetType("Souvenir.SouvenirQuestionAttribute");

            var allInfos = new Dictionary<string, List<(FieldInfo fld, dynamic attr)>>();
            var addThe = new Dictionary<string, bool>();

            foreach (var fld in questionsType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                dynamic attr = fld.GetCustomAttribute(attributeType);
                var key = (string) attr.ModuleName;
                if (!allInfos.ContainsKey(key))
                    allInfos.Add(key, new List<(FieldInfo fld, dynamic attr)>());
                allInfos[key].Add((fld, attr));
                addThe[key] = attr.AddThe;
            }

            foreach (var language in "ja,eo".Split(','))
            {
                var alreadyType = assembly.GetType($"Souvenir.Translation_{language}");
                var already = (IDictionary) (alreadyType == null ? null : (dynamic) Activator.CreateInstance(alreadyType))?.Translations;
                var sb = new StringBuilder();
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("");
                sb.AppendLine("namespace Souvenir");
                sb.AppendLine("{");
                sb.AppendLine($"    public class Translation_{language} : Translation");
                sb.AppendLine("    {");
                sb.AppendLine("        public override Dictionary<Question, TranslationInfo> Translations => new Dictionary<Question, TranslationInfo>");
                sb.AppendLine("        {");
                foreach (var kvp in allInfos)
                {
                    sb.AppendLine($"            // {(addThe[kvp.Key] ? "The " : "")}{kvp.Key}");
                    foreach (var (fld, attr) in kvp.Value)
                    {
                        var id = fld.GetValue(null);
                        var qText = (string) attr.QuestionText;
                        sb.AppendLine($"            // {qText}");
                        var answers = attr.AllAnswers == null || attr.AllAnswers.Length == 0 ? null : (string[]) attr.AllAnswers;
                        var formatArgs = attr.ExampleExtraFormatArguments == null || attr.ExampleExtraFormatArguments.Length == 0 ? null : ((string[]) attr.ExampleExtraFormatArguments).Distinct().ToArray();
                        dynamic ti = already?.Contains(id) == true ? already[id] : null;
                        sb.AppendLine($@"            [Question.{id}] = new TranslationInfo");
                        sb.AppendLine("            {");
                        sb.AppendLine($@"                QuestionText = ""{((string) (ti?.QuestionText) ?? qText).CLiteralEscape()}"",");
                        if (ti?.ModuleName != null)
                            sb.AppendLine($@"                ModuleName = ""{((string) ti.ModuleName).CLiteralEscape()}"",");
                        if (answers != null && attr.TranslateAnswers)
                        {
                            sb.AppendLine("                Answers = new Dictionary<string, string>");
                            sb.AppendLine("                {");
                            foreach (var answer in answers)
                                sb.AppendLine($@"                    [""{answer.CLiteralEscape()}""] = ""{(ti?.Answers?.ContainsKey(answer) == true ? (string) ti.Answers[answer] : answer).CLiteralEscape()}"",");
                            sb.AppendLine("                },");
                        }
                        if (formatArgs != null && attr.TranslateFormatArgs)
                        {
                            sb.AppendLine("                FormatArgs = new Dictionary<string, string>");
                            sb.AppendLine("                {");
                            foreach (var formatArg in formatArgs)
                                sb.AppendLine($@"                    [""{formatArg.CLiteralEscape()}""] = ""{(ti?.FormatArgs?.ContainsKey(formatArg) == true ? (string) ti.FormatArgs[formatArg] : formatArg).CLiteralEscape()}"",");
                            sb.AppendLine("                },");
                        }
                        sb.AppendLine("            },");
                    }
                    sb.AppendLine("");
                }
                sb.AppendLine("        };");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                File.WriteAllText(Path.Combine(args[1], $"Translation{language.ToUpperInvariant()}.cs"), sb.ToString());
            }
        }

        /// <summary>
        ///     Escapes all characters in this string whose code is less than 32 or form invalid UTF-16 using C/C#-compatible
        ///     backslash escapes.</summary>
        static string CLiteralEscape(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var result = new StringBuilder(value.Length + value.Length / 2);

            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case '\0': result.Append(@"\0"); break;
                    case '\a': result.Append(@"\a"); break;
                    case '\b': result.Append(@"\b"); break;
                    case '\t': result.Append(@"\t"); break;
                    case '\n': result.Append(@"\n"); break;
                    case '\v': result.Append(@"\v"); break;
                    case '\f': result.Append(@"\f"); break;
                    case '\r': result.Append(@"\r"); break;
                    case '\\': result.Append(@"\\"); break;
                    case '"': result.Append(@"\"""); break;
                    default:
                        if (c >= 0xD800 && c < 0xDC00)
                        {
                            if (i == value.Length - 1) // string ends on a broken surrogate pair
                                result.AppendFormat(@"\u{0:X4}", (int) c);
                            else
                            {
                                char c2 = value[i + 1];
                                if (c2 >= 0xDC00 && c2 <= 0xDFFF)
                                {
                                    // nothing wrong with this surrogate pair
                                    i++;
                                    result.Append(c);
                                    result.Append(c2);
                                }
                                else // first half of a surrogate pair is not followed by a second half
                                    result.AppendFormat(@"\u{0:X4}", (int) c);
                            }
                        }
                        else if (c >= 0xDC00 && c <= 0xDFFF) // the second half of a broken surrogate pair
                            result.AppendFormat(@"\u{0:X4}", (int) c);
                        else if (c >= ' ')
                            result.Append(c);
                        else // the character is in the 0..31 range
                            result.AppendFormat(@"\u{0:X4}", (int) c);
                        break;
                }
            }

            return result.ToString();
        }
    }
}
