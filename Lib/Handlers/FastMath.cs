using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFastMath
{
    [SouvenirQuestion("What was the last pair of letters in {0}?", ThreeColumns6Answers, ExampleAnswers = ["CT", "DK", "SA", "SG", "SX", "TX", "TZ", "XP", "XX", "ZB"])]
    LastLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("fastMath", "Fast Math", typeof(SFastMath), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessFastMath(ModuleData module)
    {
        var comp = GetComponent(module, "FastMathModule");
        var fldScreen = GetField<TextMesh>(comp, "Screen", isPublic: true);
        var usableLetters = GetField<string>(comp, "letters").Get();

        yield return WaitForActivate;

        var wrongAnswers = new HashSet<string>();
        string letters = null;
        while (module.Unsolved)
        {
            var display = fldScreen.Get().text;
            if (display.Length != 3)
                throw new AbandonModuleException($"The screen contains something other than three characters: “{display}” ({display.Length} characters).");
            letters = display[0] + "" + display[2];
            wrongAnswers.Add(letters);
            yield return new WaitForSeconds(.1f);
        }
        if (letters == null)
            throw new AbandonModuleException("No letters were extracted before the module was solved.");

        while (wrongAnswers.Count < 6)
            foreach (var ans in new AnswerGenerator.Strings(2, usableLetters).GetAnswers(this).Take(6 - wrongAnswers.Count))
                wrongAnswers.Add(ans);

        yield return question(SFastMath.LastLetters).Answers(letters, preferredWrong: wrongAnswers.ToArray());
    }
}