using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMissingLetter
{
    [Question("What letter was missing in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    MissingLetter
}

public partial class SouvenirModule
{
    [Handler("theMissingLetter", "Missing Letter", typeof(SMissingLetter), "KiloBites", AddThe = true)]
    [ManualQuestion("What letter was missing?")]
    private IEnumerator<SouvenirInstruction> ProcessMissingLetter(ModuleData module)
    {
        var comp = GetComponent(module, "TheMissingLetterScript");
        yield return WaitForSolve;

        var labels = GetArrayField<TextMesh>(comp, "Labels", isPublic: true).Get(expectedLength: 25);
        var letters = GetListField<char>(comp, "alph").Get(expectedLength: 26);

        IEnumerator DisappearLetters()
        {
            foreach (var label in labels)
            {
                label.text = "";
                yield return new WaitForSeconds(.1f);
            }
        }
        StartCoroutine(DisappearLetters());

        yield return question(SMissingLetter.MissingLetter).Answers(letters.Last().ToString());
    }
}
