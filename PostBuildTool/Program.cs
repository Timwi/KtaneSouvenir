using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using RT.CommandLine;
using RT.Util.ExtensionMethods;
using RT.Util.Text;

namespace SouvenirPostBuildTool
{
    class CommandLineOptions
    {
        [IsMandatory, IsPositional, Documentation("Specifies the full path and filename of SouvenirLib.dll.")]
        public string AssemblyPath = null;

        [IsMandatory, IsPositional, Documentation("Specifies the path to the KTANE files.")]
        public string GameFolder = null;

        [Option("-c", "--contributors"), Documentation("Specifies the path and filename to the CONTRIBUTORS.md file to be updated.")]
        public string ContributorsFile = null;

        [Option("-t", "--translations"), Documentation("If specified, the translation files in this folder are updated.")]
        public string TranslationsFolder = null;
    }

    public static class Program
    {
        static int Main(string[] args)
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

            if (opt.TranslationsFolder != null)
                DoTranslationStuff(opt.TranslationsFolder, assembly);

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
                contributorToModules.AddSafe((string) ((dynamic) entry.Value).Contributor, (string) ((dynamic) entry.Value).ModuleName);

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
                sb.Append(tt.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(row => $"    {row.Trim().Replace("|", "│")}").JoinString(Environment.NewLine));
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

        private static void DoTranslationStuff(string translationFilePath, Assembly assembly)
        {
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

            foreach (var language in "de,ja,ru".Split(','))
            {
                var alreadyType = assembly.GetType($"Souvenir.Translation_{language}");
                var already = (IDictionary) (alreadyType == null ? null : (dynamic) Activator.CreateInstance(alreadyType))?.Translations;
                var sb = new StringBuilder();
                sb.AppendLine("        protected override Dictionary<Question, TranslationInfo> _translations => new()");
                sb.AppendLine("        {");
                foreach (var kvp in allInfos)
                {
                    sb.AppendLine($"            // {(addThe[kvp.Key] ? "The " : "")}{kvp.Key}");
                    foreach (var (fld, attr) in kvp.Value)
                    {
                        var id = fld.GetValue(null);
                        var qText = (string) attr.QuestionText;
                        sb.AppendLine($"            // {qText}");
                        var exFormatArgs = new[] { ((string) attr.ModuleNameWithThe).Replace("\u00a0", " ") };
                        if (attr.ExampleExtraFormatArguments != null)
                            exFormatArgs = exFormatArgs.Concat(((string[]) attr.ExampleExtraFormatArguments).Take((int) attr.ExampleExtraFormatArgumentGroupSize).Select(str => str == "\ufffdordinal" ? "first" : str)).ToArray();
                        string formatArgsComment = null;
                        try { formatArgsComment = string.Format(qText, exFormatArgs); }
                        catch { }
                        if (formatArgsComment != null)
                            sb.AppendLine($"            // {formatArgsComment}");
                        var answers = attr.AllAnswers == null || attr.AllAnswers.Length == 0 ? null : (string[]) attr.AllAnswers;
                        dynamic ti = already?.Contains(id) == true ? already[id] : null;
                        sb.AppendLine($@"            [Question.{id}] = new TranslationInfo");
                        sb.AppendLine("            {");
                        sb.AppendLine($@"                QuestionText = ""{((string) (ti?.QuestionText) ?? qText).CLiteralEscape()}"",");
                        if (ti?.ModuleName != null)
                            sb.AppendLine($@"                ModuleName = ""{((string) ti.ModuleName).CLiteralEscape()}"",");
                        if (ti?.ModuleNameWithThe != null)
                            sb.AppendLine($@"                ModuleNameWithThe = ""{((string) ti.ModuleNameWithThe).CLiteralEscape()}"",");
                        var trFAs = (bool[]) attr.TranslateFormatArgs;
                        var formatArgs = attr.ExampleExtraFormatArguments == null || attr.ExampleExtraFormatArguments.Length == 0 || trFAs == null || trFAs.Length == 0 ? null :
                            ((string[]) attr.ExampleExtraFormatArguments).Split((int) attr.ExampleExtraFormatArgumentGroupSize)
                                .SelectMany(chunk => Enumerable.Range(0, trFAs.Length).Where(ix => trFAs[ix]).Select(ix => chunk.Skip(ix).First()))
                                .Distinct().ToArray();
                        if (formatArgs != null)
                        {
                            sb.AppendLine("                FormatArgs = new Dictionary<string, string>");
                            sb.AppendLine("                {");
                            foreach (var fa in formatArgs)
                                sb.AppendLine($@"                    [""{fa.CLiteralEscape()}""] = ""{(ti?.FormatArgs?.ContainsKey(fa) == true ? (string) ti.FormatArgs[fa] : fa).CLiteralEscape()}"",");
                            sb.AppendLine("                },");
                        }
                        if (answers != null && attr.TranslateAnswers)
                        {
                            sb.AppendLine("                Answers = new Dictionary<string, string>");
                            sb.AppendLine("                {");
                            foreach (var answer in answers)
                                sb.AppendLine($@"                    [""{answer.CLiteralEscape()}""] = ""{(ti?.Answers?.ContainsKey(answer) == true ? (string) ti.Answers[answer] : answer).CLiteralEscape()}"",");
                            sb.AppendLine("                },");
                        }
                        sb.AppendLine("            },");
                    }
                    sb.AppendLine("");
                }
                sb.AppendLine("        };");

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
