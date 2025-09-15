using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAngelHernandez
{
    [SouvenirQuestion("What letter was shown by the raised buttons on the {1} stage on {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    MainLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("AngelHernandezModule", "Ángel Hernández", typeof(SAngelHernandez), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessAngelHernandez(ModuleData module)
    {
        var comp = GetComponent(module, "AngelHernandezScript");
        var fldActivated = GetField<bool>(comp, "_canPress");
        var fldStage = GetIntField(comp, "_currentStage");
        var fldMainLetter = GetIntField(comp, "_mainLetter");

        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var displayedLetters = new string[2];
        var alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(i => i.ToString()).ToArray();

        for (var i = 0; i < 2; i++)
        {
            while (fldStage.Get() == i)
            {
                while (!fldActivated.Get())
                    yield return null;

                displayedLetters[i] = alph[fldMainLetter.Get()];

                while (fldActivated.Get())
                    yield return null;
            }
        }

        yield return WaitForSolve;
        addQuestions(module, displayedLetters.Select((word, stage) => makeQuestion(SAngelHernandez.MainLetter, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { word }, preferredWrongAnswers: alph)));
    }
}