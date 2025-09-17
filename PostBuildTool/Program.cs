using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using RT.CommandLine;
using RT.Util.ExtensionMethods;
using RT.Util.Text;

namespace SouvenirPostBuildTool;

public static class Program
{
    private static int Main(string[] args)
    {
        CommandLineOptions opt;
        try { opt = CommandLineParser.Parse<CommandLineOptions>(args); }
        catch (CommandLineParseException cpe)
        {
            cpe.WriteUsageInfoToConsole();
            return 1;
        }

        AppDomain.CurrentDomain.AssemblyResolve += delegate (object sender, ResolveEventArgs e)
        {
            var assemblyPath = Path.Combine(opt.GameFolder, new AssemblyName(e.Name).Name + ".dll");
            return File.Exists(assemblyPath) ? Assembly.LoadFrom(assemblyPath) : null;
        };
        var assembly = Assembly.LoadFrom(opt.AssemblyPath);

        if (opt.ContributorsFile != null)
            DoContributorStuff(opt.ContributorsFile, assembly);

        Dictionary<string, string> translationStats = null;
        if (opt.TranslationsFolder != null)
            translationStats = DoTranslationStuff(opt.TranslationsFolder, assembly);

        if (opt.DataFile != null)
            DoDataFileStuff(opt.DataFile, assembly, translationStats);

        return 0;
    }

