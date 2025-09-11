using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SImpostor
{
    [SouvenirQuestion("Which module was {0} pretending to be?", OneColumn4Answers, ExampleAnswers = ["Ice Cream", "Microcontroller", "Sea Shells", "Combination Lock"])]
    Disguise
}

public partial class SouvenirModule
{
    [SouvenirHandler("impostor", "Impostor", typeof(SImpostor), "Kuro", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessImpostor(ModuleData module)
    {
        var comp = GetComponent(module, "impostorScript");

        yield return WaitForSolve;

        var possibleModuleNames = GetArrayField<GameObject>(comp, "Prefabs", isPublic: true).Get().Select(pref => pref.name).ToArray();
        var chosenModIndex = GetIntField(comp, "chosenMod").Get(min: 0, max: possibleModuleNames.Length - 1);
        addQuestion(module, Question.ImpostorDisguise, correctAnswers: new[] { possibleModuleNames[chosenModIndex] }, preferredWrongAnswers: possibleModuleNames);
    }
}