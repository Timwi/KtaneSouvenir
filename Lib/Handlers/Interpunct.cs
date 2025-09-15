using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SInterpunct
{
    [SouvenirQuestion("What was the symbol displayed in the {1} stage of {0}?", ThreeColumns6Answers, "(", ",", ">", "/", "}", "]", "_", "-", "\"", "|", "»", ":", ".", "{", "<", "”", "«", "`", "[", "?", ")", "!", "\\", "'", ";", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("interpunct", "Interpunct", typeof(SInterpunct), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessInterpunct(ModuleData module)
    {
        var comp = GetComponent(module, "InterpunctScript");
        var fldDisplay = GetField<string>(comp, "displaySymbol");
        var fldStage = GetIntField(comp, "stage");

        var currentStage = 0;
        var texts = new string[3];
        var hasStruck = false;
        module.Module.OnStrike += delegate () { hasStruck = true; return false; };

        while (module.Unsolved)
        {
            yield return null;
            var nextStage = fldStage.Get(min: 1, max: 3);   // stage numbers are 1–3, not 0–2
            if (currentStage != nextStage || hasStruck)
            {
                currentStage = nextStage;
                texts[currentStage - 1] = fldDisplay.Get();
                hasStruck = false;
            }
        }

        addQuestions(module, Enumerable.Range(0, 3).Select(i =>
            makeQuestion(SInterpunct.Display, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { texts[i] })));
    }
}