    private static void DoContributorStuff(string filepath, Assembly assembly)
    {
        var moduleType = assembly.GetType("SouvenirModule");
        var module = Activator.CreateInstance(moduleType);
        var awakeMethod = moduleType.GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        awakeMethod.Invoke(module, null);
        var fldDictionary = moduleType.GetField("_moduleProcessors", BindingFlags.NonPublic | BindingFlags.Instance);
        var dictionary = (IDictionary) fldDictionary.GetValue(module);
        var contributorToModules = new Dictionary<string, List<string>>();
        foreach (DictionaryEntry entry in dictionary)
            contributorToModules.AddSafe((string) ((dynamic) entry.Value).Contributor, ((string) ((dynamic) entry.Value).ModuleName).Replace("\uE001", "").Replace("\uE002", ""));

        const int numColumns = 5;

        var sb = new StringBuilder();
        sb.Append($"# Souvenir implementors{Environment.NewLine}{Environment.NewLine}The following is a list of modules supported by Souvenir, and the fine people who have contributed their effort to make it happen:{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
        foreach (var group in contributorToModules.Where(gr => gr.Value.Count > numColumns).OrderByDescending(gr => gr.Value.Count).ThenBy(gr => gr.Key))
        {
            sb.Append($"## Implemented by {group.Key} ({group.Value.Count}){Environment.NewLine}{Environment.NewLine}");
            var tt = new TextTable { ColumnSpacing = 5, VerticalRules = true };
            var numItems = group.Value.Count;
            var numRows = (numItems + numColumns - 1) / numColumns;
            var col = 0;
            foreach (var column in group.Value.Order(StringComparer.InvariantCultureIgnoreCase).Split(numRows))
            {
                var row = 0;
                foreach (var moduleName in column)
                    tt.SetCell(col, row++, moduleName);
                col++;
            }
            sb.Append(tt.ToString().Split(['\n'], StringSplitOptions.RemoveEmptyEntries).Select(row => $"    {row.Trim().Replace("|", "│")}").JoinString(Environment.NewLine));
            sb.Append(Environment.NewLine + Environment.NewLine);
        }

        var remTable = new TextTable { ColumnSpacing = 5, RowSpacing = 1, VerticalRules = true, HorizontalRules = true, HeaderRows = 1 };
        remTable.SetCell(0, 0, "MODULE");
        remTable.SetCell(1, 0, "IMPLEMENTED BY");
        var remaining = contributorToModules
            .Where(gr => gr.Value.Count <= numColumns)
            .SelectMany(gr => gr.Value.Select(v => (author: gr.Key, module: v)))
            .OrderBy(tup => tup.module, StringComparer.InvariantCultureIgnoreCase)
            .ToArray();
        for (var i = 0; i < remaining.Length; i++)
        {
            remTable.SetCell(0, i + 1, remaining[i].module);
            remTable.SetCell(1, i + 1, remaining[i].author);
        }
        sb.Append($"## Others{Environment.NewLine}{Environment.NewLine}{remTable.ToString().Split('\n').Select(r => r.Trim()).Where(row => !string.IsNullOrWhiteSpace(row) && !Regex.IsMatch(row, @"^-*\|-*$")).Select(row => $"    {row.Replace("|", "│").Replace("=│=", "═╪═").Replace("=", "═")}").JoinString(Environment.NewLine)}{Environment.NewLine}{Environment.NewLine}");

        File.WriteAllText(filepath, sb.ToString());
    }

    private static Dictionary<string, string> DoTranslationStuff(string translationFilePath, Assembly assembly)
    {
        var handlerAttrType = assembly.GetType("Souvenir.SouvenirHandlerAttribute");
        var questionAttrType = assembly.GetType("Souvenir.SouvenirQuestionAttribute");
        var discrAttrType = assembly.GetType("Souvenir.SouvenirDiscriminatorAttribute");
        var languages = (assembly.GetType("Souvenir.TranslationInfo").GetField("AllTranslations", BindingFlags.Static | BindingFlags.Public).GetValue(null) as IDictionary).Keys.Cast<string>().ToArray();

        var allInfos = new Dictionary<string, List<(FieldInfo fld, dynamic attr)>>();
        var addThe = new Dictionary<string, bool>();

        var translationStats = new Dictionary<string, string>();
        foreach (var language in languages)
        {
            var path = Path.Combine(translationFilePath, $"Translation{language.ToUpperInvariant()}.cs");
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
            var indentation = alreadyFile[p1].IndexOf('#');
            string indent() => new(' ', indentation);

            var alreadyType = assembly.GetType($"Souvenir.Translation_{language}");
            var baseType = alreadyType;
            while (baseType != null && !(baseType.IsGenericType && baseType.GetGenericTypeDefinition().Name == "TranslationBase`1"))
                baseType = baseType.BaseType;
            var translationInfoType = baseType.GetGenericArguments()[0];
            var translationInfoPrototype = Activator.CreateInstance(translationInfoType);
            var tiFields = translationInfoType.GetFields().ToArray();

            var already = alreadyType == null ? null : (dynamic) Activator.CreateInstance(alreadyType);
            var outerSb = new StringBuilder();
            var translatedModuleCount = 0;
            var totalModuleCount = 0;
            var translatedQuestionCount = 0;
            var totalQuestionCount = 0;

            // for each module handler...
            foreach (var handlerMethod in assembly.GetType("SouvenirModule").GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                dynamic handler = handlerMethod.GetCustomAttribute(handlerAttrType);
                if (handler == null)
                    continue;

                var enumType = (Type) handler.EnumType;
                dynamic alreadyHandler = already?.TranslateModule(enumType);

                outerSb.AppendLine($"{indent()}[typeof({enumType.Name})] =");
                outerSb.AppendLine($"{indent()}{{");
                indentation += 4;

                var needsTranslation = false;
                var sb = new StringBuilder();
                var skip = "NeedsTranslation,ModuleName,Questions,Discriminators".Split(',');
                if (alreadyHandler != null)
                    foreach (var f in tiFields)
                        if (!skip.Contains(f.Name) && f.GetValue(alreadyHandler) is object v && !v.Equals(f.GetValue(translationInfoPrototype)))
                            sb.AppendLine($@"{indent()}{f.Name} = {(
                                f.FieldType == typeof(string) ? $@"""{((string) v).CLiteralEscape()}""" :
                                f.FieldType.IsEnum ? $@"{f.FieldType.Name}.{v}" :
                                throw new NotImplementedException()
                            )},");

                var questionsAndDiscriminators = enumType.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(f => (enumValue: f.GetValue(null), qAttr: (dynamic) f.GetCustomAttribute(questionAttrType), dAttr: (dynamic) f.GetCustomAttribute(discrAttrType)))
                    .ToArray();

                sb.AppendLine($"{indent()}Questions =");
                sb.AppendLine($"{indent()}{{");
                indentation += 4;

                // for each question for this module...
                foreach (var (enumValue, qAttr, _) in questionsAndDiscriminators.Where(tup => tup.qAttr != null))
                {
                    dynamic alreadyQuestion = already?.TranslateQuestion(enumValue);

                    sb.AppendLine($"{indent()}[{enumValue.GetType().Name}.{enumValue}] =");
                    sb.AppendLine($"{indent()}{{");
                    indentation += 4;

                    if (qAttr.IsEntireQuestionSprite)
                        sb.AppendLine($"{indent()}// This question is depicted visually, rather than with words. The translation here will only be used for logging.");
                    else
                    {
                        sb.AppendLine($@"{indent()}// Question: {qAttr.QuestionText}");
                        if (qAttr.Arguments is string[] args)
                            sb.AppendLine($@"{indent()}// Example: {string.Format((string) qAttr.QuestionText, (object[]) [handler.ModuleName, .. args])}");
                    }
                    sb.AppendLine($@"{indent()}Question = ""{((string) (alreadyQuestion?.Question ?? qAttr.QuestionText)).CLiteralEscape()}"",");

                    void AddDictionary(string name, IEnumerable<string> original, Dictionary<string, string> already)
                    {
                        sb.AppendLine($"{indent()}{name} =");
                        sb.AppendLine($"{indent()}{{");
                        indentation += 4;
                        foreach (var str in original)
                            sb.AppendLine($@"{indent()}[""{str.CLiteralEscape()}""] = ""{(already?.Get(str, null) ?? str).CLiteralEscape()}"",");
                        indentation -= 4;
                        sb.AppendLine($"{indent()}}},");
                    }

                    if (qAttr.ExampleAnswers is string[] { Length: > 0 } origAnswers && (bool) qAttr.TranslateAnswers)
                        AddDictionary("Answers", origAnswers.Distinct(), alreadyQuestion?.Answers);
                    if (qAttr.Arguments is string[] { Length: > 0 } origArguments && qAttr.ArgumentGroupSize is int groupSize && qAttr.TranslateArguments is bool[] trArgs)
                        AddDictionary("Arguments", origArguments.Select((str, ix) => trArgs[ix % groupSize] ? str : null).Where(s => s != null).Distinct(), alreadyQuestion?.Arguments);
                    if (qAttr.TranslatableStrings is string[] { Length: > 0 } origStrings)
                        AddDictionary("Additional", origStrings.Distinct(), alreadyQuestion?.Additional);

                    indentation -= 4;
                    sb.AppendLine($"{indent()}}},");
                }

                outerSb.AppendLine("}");

                translationStats[language] = $"{((float) translatedCount / totalCount * 100f).ToString("f2", CultureInfo.InvariantCulture)}%";

                File.WriteAllText(path, $"{string.Join(Environment.NewLine, alreadyFile.Take(p1 + 1))}{Environment.NewLine}{outerSb}{string.Join(Environment.NewLine, alreadyFile.Skip(p2))}");
            }
        }
        return translationStats;
    }

