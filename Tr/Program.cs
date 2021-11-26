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
            var tmpJaQuestionsType = assembly.GetType("Souvenir.QuestionJa");

            var allInfos = new Dictionary<string, List<(FieldInfo fld, dynamic attr)>>();
            var addThe = new Dictionary<string, bool>();
            var japanese = new Dictionary<string, string>();
            var trAnswers = new HashSet<string>();
            var trFArgs = new HashSet<string>();

            foreach (var fld in questionsType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                dynamic attr = fld.GetCustomAttribute(attributeType);
                var key = (string) attr.ModuleName;
                if (!allInfos.ContainsKey(key))
                    allInfos.Add(key, new List<(FieldInfo fld, dynamic attr)>());
                allInfos[key].Add((fld, attr));
                addThe[key] = attr.AddThe;

                var japaneseFld = tmpJaQuestionsType.GetField(fld.Name, BindingFlags.Public | BindingFlags.Static);
                if (japaneseFld != null)
                {
                    dynamic japaneseAttr = japaneseFld.GetCustomAttribute(attributeType);
                    japanese[key] = japaneseAttr.ModuleName;
                    japanese[attr.QuestionText] = japaneseAttr.QuestionText;

                    var jaAns = (string[]) japaneseAttr.AllAnswers;
                    var ans = (string[]) attr.AllAnswers;
                    if (ans != null && ans.Length > 0 && ans.Length == jaAns.Length)
                        for (var i = 0; i < ans.Length; i++)
                            if (ans[i] != jaAns[i])
                            {
                                trAnswers.Add(fld.GetValue(null).ToString());
                                japanese[ans[i]] = jaAns[i];
                            }

                    var jaFArgs = (string[]) japaneseAttr.ExampleExtraFormatArguments;
                    var fArgs = (string[]) attr.ExampleExtraFormatArguments;
                    if (fArgs != null && fArgs.Length > 0 && fArgs.Length == jaFArgs.Length)
                        for (var i = 0; i < fArgs.Length; i++)
                            if (fArgs[i] != jaFArgs[i])
                            {
                                trFArgs.Add(fld.GetValue(null).ToString());
                                japanese[fArgs[i]] = jaFArgs[i];
                            }
                }
            }

            Console.WriteLine(string.Join("\r\n", trAnswers));
            Console.WriteLine("--------------------------");
            Console.WriteLine(string.Join("\r\n", trFArgs));

            foreach (var language in "de,eo,es,ja".Split(','))
            {
                string tr(string text) => language != "ja" ? text : !japanese.ContainsKey(text) ? text : japanese[text];
                var alreadyType = assembly.GetType($"Souvenir.Translation_{language}");
                var already = (IDictionary) (alreadyType == null ? null : (dynamic) Activator.CreateInstance(alreadyType))?.Translations;
                var sb = new StringBuilder();
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
                        var exFormatArgs = new[] { (string) attr.ModuleNameWithThe };
                        if (attr.ExampleExtraFormatArguments != null)
                            exFormatArgs = exFormatArgs.Concat(((string[]) attr.ExampleExtraFormatArguments).Take((int) attr.ExampleExtraFormatArgumentGroupSize).Select(str => str == "\ufffdordinal" ? "first" : str)).ToArray();
                        try { sb.AppendLine($"            // {string.Format(qText, exFormatArgs)}"); }
                        catch { }
                        var answers = attr.AllAnswers == null || attr.AllAnswers.Length == 0 ? null : (string[]) attr.AllAnswers;
                        var formatArgs = attr.ExampleExtraFormatArguments == null || attr.ExampleExtraFormatArguments.Length == 0 ? null : ((string[]) attr.ExampleExtraFormatArguments).Distinct().ToArray();
                        dynamic ti = already?.Contains(id) == true ? already[id] : null;
                        sb.AppendLine($@"            [Question.{id}] = new TranslationInfo");
                        sb.AppendLine("            {");
                        sb.AppendLine($@"                QuestionText = ""{tr((string) (ti?.QuestionText) ?? qText).CLiteralEscape()}"",");
                        if (ti?.ModuleName != null)
                            sb.AppendLine($@"                ModuleName = ""{tr((string) ti.ModuleName).CLiteralEscape()}"",");
                        if (answers != null && attr.TranslateAnswers)
                        {
                            sb.AppendLine("                Answers = new Dictionary<string, string>");
                            sb.AppendLine("                {");
                            foreach (var answer in answers)
                                sb.AppendLine($@"                    [""{answer.CLiteralEscape()}""] = ""{tr((ti?.Answers?.ContainsKey(answer) == true ? (string) ti.Answers[answer] : answer)).CLiteralEscape()}"",");
                            sb.AppendLine("                },");
                        }
                        if (formatArgs != null && attr.TranslateFormatArgs != null && Enumerable.Contains(attr.TranslateFormatArgs, true))
                        {
                            sb.AppendLine("                FormatArgs = new Dictionary<string, string>");
                            sb.AppendLine("                {");
                            for (var fArgIx = 0; fArgIx < formatArgs.Length; fArgIx++)
                                if (attr.TranslateFormatArgs[fArgIx % attr.ExampleExtraFormatArgumentGroupSize])
                                    sb.AppendLine($@"                    [""{formatArgs[fArgIx].CLiteralEscape()}""] = ""{tr((ti?.FormatArgs?.ContainsKey((string) formatArgs[fArgIx]) == true ? (string) ti.FormatArgs[(string) formatArgs[fArgIx]] : formatArgs[fArgIx])).CLiteralEscape()}"",");
                            sb.AppendLine("                },");
                        }
                        sb.AppendLine("            },");
                    }
                    sb.AppendLine("");
                }
                sb.AppendLine("        };");

                var path = Path.Combine(args[1], $"Translation{language.ToUpperInvariant()}.cs");
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($@"File {path} does not exist.");
                    continue;
                }
                var alreadyFile = File.ReadLines(path).ToList();
                var p1 = alreadyFile.FindIndex(str => str.Trim() == "#region Translatable strings");
                var p2 = alreadyFile.FindIndex(p1 + 1, str => str.Trim() == "#endregion");
                if (p1 == -1 || p2 == -1)
                {
                    Console.Error.WriteLine($@"File {path} does not contain the “#region Translatable strings” and “#endregion” directives. Please put them back in.");
                    continue;
                }
                File.WriteAllText(path, $"{string.Join(Environment.NewLine, alreadyFile.Take(p1 + 1))}{Environment.NewLine}{sb}{string.Join(Environment.NewLine, alreadyFile.Skip(p2))}");
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
