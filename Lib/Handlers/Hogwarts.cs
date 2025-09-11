using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHogwarts
{
    [SouvenirQuestion("Which House was {1} solved\u00a0for in {0}?", TwoColumns4Answers, "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw", TranslateAnswers = true, Arguments = ["Binary Puzzle", "Zoni", "Rock-Paper- Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards"], ArgumentGroupSize = 1)]
    House,
    
    [SouvenirQuestion("Which module was solved\u00a0for {1} in {0}?", OneColumn4Answers, ExampleAnswers = ["Binary Puzzle", "Zoni", "Rock-Paper-Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards"], Arguments = ["Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Module
}

public partial class SouvenirModule
{
    [SouvenirHandler("HogwartsModule", "Hogwarts", typeof(SHogwarts), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessHogwarts(ModuleData module)
    {
        var comp = GetComponent(module, "HogwartsModule");
        var fldModuleNames = GetField<IDictionary>(comp, "_moduleNames");
        yield return WaitForSolve;

        var dic = fldModuleNames.Get();
        if (dic.Count == 0)
            yield return legitimatelyNoQuestion(module, "No module solves were awarded to it.");

        // Rock-Paper-Scissors-Lizard-Spock needs to be broken up in the question because hyphens don't word-wrap.
        addQuestions(module,
            dic.Keys.Cast<object>().Where(house => dic[house] != null).SelectMany(house => Ut.NewArray(
                makeQuestion(Question.HogwartsHouse, module,
                    formatArgs: new[] { dic[house].ToString() == "Rock-Paper-Scissors-L.-Sp." ? "Rock-Paper- Scissors-L.-Sp." : dic[house].ToString() },
                    correctAnswers: new[] { house.ToString() }),
                makeQuestion(Question.HogwartsModule, module,
                    formatArgs: new[] { house.ToString() },
                    correctAnswers: new[] { dic[house].ToString() },
                    preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()))));
    }
}