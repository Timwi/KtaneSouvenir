using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SYellowButtont
{
    [SouvenirQuestion("What was the filename in {0}?", TwoColumns4Answers, ExampleAnswers = ["ABACUS.JPG", "BABBLE.MP4", "CABLES.MP3", "DABBLE.CS", "EAGLES.EXE", "FABLED.ISO"])]
    Filename
}

public partial class SouvenirModule
{
    [SouvenirHandler("yellowbuttont", "Yellow Button’t", typeof(SYellowButtont), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessYellowButtont(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "yellow");
        var text = GetField<TextMesh>(comp, "DisplayText", true).Get();
        var ans = text.text;
        if (!ans.Contains('.'))
            throw new AbandonModuleException($"Expected a filename with a dot, got {ans}");

        yield return null; // Wait one frame to allow other Souvenirs to also grab the text
        text.text = "";

        var names = GetArrayField<string>(comp, "names").Get(expectedLength: 4031);
        var extensions = new[] {
            new[] { "JPG", "JPEG", "SVG", "PNG" },
            new[] { "MP4", "AVI", "MKV", "WMV" },
            new[] { "MP3", "WAV", "OGG", "WMA" },
            new[] { "CS", "TXT", "JSON", "CSV", "DOC", "DOCX" },
            new[] { "EXE" },
            new[] { "ISO", "XYZ", "RET", "MAE" }
        };

        var ext = ans.Split('.').Last();
        var extIx = extensions.IndexOf(a => a.Contains(ext));
        var chosenNames = names.OrderRandomly().Take(6).ToArray();
        var answers = Enumerable
            .Range(0, 6)
            .Except(new[] { extIx })
            .Select(i => $"{names[i]}.{extensions[i].PickRandom()}")
            .Concat(new[] { ans })
            .ToArray();

        yield return question(SYellowButtont.Filename).Answers(ans, all: answers);
    }
}