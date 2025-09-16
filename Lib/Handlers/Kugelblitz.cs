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
}

public partial class SouvenirModule
{
    [SouvenirHandler("kugelblitz", "Kugelblitz", typeof(SKugelblitz), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKugelblitz(ModuleData module)
    {
        module.SolveIndex = 1;

        while (!_isActivated)
            yield return null;

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
        {
            askedQuirks = _kugelblitzUsedQuirks[lobby] = new();
            _kugelblitzQuirksGroupings.Add(usedQuirks);
        }

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

        yield return indices.Skip(1).Any(x => x is not null && x != indices[0])
            ? throw new AbandonModuleException("Two quirks disagreed on how many stages were shown.")
            : null;

        string constructStandardAnswer(int b) => string.Format(
            translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, "{0}{1}{2}{3}{4}{5}{6}"),
            Enumerable.Range(0, 7).Select(i => (b & (1 << i)) != 0 ? translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, "ROYGBIV"[i].ToString()) : "").ToArray());

        string constructRGBAnswer(byte[] b) => string.Format(
            translateString(SKugelblitz.RedGreenBlue, "R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"),
            b[0], b[1], b[2], b[3], b[4], b[5], b[6]);

        IEnumerable<string> allRGBAnswers(int max)
        {
            var options = new HashSet<string>();
            while (options.Count < 6)
                options.Add(constructRGBAnswer(Ut.NewArray(7, i => (byte) UnityEngine.Random.Range(0, max + 1))));
            return options;
        }

        var allStandardAnswers = Enumerable.Range(0, 128).Select(constructStandardAnswer).ToArray();
        allStandardAnswers[0] = translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, "None");

        var quirkNames = new[] { "black", "red", "orange", "yellow", "green", "blue", "indigo", "violet" };
        string formatName(int color) => string.Format(translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, "the {0} Kugelblitz"), translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, quirkNames[color]));

        var templates = Ut.NewArray(
            "the Kugelblitz linked with no other Kugelblitzes",
            "the {0} Kugelblitz linked with one other Kugelblitz",
            "the {0} Kugelblitz linked with two other Kugelblitzes",
            "the {0} Kugelblitz linked with three other Kugelblitzes",
            "the {0} Kugelblitz linked with four other Kugelblitzes",
            "the {0} Kugelblitz linked with five other Kugelblitzes",
            "the {0} Kugelblitz linked with six other Kugelblitzes",
            "the {0} Kugelblitz linked with seven other Kugelblitzes");
        string formatNameSized(int color, int size) => string.Format(translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, templates[size - 1]), translateString(SKugelblitz.BlackOrangeYellowIndigoViolet, quirkNames[color]));

        bool myFormat(int color, out string format)
        {
            if (color == 0 && _kugelblitzQuirksGroupings.Count == 1 && _kugelblitzQuirksGroupings[0].Count == 1)
                format = null;
            else if (_kugelblitzQuirksGroupings.Count(g => g.Contains(color)) == 1)
                format = formatName(color);
            else if (_kugelblitzQuirksGroupings.Count(g => g.Contains(color) && g.Count == linkSize) == 1)
                format = formatNameSized(color, linkSize);
            else
            {
                legitimatelyNoQuestion(module, $"There are multiple lobbies with {linkSize} kugelblitzes and a(n) {quirkNames[color]} one, so I can’t ask about them.");
                format = null;
                return false;
            }
            return true;
        }

        var finalQuirk = usedQuirks.FirstOrNull(q => !askedQuirks.Contains(q)) ?? throw new AbandonModuleException("I somehow ran out of quirks.");
        askedQuirks.Add(finalQuirk);
        var qp = Array.IndexOf(normalQuirks, finalQuirk);
        var isNormal = qp != -1;
        var answers = isNormal
            ? KOYIV[qp].Select(b => allStandardAnswers[b])
            : RGB[Array.IndexOf(abnormalQuirks, finalQuirk)].Select(constructRGBAnswer);
        if (!myFormat(finalQuirk, out var format))
            yield break;
        for (var i = 0; i < answers.Length; i++)
            yield return question(isNormal ? SKugelblitz.BlackOrangeYellowIndigoViolet : SKugelblitz.RedGreenBlue, args: [Ordinal(i + 1)]).Answers(answers[i], all: isNormal ? allStandardAnswers : null, preferredWrong: isNormal ? null : allRGBAnswers(finalQuirk == 5 ? 2 : 6).ToArray());
    }
}
