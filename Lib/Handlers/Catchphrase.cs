using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SCatchphrase
{
    [SouvenirQuestion("What was the colour of the {1} panel in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Orange", "Purple", "Yellow", Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    Colour
}

public partial class SouvenirModule
{
    [SouvenirHandler("catchphrase", "Catchphrase", typeof(SCatchphrase), "GoodHood")]
    private IEnumerator<SouvenirInstruction> ProcessCatchphrase(ModuleData module)
    {
        var comp = GetComponent(module, "catchphraseScript");
        bool[] isPanelSolved = null;
        module.Module.OnPass += () =>
        {
            // Find out which panels were removed (it’s possible to solve the module without removing all of them)
            isPanelSolved = GetArrayField<KMSelectable>(comp, "panels", isPublic: true).Get(expectedLength: 4)
                .Select(panel => panel.GetComponentInParent<Animator>().GetBool("shrink"))
                .ToArray();
            return false;
        };
        yield return WaitForSolve;

        var panelNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        var panelColors = GetListField<string>(comp, "selectedColours").Get(expectedLength: 4).Select(x => char.ToUpperInvariant(x[0]) + x.Substring(1)).ToArray();

        for (var panel = 0; panel < 4; panel++)
            if (isPanelSolved[panel])
                yield return question(SCatchphrase.Colour, args: [panelNames[panel]]).Answers(panelColors[panel]);
    }
}