    private static void DoDataFileStuff(string path, Assembly assembly, Dictionary<string, string> translationStats)
    {
        var moduleType = assembly.GetType("SouvenirModule");
        var languages = (assembly.GetType("Souvenir.TranslationInfo").GetField("AllTranslations", BindingFlags.Static | BindingFlags.Public).GetValue(null) as IDictionary).Keys.Cast<string>().ToArray();
        var module = Activator.CreateInstance(moduleType);
        var awakeMethod = moduleType.GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        awakeMethod.Invoke(module, null);
        var fldDictionary = moduleType.GetField("_moduleProcessors", BindingFlags.NonPublic | BindingFlags.Instance);
        var dictionary = (IDictionary) fldDictionary.GetValue(module);
        var contributors = new HashSet<string>();
        var modules = new HashSet<string>();
        foreach (DictionaryEntry entry in dictionary)
        {
            contributors.Add(((dynamic) entry.Value).Contributor);
            modules.Add(((dynamic) entry.Value).ModuleName);
        }

        var questionsType = assembly.GetType("Souvenir.Question");

        if (translationStats is null)
        {
            translationStats = [];

            foreach (var language in languages)
            {
                var alreadyType = assembly.GetType($"Souvenir.Translation_{language}");
                var translationsProp = alreadyType.GetProperty("_translations", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                var already = alreadyType == null ? null : (dynamic) Activator.CreateInstance(alreadyType);
                if (already == null)
                {
                    translationStats[language] = "Unknown";
                    continue;
                }
                var translations = translationsProp.GetValue(already);

                var count = 0;
                var done = 0;
                foreach (var tr in translations?.Values)
                {
                    count++;
                    if (!tr.NeedsTranslation)
                        done++;
                }
                var percent = (float) done / count * 100f;
                translationStats.Add(language, $"{percent:f2}%");
            }
        }

        File.WriteAllText(path, $$"""
        {
            "contributorsCount": {{contributors.Count}},
            "modulesCount": {{modules.Count}},
            "questionsCount": {{Enum.GetValues(questionsType).Length}},
            "translationProgress": { {{languages.Select(lang => $@"""{lang}"": ""{translationStats[lang]}""").JoinString(", ")}} }
        }
        """);
    }

    /// <summary>
    ///     Escapes all characters in this string whose code is less than 32 or form invalid UTF-16 using C/C#-compatible
    ///     backslash escapes.</summary>
    private static string CLiteralEscape(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var result = new StringBuilder(value.Length + value.Length / 2);

        for (var i = 0; i < value.Length; i++)
        {
            var c = value[i];
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
                    if (c is >= '\uE000' and < '\uF900') // Private Use Area
                        result.AppendFormat(@"\u{0:X4}", (int) c);
                    else if (c is >= '\uD800' and < '\uDC00')
                    {
                        if (i == value.Length - 1) // string ends on a broken surrogate pair
                            result.AppendFormat(@"\u{0:X4}", (int) c);
                        else
                        {
                            var c2 = value[i + 1];
                            if (c2 is >= '\uDC00' and <= '\uDFFF')
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
                    else if (c is >= '\uDC00' and <= '\uDFFF') // the second half of a broken surrogate pair
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
