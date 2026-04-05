using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using RT.CommandLine;
using RT.Json;
using RT.Serialization;
using RT.Util;
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

        if (opt.JsFile != null)
            DoManualQuestionsStuff(opt.JsFile, assembly);

        Dictionary<string, string> translationStats = null;
        if (opt.DoTranslations)
            translationStats = DoTranslationStuff(opt.SourceFolder, assembly);

        if (opt.DataFile != null)
            DoDataFileStuff(opt.DataFile, assembly, translationStats);

        if (opt.FindDiscriminators)
            DoFindDiscriminators(opt.SourceFolder, assembly);

        return 0;
    }

    private static void DoFindDiscriminators(string sourcePath, Assembly assembly)
    {
        var handlerAttrType = assembly.GetType("Souvenir.HandlerAttribute");
        var discrAttrType = assembly.GetType("Souvenir.DiscriminatorAttribute");
        var noDiscrAttrType = assembly.GetType("Souvenir.NoDiscriminatorAttribute");
        foreach (var method in assembly.GetType("SouvenirModule").GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            dynamic handler = method.GetCustomAttribute(handlerAttrType);
            if (handler == null || method.GetCustomAttribute(noDiscrAttrType) != null)
                continue;
            var enumType = (Type) handler.EnumType;
            if (!enumType.GetFields(BindingFlags.Public | BindingFlags.Static).Any(f => f.GetCustomAttribute(discrAttrType) != null))
            {
                var sourceFile = Path.Combine(sourcePath, "Handlers", $"{enumType.Name.Substring(1)}.cs");
                var lineNumber = File.Exists(sourceFile) ? File.ReadLines(sourceFile).IndexOf(line => line.Contains("private IEnumerator<SouvenirInstruction>")) : -1;
                Console.WriteLine($"{sourceFile}({(lineNumber == -1 ? 1 : lineNumber + 1)},1): warning SOUV0001: {handler.ModuleName} has no discriminator.");
            }
        }
    }

    private static void DoManualQuestionsStuff(string folderpath, Assembly assembly)
    {
        var questions = new Dictionary<string, List<(object translationObject, bool the, string name, string translated, string id, List<string> questions)>>() { ["en"] = [] };

        var questionAttrType = assembly.GetType("Souvenir.ManualQuestionAttribute");
        var propQuestionText = questionAttrType.GetProperty("QuestionText", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);

        var translationType = assembly.GetType("Souvenir.ITranslation");
        var fldTranslateManualQuestions = translationType.GetProperty("TranslateManualQuestions", BindingFlags.Public | BindingFlags.Instance);
        var mthTranslateModule = translationType.GetMethod("TranslateModule", BindingFlags.Public | BindingFlags.Instance, null, [typeof(Type)], null);
        var mthManualQuestionSortBy = translationType.GetMethod("ManualQuestionSortBy", BindingFlags.Public | BindingFlags.Instance);

        var translationInfoType = assembly.GetType("Souvenir.TranslationInfo");
        var fldManualQuestions = translationInfoType.GetField("ManualQuestions", BindingFlags.Public | BindingFlags.Instance);
        var fldModuleName = translationInfoType.GetField("ModuleName", BindingFlags.Public | BindingFlags.Instance);
        var fldManualModuleName = translationInfoType.GetField("ManualModuleName", BindingFlags.Public | BindingFlags.Instance);
        var languages = translationInfoType.GetField("AllTranslations", BindingFlags.Static | BindingFlags.Public).GetValue(null) as IDictionary;

        var handlerAttrType = assembly.GetType("Souvenir.HandlerAttribute");
        var propModuleId = handlerAttrType.GetProperty("ModuleId", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propModuleName = handlerAttrType.GetProperty("ModuleName", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propEnumType = handlerAttrType.GetProperty("EnumType", BindingFlags.Public | BindingFlags.Instance, null, typeof(Type), [], null);
        var propAddThe = handlerAttrType.GetProperty("AddThe", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);

        foreach (DictionaryEntry language in languages)
            if ((bool) fldTranslateManualQuestions.GetValue(language.Value))
                questions[(string) language.Key] = [];

        foreach (var handlerMethod in assembly.GetType("SouvenirModule").GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var handlerAttr = handlerMethod.GetCustomAttribute(handlerAttrType);
            if (handlerAttr == null)
                continue;
            var manualQuestions = new List<string>();
            foreach (var questionAttr in handlerMethod.GetCustomAttributes(questionAttrType))
                manualQuestions.Add((string) propQuestionText.GetValue(questionAttr));
            questions["en"].Add((null, (bool) propAddThe.GetValue(handlerAttr), (string) propModuleName.GetValue(handlerAttr), null, (string) propModuleId.GetValue(handlerAttr), manualQuestions));
            foreach (DictionaryEntry language in languages)
                if ((bool) fldTranslateManualQuestions.GetValue(language.Value))
                {
                    var enumType = (Type) propEnumType.GetValue(handlerAttr);
                    var translatedHandler = mthTranslateModule.Invoke(language.Value, [enumType]);
                    var translatedManualQuestions = translatedHandler.NullOr(th => (Dictionary<string, string>) fldManualQuestions.GetValue(th));
                    var translatedModuleName = translatedHandler.NullOr(th => (string) fldManualModuleName.GetValue(th) ?? (string) fldModuleName.GetValue(th));
                    questions[(string) language.Key].Add((
                        translatedHandler,
                        (bool) propAddThe.GetValue(handlerAttr),
                        (string) propModuleName.GetValue(handlerAttr),
                        translatedModuleName,
                        (string) propModuleId.GetValue(handlerAttr),
                        manualQuestions.Select(mq => translatedManualQuestions?.Get(mq, null) ?? mq).ToList()));
                }
        }

        foreach (var (lang, qs) in questions)
        {
            File.WriteAllText(Path.Combine(folderpath, $"SouvenirData{(lang == "en" ? "" : $".{lang}")}.js"), $"""
                let SouvenirModuleData = [
                {qs.OrderBy(tup => lang == "en" ? tup.name : mthManualQuestionSortBy.Invoke(languages[lang], [tup.translationObject, tup.name])).Select(tup =>
                    {
                        var englishName = (tup.the ? "The " : "") + tup.name.Replace("'", "’");
                        return $"\t{{ {(lang == "en" ? "" : $"original: {englishName.JsEscape()},\t")}name: {(tup.translated ?? englishName).JsEscape()},\tid: {tup.id.JsEscape()},\tquestions: [{tup.questions.Select(q => q.JsEscape()).JoinString(", ")}] }},";
                    }).JoinString("\n")}
                ];
                """);
        }
    }

    private static void DoContributorStuff(string filepath, Assembly assembly)
    {
        var handlerAttrType = assembly.GetType("Souvenir.HandlerAttribute");
        var propContributor = handlerAttrType.GetProperty("Contributor", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propModuleName = handlerAttrType.GetProperty("ModuleName", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propAddThe = handlerAttrType.GetProperty("AddThe", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);

        var contributorToModules = new Dictionary<string, List<string>>();
        foreach (var attr in assembly.GetType("SouvenirModule").GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Select(m => m.GetCustomAttribute(handlerAttrType)).ToArray())
            if (attr != null)
            {
                var m = (string) propModuleName.GetValue(attr);
                contributorToModules.AddSafe((string) propContributor.GetValue(attr), (bool) propAddThe.GetValue(attr) ? $"{m}, The" : m);
            }

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
        var manualQuestionAttrType = assembly.GetType("Souvenir.ManualQuestionAttribute");
        var propManualQuestionText = manualQuestionAttrType.GetProperty("QuestionText", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);

        var handlerAttrType = assembly.GetType("Souvenir.HandlerAttribute");
        var propModuleId = handlerAttrType.GetProperty("ModuleId", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propModuleName = handlerAttrType.GetProperty("ModuleName", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propEnumType = handlerAttrType.GetProperty("EnumType", BindingFlags.Public | BindingFlags.Instance, null, typeof(Type), [], null);
        var propAddThe = handlerAttrType.GetProperty("AddThe", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);

        var questionAttrType = assembly.GetType("Souvenir.QuestionAttribute");
        var propQQuestionText = questionAttrType.GetProperty("QuestionText", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propQAllAnswers = questionAttrType.GetProperty("AllAnswers", BindingFlags.Public | BindingFlags.Instance, null, typeof(string[]), [], null);
        var propQArguments = questionAttrType.GetProperty("Arguments", BindingFlags.Public | BindingFlags.Instance, null, typeof(string[]), [], null);
        var propQArgumentGroupSize = questionAttrType.GetProperty("ArgumentGroupSize", BindingFlags.Public | BindingFlags.Instance, null, typeof(int), [], null);
        var propQTranslateAnswers = questionAttrType.GetProperty("TranslateAnswers", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);
        var propQTranslateArguments = questionAttrType.GetProperty("TranslateArguments", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool[]), [], null);
        var propQTranslatableStrings = questionAttrType.GetProperty("TranslatableStrings", BindingFlags.Public | BindingFlags.Instance, null, typeof(string[]), [], null);
        var propQReferenceDocumentation = questionAttrType.GetProperty("ReferenceDocumentation", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);
        var propQUsesQuestionSprite = questionAttrType.GetProperty("UsesQuestionSprite", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);
        var propQIsEntireQuestionSprite = questionAttrType.GetProperty("IsEntireQuestionSprite", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);

        var discrAttrType = assembly.GetType("Souvenir.DiscriminatorAttribute");
        var propDDiscriminatorText = discrAttrType.GetProperty("DiscriminatorText", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);
        var propDArguments = discrAttrType.GetProperty("Arguments", BindingFlags.Public | BindingFlags.Instance, null, typeof(string[]), [], null);
        var propDArgumentGroupSize = discrAttrType.GetProperty("ArgumentGroupSize", BindingFlags.Public | BindingFlags.Instance, null, typeof(int), [], null);
        var propDTranslateArguments = discrAttrType.GetProperty("TranslateArguments", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool[]), [], null);
        var propDTranslatableStrings = discrAttrType.GetProperty("TranslatableStrings", BindingFlags.Public | BindingFlags.Instance, null, typeof(string[]), [], null);
        var propDReferenceDocumentation = discrAttrType.GetProperty("ReferenceDocumentation", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);

        var removeAttrType = assembly.GetType("Souvenir.RemoveAttribute");

        var translationInfoType = assembly.GetType("Souvenir.TranslationInfo");
        var languages = (translationInfoType.GetField("AllTranslations", BindingFlags.Static | BindingFlags.Public).GetValue(null) as IDictionary).Keys.Cast<string>().ToArray();
        var fldModuleName = translationInfoType.GetField("ModuleName", BindingFlags.Public | BindingFlags.Instance);
        var fldNeedsTranslation = translationInfoType.GetField("NeedsTranslation", BindingFlags.Public | BindingFlags.Instance);
        var fldManualQuestions = translationInfoType.GetField("ManualQuestions", BindingFlags.Public | BindingFlags.Instance);

        var iTranslation = assembly.GetType("Souvenir.ITranslation");
        var mthTranslateModule = iTranslation.GetMethod("TranslateModule", BindingFlags.Public | BindingFlags.Instance, null, [typeof(Type)], null);
        var mthTranslateQuestion = iTranslation.GetMethod("TranslateQuestion", BindingFlags.Public | BindingFlags.Instance, null, [typeof(Enum)], null);
        var mthTranslateDiscriminator = iTranslation.GetMethod("TranslateDiscriminator", BindingFlags.Public | BindingFlags.Instance, null, [typeof(Enum)], null);
        var propTranslateManualQuestions = iTranslation.GetProperty("TranslateManualQuestions", BindingFlags.Public | BindingFlags.Instance, null, typeof(bool), [], null);

        var questionTranslationInfoType = assembly.GetType("Souvenir.QuestionTranslationInfo");
        var fldQuestion = questionTranslationInfoType.GetField("Question", BindingFlags.Public | BindingFlags.Instance);
        var fldAnswers = questionTranslationInfoType.GetField("Answers", BindingFlags.Public | BindingFlags.Instance);
        var fldArguments = questionTranslationInfoType.GetField("Arguments", BindingFlags.Public | BindingFlags.Instance);
        var fldAdditional = questionTranslationInfoType.GetField("Additional", BindingFlags.Public | BindingFlags.Instance);

        var discriminatorTranslationInfoType = assembly.GetType("Souvenir.DiscriminatorTranslationInfo");
        var fldDiscriminator = discriminatorTranslationInfoType.GetField("Discriminator", BindingFlags.Public | BindingFlags.Instance);
        var fldDArguments = discriminatorTranslationInfoType.GetField("Arguments", BindingFlags.Public | BindingFlags.Instance);
        var fldDAdditional = discriminatorTranslationInfoType.GetField("Additional", BindingFlags.Public | BindingFlags.Instance);

        var allInfos = new Dictionary<string, List<(FieldInfo fld, Attribute attr)>>();
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
            var customTranslationInfoType = baseType.GetGenericArguments()[0];
            var translationInfoPrototype = Activator.CreateInstance(customTranslationInfoType);
            var tiFields = customTranslationInfoType.GetFields().ToArray();

            var already = alreadyType == null ? null : Activator.CreateInstance(alreadyType);
            var outerSb = new StringBuilder();
            var translatedCount = 0;
            var totalCount = 0;

            object questionTranslationInfoPrototype = null;
            FieldInfo[] questionTranslationAdditionalFields = null;

            // for each module handler...
            foreach (var handlerMethod in assembly.GetType("SouvenirModule").GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).OrderBy(m => m.Name))
            {
                var handler = handlerMethod.GetCustomAttribute(handlerAttrType);
                if (handler == null)
                    continue;

                var enumType = (Type) propEnumType.GetValue(handler);
                var alreadyHandler = already == null ? null : mthTranslateModule.Invoke(already, [enumType]);

                outerSb.AppendLine($"{indent()}// {((bool) propAddThe.GetValue(handler) ? "The " : "")}{propModuleName.GetValue(handler)}");
                outerSb.AppendLine($"{indent()}[typeof({enumType.Name})] = new()");
                outerSb.AppendLine($"{indent()}{{");
                indentation += 4;

                var sb = new StringBuilder();
                var prospectiveModuleName = alreadyHandler == null ? null : (string) fldModuleName.GetValue(alreadyHandler);
                if (prospectiveModuleName != null && prospectiveModuleName != (string) propModuleName.GetValue(handler))
                    sb.AppendLine($@"{indent()}ModuleName = ""{prospectiveModuleName.CLiteralEscape()}"",");

                var needsTranslation = already == null || alreadyHandler == null || (bool) fldNeedsTranslation.GetValue(alreadyHandler);
                var skip = "NeedsTranslation,ModuleName,Questions,Discriminators,ManualQuestions".Split(',');
                if (alreadyHandler != null)
                    foreach (var f in tiFields)
                        if (!skip.Contains(f.Name) && f.GetValue(alreadyHandler) is object v && !v.Equals(f.GetValue(translationInfoPrototype)))
                            sb.AppendLine($@"{indent()}{f.Name} = {(
                                f.FieldType == typeof(string) ? $@"""{((string) v).CLiteralEscape()}""" :
                                f.FieldType.IsEnum ? $@"{f.FieldType.Name}.{v}" :
                                throw new NotImplementedException()
                            )},");

                if ((bool) propTranslateManualQuestions.GetValue(already))
                {
                    var manualQuestions = new List<string>();
                    foreach (var questionAttr in handlerMethod.GetCustomAttributes(manualQuestionAttrType))
                        manualQuestions.Add((string) propManualQuestionText.GetValue(questionAttr));
                    var alreadyManualQuestions = alreadyHandler == null ? null : (Dictionary<string, string>) fldManualQuestions.GetValue(alreadyHandler);
                    if (manualQuestions.Count > 0)
                    {
                        sb.AppendLine($"{indent()}ManualQuestions = new()");
                        sb.AppendLine($"{indent()}{{");
                        indentation += 4;
                        foreach (var q in manualQuestions)
                        {
                            var alreadyQ = alreadyManualQuestions?.Get(q, null);
                            sb.AppendLine($"{indent()}[\"{q.CLiteralEscape()}\"] = \"{(alreadyQ ?? q).CLiteralEscape()}\",");
                            if (alreadyQ == null)
                                needsTranslation = true;
                        }
                        indentation -= 4;
                        sb.AppendLine($"{indent()}}},");
                    }
                }

                var questionsAndDiscriminators = enumType.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(f => f.GetCustomAttribute(removeAttrType) == null)
                    .Select(f => (enumValue: f.GetValue(null), qAttr: f.GetCustomAttribute(questionAttrType), dAttr: f.GetCustomAttribute(discrAttrType)))
                    .ToArray();

                sb.AppendLine($"{indent()}Questions = new()");
                sb.AppendLine($"{indent()}{{");
                indentation += 4;

                void AddDictionary(string name, IEnumerable<string> original, Dictionary<string, string> already)
                {
                    sb.AppendLine($"{indent()}{name} = new()");
                    sb.AppendLine($"{indent()}{{");
                    indentation += 4;
                    foreach (var str in original)
                        sb.AppendLine($@"{indent()}[""{str.CLiteralEscape()}""] = ""{(already?.Get(str, null) ?? str)
                            .Apply(str => str.IndexOf('\uE003') is int p and not -1 ? str.Substring(0, p) : str)
                            .CLiteralEscape()}"",");
                    indentation -= 4;
                    sb.AppendLine($"{indent()}}},");

                    if (already == null || original.Any(str => !already.ContainsKey(str)))
                        needsTranslation = true;
                }

                // for each QUESTION...
                foreach (var (enumValue, qAttr, _) in questionsAndDiscriminators.Where(tup => tup.qAttr != null))
                {
                    var alreadyQuestion = already == null ? null : mthTranslateQuestion.Invoke(already, [enumValue]);
                    var alreadyQuestionText = alreadyQuestion == null ? null : (string) fldQuestion.GetValue(alreadyQuestion);
                    if (alreadyQuestion == null || alreadyQuestionText == null)
                        needsTranslation = true;

                    sb.AppendLine($"{indent()}[{enumValue.GetType().Name}.{enumValue}] = new()");
                    sb.AppendLine($"{indent()}{{");
                    indentation += 4;

                    if ((bool) propQIsEntireQuestionSprite.GetValue(qAttr))
                        sb.AppendLine($"{indent()}// This question is depicted visually, rather than with words. The translation here will only be used for logging.");
                    else
                    {
                        var extra = (bool) propQUsesQuestionSprite.GetValue(qAttr) ? $@" (+ sprite)" : "";
                        sb.AppendLine($@"{indent()}// English: {propQQuestionText.GetValue(qAttr)}{extra}");
                        if (propQArguments.GetValue(qAttr) is string[] args)
                            sb.AppendLine($@"{indent()}// Example: {string.Format((string) propQQuestionText.GetValue(qAttr), [((bool) propAddThe.GetValue(handler) ? "The " : "") + propModuleName.GetValue(handler), .. processString(args.Take((int) propQArgumentGroupSize.GetValue(qAttr)))])}{extra}");
                    }
                    sb.AppendLine($@"{indent()}Question = ""{(alreadyQuestionText ?? (string) propQQuestionText.GetValue(qAttr)).CLiteralEscape()}"",");

                    if (alreadyQuestion != null)
                    {
                        var questionType = alreadyQuestion.GetType();
                        questionTranslationInfoPrototype ??= Activator.CreateInstance(questionType);
                        questionTranslationAdditionalFields ??= questionType.GetFields(BindingFlags.Instance | BindingFlags.Public)
                            .Where(field => !(field.Name is "Question" or "Answers" or "Arguments" or "Additional"))
                            .ToArray();

                        foreach (var field in questionTranslationAdditionalFields)
                        {
                            var value = field.GetValue(alreadyQuestion);
                            if (!Equals(value, field.GetValue(questionTranslationInfoPrototype)))
                                sb.AppendLine($@"{indent()}{field.Name} = {(
                                    value is string v ? $@"""{v.CLiteralEscape()}""" :
                                    value is Enum e ? $@"{e.GetType().Name}.{e}" :
                                    value is int i ? i.ToString() :
                                    throw new InvalidOperationException($"Unsupported field type: {value.GetType().FullName}"))},");
                        }
                    }

                    if ((bool) propQReferenceDocumentation.GetValue(qAttr) || propQTranslatableStrings.GetValue(qAttr) is string[] { Length: > 0 })
                        sb.AppendLine($@"{indent()}// Refer to translations.md to understand the weird strings");

                    if (propQArguments.GetValue(qAttr) is string[] { Length: > 0 } origArguments && propQArgumentGroupSize.GetValue(qAttr) is int groupSize && propQTranslateArguments.GetValue(qAttr) is bool[] trArgs)
                        AddDictionary("Arguments", Enumerable.Range(0, groupSize).Where(ix => trArgs[ix]).SelectMany(ix => Enumerable.Range(0, origArguments.Length / groupSize).Select(jx => origArguments[jx * groupSize + ix])).Distinct(), alreadyQuestion == null ? null : (Dictionary<string, string>) fldArguments.GetValue(alreadyQuestion));
                    if (propQAllAnswers.GetValue(qAttr) is string[] { Length: > 0 } origAnswers && (bool) propQTranslateAnswers.GetValue(qAttr))
                        AddDictionary("Answers", origAnswers.Distinct(), alreadyQuestion == null ? null : (Dictionary<string, string>) fldAnswers.GetValue(alreadyQuestion));
                    if (propQTranslatableStrings.GetValue(qAttr) is string[] { Length: > 0 } origStrings)
                        AddDictionary("Additional", origStrings.Distinct(), alreadyQuestion == null ? null : (Dictionary<string, string>) fldAdditional.GetValue(alreadyQuestion));

                    indentation -= 4;
                    sb.AppendLine($"{indent()}}},");
                }

                indentation -= 4;
                sb.AppendLine($"{indent()}}},");

                if (questionsAndDiscriminators.Where(tup => tup.dAttr != null).Any())
                {
                    sb.AppendLine($"{indent()}Discriminators = new()");
                    sb.AppendLine($"{indent()}{{");
                    indentation += 4;

                    // for each DISCRIMINATOR...
                    foreach (var (enumValue, _, dAttr) in questionsAndDiscriminators.Where(tup => tup.dAttr != null))
                    {
                        var alreadyDiscriminator = already == null ? null : mthTranslateDiscriminator.Invoke(already, [enumValue]);
                        var alreadyDiscriminatorText = alreadyDiscriminator == null ? null : (string) fldDiscriminator.GetValue(alreadyDiscriminator);
                        if (alreadyDiscriminator == null || alreadyDiscriminatorText == null)
                            needsTranslation = true;

                        sb.AppendLine($"{indent()}[{enumValue.GetType().Name}.{enumValue}] = new()");
                        sb.AppendLine($"{indent()}{{");
                        indentation += 4;

                        sb.AppendLine($@"{indent()}// English: {propDDiscriminatorText.GetValue(dAttr)}");
                        if (propDArguments.GetValue(dAttr) is string[] args)
                            sb.AppendLine($@"{indent()}// Example: {string.Format((string) propDDiscriminatorText.GetValue(dAttr), processString(args.Take((int) propDArgumentGroupSize.GetValue(dAttr))).ToArray())}");
                        sb.AppendLine($@"{indent()}Discriminator = ""{((fldDiscriminator == null ? null : (string) fldDiscriminator.GetValue(alreadyDiscriminator)) ?? (string) propDDiscriminatorText.GetValue(dAttr)).CLiteralEscape()}"",");

                        if ((bool) propDReferenceDocumentation.GetValue(dAttr) || propDTranslatableStrings.GetValue(dAttr) is string[] { Length: > 0 })
                            sb.AppendLine($@"{indent()}// Refer to translations.md to understand the weird strings");

                        if (propDArguments.GetValue(dAttr) is string[] { Length: > 0 } origArguments && propDArgumentGroupSize.GetValue(dAttr) is int groupSize && propDTranslateArguments.GetValue(dAttr) is bool[] trArgs)
                            AddDictionary("Arguments", Enumerable.Range(0, groupSize).Where(ix => trArgs[ix]).SelectMany(ix => Enumerable.Range(0, origArguments.Length / groupSize).Select(jx => origArguments[jx * groupSize + ix])).Distinct(), alreadyDiscriminator == null ? null : (Dictionary<string, string>) fldDArguments.GetValue(alreadyDiscriminator));

                        if (propDTranslatableStrings.GetValue(dAttr) is string[] { Length: > 0 } origStrings)
                            AddDictionary("Additional", origStrings.Distinct(), alreadyDiscriminator == null ? null : (Dictionary<string, string>) fldDAdditional.GetValue(alreadyDiscriminator));

                        indentation -= 4;
                        sb.AppendLine($"{indent()}}},");
                    }
                    indentation -= 4;
                    sb.AppendLine($"{indent()}}},");
                }

                if (needsTranslation)
                    outerSb.AppendLine($"{indent()}NeedsTranslation = true,");
                outerSb.Append(sb);

                indentation -= 4;
                outerSb.AppendLine($"{indent()}}},");
                outerSb.AppendLine();

                totalCount++;
                if (!needsTranslation)
                    translatedCount++;
            }

            translationStats[language] = $"{((float) translatedCount / totalCount * 100f).ToString("f2", CultureInfo.InvariantCulture)}%";
            File.WriteAllText(path, $"{alreadyFile.Take(p1 + 1).JoinString(Environment.NewLine)}{Environment.NewLine}{outerSb}{alreadyFile.Skip(p2).JoinString(Environment.NewLine)}");
        }
        return translationStats;
    }

    private static IEnumerable<string> processString(IEnumerable<string> source) => source.Select(str => str == "\uE047ordinal" ? "first" : str.IndexOf('\uE003') is int p and not -1 ? str.Substring(0, p) : str);

    private static void DoDataFileStuff(string path, Assembly assembly, Dictionary<string, string> translationStats)
    {
        var moduleType = assembly.GetType("SouvenirModule");
        var languages = (assembly.GetType("Souvenir.TranslationInfo").GetField("AllTranslations", BindingFlags.Static | BindingFlags.Public).GetValue(null) as IDictionary).Keys.Cast<string>().ToArray();
        var questionAttrType = assembly.GetType("Souvenir.QuestionAttribute");
        var discrAttrType = assembly.GetType("Souvenir.DiscriminatorAttribute");

        var handlerAttrType = assembly.GetType("Souvenir.HandlerAttribute");
        var propEnumType = handlerAttrType.GetProperty("EnumType", BindingFlags.Public | BindingFlags.Instance, null, typeof(Type), [], null);
        var propContributor = handlerAttrType.GetProperty("Contributor", BindingFlags.Public | BindingFlags.Instance, null, typeof(string), [], null);

        var modules = 0;
        var questions = 0;
        var discriminators = 0;
        var contributors = new HashSet<string>();

        foreach (var method in moduleType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var handler = method.GetCustomAttribute(handlerAttrType);
            if (handler == null)
                continue;
            modules++;
            contributors.Add((string) propContributor.GetValue(handler));

            var enumType = (Type) propEnumType.GetValue(handler);
            foreach (var value in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (value.GetCustomAttribute(questionAttrType) != null)
                    questions++;
                if (value.GetCustomAttribute(discrAttrType) != null)
                    discriminators++;
            }
        }

        translationStats ??= ClassifyJson.Deserialize<Dictionary<string, string>>(JsonDict.Parse(File.ReadAllText(path))["translationProgress"]);

        File.WriteAllText(path, $$"""
        {
            "contributorsCount": {{contributors.Count}},
            "modulesCount": {{modules}},
            "questionsCount": {{questions}},
            "discriminatorsCount": {{discriminators}},
            "translationProgress": { {{languages.Select(lang => $@"""{lang}"": ""{translationStats.Get(lang, "0%")}""").JoinString(", ")}} }
        }
        """);
    }

    /// <summary>
    ///     Escapes all characters in this string whose code is less than 32 or form invalid UTF-16 using C/C#-compatible
    ///     backslash escapes.</summary>
    private static string CLiteralEscape(this string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

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
