using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SKugelblitz
{
    [SouvenirQuestion("Which particles were present for the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, ExampleAnswers = ["None", "RGB", "RYV", "ROYGBIV", "YIV", "O"])]
    BlackOrangeYellowIndigoViolet,

    [SouvenirQuestion("What were the particles’ values for the {1} stage of {0}?", OneColumn4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, ExampleAnswers = ["R=0, O=0, Y=0, G=0, B=0, I=0, V=0", "R=1, O=0, Y=2, G=3, B=4, I=1, V=6", "R=1, O=0, Y=1, G=1, B=1, I=1, V=0", "R=6, O=5, Y=2, G=4, B=3, I=1, V=2"], TranslatableStrings = ["R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"])]
    RedGreenBlue,

    [SouvenirDiscriminator("the {0} Kugelblitz", Arguments = ["black", "red", "orange", "yellow", "green", "blue", "indigo", "violet"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Color,

    [SouvenirDiscriminator("the Kugelblitz linked with no other Kugelblitzes")]
    NoLinks,

    [SouvenirDiscriminator("the {0} Kugelblitz linked with {1}", Arguments = ["black", "one other Kugelblitz", "red", "two other Kugelblitzes", "orange", "three other Kugelblitzes", "yellow", "four other Kugelblitzes", "green", "five other Kugelblitzes", "blue", "six other Kugelblitzes", "indigo", "seven other Kugelblitzes", "violet", "seven other Kugelblitzes"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    Links
}

public partial class SouvenirModule
{
    private readonly Dictionary<object, HashSet<int>> _kugelblitzUsedQuirks = [];

    [SouvenirHandler("kugelblitz", "Kugelblitz", typeof(SKugelblitz), "Anonymous", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessKugelblitz(ModuleData module)
    {
        yield return WaitForActivate;

        yield return null;
        yield return null; // The module takes this long to subscribe to a lobby

        if (module.IsSolved)
            yield return legitimatelyNoQuestion(module, "The module had too few stages to generate and autosolved.");

        yield return WaitForUnignoredModules;

        var comp = GetComponent(module, "KugelblitzScript");
        var lobby = GetField<object>(comp, "_lobby").Get();
        var linkSize = GetField<IList>(lobby, "_members").Get().Count;
        var quirks = GetField<IList>(lobby, "_quirks").Get().Cast<object>().ToArray();

        var quirkTypes = new[] { "BaseStageManager", "OffsetStageManager", "InvertStageManager", "InsertStageManager", "LengthStageManager", "TurnStageManager", "FlipStageManager", "WrapStageManager" };
        var orderedQuirks = Enumerable.Range(0, 8).Select(qId => quirks.SingleOrDefault(quirk => quirk.GetType().Name == quirkTypes[qId])).ToArray();
        var usedQuirks = new HashSet<int>(Enumerable.Range(0, 8).Where(qId => orderedQuirks[qId] != null));

        if (!_kugelblitzUsedQuirks.TryGetValue(lobby, out var askedQuirks))
            askedQuirks = _kugelblitzUsedQuirks[lobby] = [];

        var KOYIV = new List<byte>[5];
        var RGB = new List<byte[]>[3];
        var indices = new int?[8];

        if (!usedQuirks.Contains(0))
            throw new AbandonModuleException("There was no black Kugelblitz in the lobby.");

        var normalQuirks = new[] { 0, 2, 3, 6, 7 };
        var abnormalQuirks = new[] { 1, 4, 5 };

        for (var q = 0; q < normalQuirks.Length; q++)
        {
            if (usedQuirks.Contains(normalQuirks[q]))
            {
                var stageObjects = GetField<IList>(orderedQuirks[normalQuirks[q]], "_stages").Get().Cast<object>();
                var fldData = GetArrayField<bool>(stageObjects.First(), "_data");
                KOYIV[q] = stageObjects.Select(s => (byte) fldData.GetFrom(s, expectedLength: 7).Select((b, i) => b ? 1 << i : 0).Sum()).ToList();
                indices[normalQuirks[q]] = GetIntField(orderedQuirks[normalQuirks[q]], "_index").Get(min: 0);
            }
        }

        if (usedQuirks.Contains(1))
        {
            var stageObjects = GetField<IList>(orderedQuirks[1], "_stages").Get().Cast<object>();
            var fldData = GetArrayField<byte>(stageObjects.First(), "_data");
            RGB[0] = stageObjects.Select(s => fldData.GetFrom(s, expectedLength: 7, validator: b => b is > 6 ? "Expected red data to be [0, 6]" : null)).ToList();
            indices[1] = GetIntField(orderedQuirks[1], "_index").Get(min: 0);
        }
        if (usedQuirks.Contains(4))
        {
            var stageObjects = GetField<IList>(orderedQuirks[4], "_stages").Get().Cast<object>();
            var fldData = GetArrayField<byte>(stageObjects.First(), "_data");
            RGB[1] = stageObjects.Select(s => fldData.GetFrom(s, expectedLength: 7, validator: b => b is > 6 ? "Expected green data to be [0, 6]" : null)).ToList();
            indices[4] = GetIntField(orderedQuirks[4], "_index").Get(min: 0);
        }
        if (usedQuirks.Contains(5))
        {
            var stageObjects = GetField<IList>(orderedQuirks[5], "_stages").Get().Cast<object>();
            var fldData = GetArrayField<byte>(stageObjects.First(), "_data");
            RGB[2] = stageObjects.Select(s => fldData.GetFrom(s, expectedLength: 7, validator: b => b is > 2 ? "Expected blue data to be [0, 2]" : null)).ToList();
            indices[5] = GetIntField(orderedQuirks[5], "_index").Get(min: 0);
        }

        if (indices.Skip(1).Any(x => x is not null && x != indices[0]))
            throw new AbandonModuleException("Two quirks disagreed on how many stages were shown.");

        yield return null;

        string constructStandardAnswer(int b) => string.Format(
            TranslateQuestionString(SKugelblitz.BlackOrangeYellowIndigoViolet, "{0}{1}{2}{3}{4}{5}{6}"),
            Enumerable.Range(0, 7).Select(i => (b & (1 << i)) != 0 ? TranslateQuestionString(SKugelblitz.BlackOrangeYellowIndigoViolet, "ROYGBIV"[i].ToString()) : "").ToArray());

        string constructRGBAnswer(byte[] b) => string.Format(
            TranslateQuestionString(SKugelblitz.RedGreenBlue, "R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"),
            b[0], b[1], b[2], b[3], b[4], b[5], b[6]);

        IEnumerable<string> allRGBAnswers(int max)
        {
            var options = new HashSet<string>();
            while (options.Count < 6)
                options.Add(constructRGBAnswer(Ut.NewArray(7, i => (byte) UnityEngine.Random.Range(0, max + 1))));
            return options;
        }

        var allStandardAnswers = Enumerable.Range(0, 128).Select(constructStandardAnswer).ToArray();
        allStandardAnswers[0] = TranslateQuestionString(SKugelblitz.BlackOrangeYellowIndigoViolet, "None");

        var quirkNames = new[] { "black", "red", "orange", "yellow", "green", "blue", "indigo", "violet" };

        var templates = Ut.NewArray(
            "one other Kugelblitz",
            "two other Kugelblitzes",
            "three other Kugelblitzes",
            "four other Kugelblitzes",
            "five other Kugelblitzes",
            "six other Kugelblitzes",
            "seven other Kugelblitzes");

        var finalQuirk = usedQuirks.FirstOrNull(q => !askedQuirks.Contains(q)) ?? throw new AbandonModuleException("I somehow ran out of quirks.");
        askedQuirks.Add(finalQuirk);
        var qp = Array.IndexOf(normalQuirks, finalQuirk);
        var isNormal = qp != -1;
        var answers = isNormal
            ? KOYIV[qp].Select(b => allStandardAnswers[b]).ToArray()
            : RGB[Array.IndexOf(abnormalQuirks, finalQuirk)].Select(constructRGBAnswer).ToArray();

        for (var i = 0; i < answers.Length; i++)
            yield return question(isNormal ? SKugelblitz.BlackOrangeYellowIndigoViolet : SKugelblitz.RedGreenBlue, args: [Ordinal(i + 1)])
                .Answers(answers[i], all: isNormal ? allStandardAnswers : null, preferredWrong: isNormal ? null : allRGBAnswers(finalQuirk == 5 ? 2 : 6).ToArray());
        yield return new Discriminator(SKugelblitz.Color, "color", finalQuirk, [quirkNames[finalQuirk]]);
        yield return linkSize == 1
            ? new Discriminator(SKugelblitz.NoLinks, "nolink", true)
            : new Discriminator(SKugelblitz.Links, "colorlink", (finalQuirk, linkSize), args: [quirkNames[finalQuirk], templates[linkSize - 2]]) { Priority = 1 };
    }
}
