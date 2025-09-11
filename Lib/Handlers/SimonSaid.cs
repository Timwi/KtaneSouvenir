using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSaid
{
    [SouvenirQuestion("What color flashed {1} in the final sequence of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSaidModule", "Simon Said", typeof(SSimonSaid), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSaid(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSaidScript");
        var fldModuleActivated = GetField<bool>(comp, "activated");
        var btnColors = GetListField<int>(comp, "btnColors").Get();
        var colorNames = GetArrayField<string>(comp, "colorNames").Get();

        while (!fldModuleActivated.Get())
            yield return new WaitForSeconds(.1f);

        var pressIx = GetListField<int>(comp, "correctBtnPresses");
        var flashingBtn = GetIntField(comp, "flashingBtn");
        var flashes = new int[6];
        while (module.Unsolved)
        {
            var btn = flashingBtn.Get();
            var ix = pressIx.Get().Count() - 1;
            flashes[ix] = btn;
            yield return null;
        }

        addQuestions(module, flashes.Select((val, ix) => makeQuestion(Question.SimonSaidFlashes, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[btnColors[val]] })));
    }
}