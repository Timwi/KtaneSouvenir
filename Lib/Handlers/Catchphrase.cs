using System.Collections.Generic;
using System.Linq;
using Souvenir;

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
        yield return WaitForSolve;

        var panelColors = GetListField<string>(comp, "selectedColours").Get(expectedLength: 4);
        var panelNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };

        panelColors = panelColors.Select(x => char.ToUpperInvariant(x[0]) + x.Substring(1)).ToList();

        addQuestions(module,
            Enumerable.Range(0, 4).Select(panel => makeQuestion(SCatchphrase.Colour, module, formatArgs: new[] { panelNames[panel] }, correctAnswers: new[] { panelColors[panel] })));
    }
}