using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SBamboozlingButtonGrid
{
    [Question("What was shown on the {1} display in the {2} step of the cycle in the {3} stage in {0}?", OneColumn4Answers, Arguments = ["top", QandA.Ordinal, QandA.Ordinal, "right", QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, false, false])]
    [AnswerGenerator.BamboozlingButtonGrid]
    QLabel,
    [Discriminator("the Bamboozling Button Grid that showed {0} on the {1} display in the {2} stage", Arguments = ["MISSPELT NOVEMBAR", "top", QandA.Ordinal, "THE LETTER ROMYO", "right", QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [false, true, false])]
    DLabel,

    [Question("What was the color of the {1} display in the {2} step of the cycle in the {3} stage in {0}?", ThreeColumns6Answers, "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", Arguments = ["top", QandA.Ordinal, QandA.Ordinal, "right", QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, false, false])]
    QLabelColor,
    [Discriminator("the Bamboozling Button Grid where the {0} display was sometimes {1} in the {2} stage", Arguments = ["top", "white", QandA.Ordinal, "top", "red", QandA.Ordinal, "top", "orange", QandA.Ordinal, "top", "yellow", QandA.Ordinal, "top", "lime", QandA.Ordinal, "top", "green", QandA.Ordinal, "top", "jade", QandA.Ordinal, "right", "grey", QandA.Ordinal, "right", "cyan", QandA.Ordinal, "right", "azure", QandA.Ordinal, "right", "blue", QandA.Ordinal, "right", "violet", QandA.Ordinal, "right", "magenta", QandA.Ordinal, "right", "rose", QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, true, false])]
    DLabelColor
}

namespace Souvenir
{
    static partial class AnswerGenerator
    {
        public class BamboozlingButtonGrid : AnswerGeneratorAttribute<string>
        {
            public static readonly string[] _prepend = ["", "THE LETTER ", "THE WORD ", "MISSPELT "];
            public static readonly string[] _alph = [
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "FOXTROT", "GOLF", "HOTEL", "INDIA", "JULIET", "KILO", "LIMA", "MIKE", "NOVEMBER", "OSCAR", "PAPA", "QUEBEC", "ROMEO", "SIERRA", "TANGO", "UNIFORM", "VICTOR", "WHISKEY", "XRAY", "YANKEE", "ZULU",
                "ALFHA", "BRAVOH", "CHARLEE", "DELLTA", "EKO", "FOKSTROT", "GOLPH", "HOATEL", "INDYA", "JOOLIET", "KEYLO", "LEEMA", "MYKE", "NOBEMVER", "OSCAH", "PAPPA", "QEUBEC", "ROMIO", "SCIERA", "TANKO", "YUNIFORM", "VICKTOR", "WISKEY", "EKSRAY", "YANKIE", "ZOOLU", "ALPA", "BRAVOE", "CHARLY", "DELTER", "ECOH", "FOXCHROT", "GOLLF", "HOTELL", "INDEA", "JULYET", "KEELO", "LEYMA", "MAIK", "NOVEBMER", "OZCAR", "PAPHA", "QUEBECK", "REOMO", "SIARRA", "TONGO", "UNIPHORM", "VICTAH", "WHISKEE", "XRAE", "YANGKEE", "ZULOO", "ALEPH", "BRAHVO", "CHAALIE", "TELTA", "ECCO", "FOXTROY", "COLF", "OHTEL", "INDIR", "GULIET", "CHILO", "LEMA", "MIC", "NOVEMBAR", "OSKAR", "PAHPAH", "CUEBEC", "ROMYO", "SEERRA", "DANGO", "UNUFORM", "VITCOR", "WHISCEY", "XREI", "YANKY", "ZULLU", "ALPHER", "BRUHVO", "SHARLIE", "OELTA", "EGHO", "POXTROT", "EOLF", "HOTLE", "NDIA", "JULLIET", "QUILO", "LIA", "MIXE", "NOVENBER", "OSSCAR", "FAFA", "QUEBEQ", "RHOMEO", "SYERRA", "TNGO", "UNIFROM", "VICTUR", "VVHISKEY", "XARRAY", "IANKEE", "ZUWU", "AELLFHAH", "BERRARVO", "TCHAALEY", "DERLEDEH", "EQUEAUX", "PHAWXTRT", "GAULTH", "HOUGHTEL", "YNNDEYER", "DGEULIRT", "QUEELOWE", "LEIGHMUR", "MISQUE", "NOUGHVMB", "AUSQUE", "PARGHPEH", "KWUBBECH", "ROAMEEOH", "CYERHA", "TAENGKOW", "EUNIFAWM", "VYCHTOUR", "UISSQUAY", "ECHSREIH", "EANGQUI", "ZOUGHLEW"
            ];

            public override IEnumerable<string> GetAnswers(SouvenirModule module)
            {
                while (true)
                    yield return _prepend[Rnd.Range(0, _prepend.Length)] + _alph[Rnd.Range(0, _alph.Length)];
            }

            public override int Count => _prepend.Length * _alph.Length;
        }
    }
}

public partial class SouvenirModule
{
    [Handler("bamboozlingButtonGrid", "Bamboozling Button Grid", typeof(SBamboozlingButtonGrid), "Timwi")]
    [ManualQuestion("What was on the displays and in what color in each step of the cycle in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessBamboozlingButtonGrid(ModuleData module)
    {
        var comp = GetComponent(module, "BamGScript");
        var fldStageCount = GetIntField(comp, "stageCount");
        var statusLightButton = GetListField<KMSelectable>(comp, "buttons", isPublic: true).Get(expectedLength: 17)[16];

        var prevStage = -1;
        statusLightButton.OnInteract += delegate { prevStage = -1; return false; };
        module.Module.OnStrike += delegate { prevStage = -1; return false; };

        var dispTexts = new string[4][][];
        var dispColors = new int[4][][];

        while (module.Unsolved)
        {
            var curStage = fldStageCount.Get();
            if (curStage != prevStage)
            {
                dispTexts[curStage] = GetArrayField<string[]>(comp, "disptext")
                    .Get(expectedLength: 2, validator: v => v.Length != 4 ? "expected length 4" : null)
                    .ToArray();

                dispColors[curStage] = GetArrayField<int[][]>(comp, "textrand")
                    .Get(expectedLength: 2, validator: v => v.Length != 4 ? "expected length 4" : v.Any(w => w.Length != 4) ? "expected inner length 4" : v.Any(w => w[3] is < 0 or > 13) ? "expected textrand[j][i][3] values 0–13" : null)
                    .Select(outer => outer.Select(inner => inner[3]).ToArray())
                    .ToArray();

                Debug.Log($"♦ stage {curStage} dispTexts: {dispTexts.Stringify()}");
                Debug.Log($"♠ stage {curStage} dispColors: {dispColors.Stringify()}");

                prevStage = curStage;
            }
            yield return null;
        }

        var dispNames = new[] { "top", "right" };
        var dispColorNames = new[] { "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose" };
        var buttonColorNames = new Dictionary<string, string> { ["B"] = "Blue", ["G"] = "Green", ["R"] = "Red", ["Y"] = "Yellow" };

        for (var stage = 0; stage < 4; stage++)
            for (var disp = 0; disp < 2; disp++)
            {
                for (var cycle = 0; cycle < 4; cycle++)
                {
                    yield return question(SBamboozlingButtonGrid.QLabel, args: [dispNames[disp], Ordinal(cycle + 1), Ordinal(stage + 1)])
                        .AvoidDiscriminators(SBamboozlingButtonGrid.DLabel)
                        .Answers(dispTexts[stage][disp][cycle]);
                    yield return question(SBamboozlingButtonGrid.QLabelColor, args: [dispNames[disp], Ordinal(cycle + 1), Ordinal(stage + 1)])
                        .AvoidDiscriminators(SBamboozlingButtonGrid.DLabelColor)
                        .Answers(dispColorNames[dispColors[stage][disp][cycle]]);
                }
                foreach (var distinctLabel in dispTexts[stage][disp].Distinct())
                    yield return new Discriminator(SBamboozlingButtonGrid.DLabel, $"label-{disp}-{stage}-{distinctLabel}", args: [distinctLabel, dispNames[disp], Ordinal(stage + 1)]);
                foreach (var distinctColor in dispColors[stage][disp].Distinct())
                    yield return new Discriminator(SBamboozlingButtonGrid.DLabelColor, $"color-{disp}-{stage}-{distinctColor}", args: [dispNames[disp], dispColorNames[distinctColor].ToLowerInvariant(), Ordinal(stage + 1)]);
            }
    }
}
