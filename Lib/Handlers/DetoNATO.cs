using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDetoNATO
{
    [SouvenirQuestion("What was the {1} display in {0}?", TwoColumns4Answers, ExampleAnswers = ["Ozzy Osbourne", "Jouleliette", "Flockstrot", "Joulelette", "Jouleliett", "Uniqueform"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("Detonato", "DetoNATO", typeof(SDetoNATO), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessDetoNATO(ModuleData module)
    {
        var comp = GetComponent(module, "Detonato");
        var fldStage = GetIntField(comp, "stage");
        var words = GetArrayField<string>(comp, "words").Get(expectedLength: 156);
        var textMesh = GetField<TextMesh>(comp, "screenText", true).Get();
        var displaysList = new List<string>();
        var currentStage = -1;
        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            var currentWord = textMesh.text;
            if (currentWord != "")
            {
                if (newStage != currentStage || currentStage >= displaysList.Count)
                {
                    displaysList.Add(currentWord);
                    currentStage = newStage;
                }
                else
                    displaysList[currentStage] = currentWord;
            }
            yield return null;
        }
        yield return WaitForSolve;
        addQuestions(module, displaysList.Select((w, ix) => makeQuestion(Question.DetoNATODisplay, module,
            formatArgs: new[] { Ordinal(ix + 1) },
            correctAnswers: new[] { displaysList[ix] },
            allAnswers: words)));
    }
}