using System.Collections;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SHogwarts
{
    [SouvenirQuestion("Which House was {1} solved\u00a0for in {0}?", TwoColumns4Answers, "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw", TranslateAnswers = true, Arguments = ["Binary Puzzle", "Zoni", "Rock-Paper- Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards"], ArgumentGroupSize = 1)]
    House,

    [SouvenirQuestion("Which module was solved\u00a0for {1} in {0}?", OneColumn4Answers, ExampleAnswers = ["Binary Puzzle", "Zoni", "Rock-Paper-Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards"], Arguments = ["Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw"], ArgumentGroupSize = 1, TranslateArguments = [true])]
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
        foreach (var house in dic.Keys)
            if (dic[house] is string name)
            {
                yield return question(SHogwarts.House, args: [name == "Rock-Paper-Scissors-L.-Sp." ? "Rock-Paper- Scissors-L.-Sp." : name]).Answers(house.ToString());
                yield return question(SHogwarts.Module, args: [house.ToString()]).Answers(name, preferredWrong: Bomb.GetSolvableModuleNames().ToArray());
            }
    }
}
