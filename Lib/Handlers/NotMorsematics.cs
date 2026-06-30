using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotMorsematics
{
    [Question("What was the {1} transmitted letter in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [Handler("notMorsematics", "Not Morsematics", typeof(SNotMorsematics), "Espik")]
    [ManualQuestion("What were the transmitted letters?")]
    private IEnumerator<SouvenirInstruction> ProcessNotMorsematics(ModuleData module)
    {
        var comp = GetComponent(module, "NMorScript");
        yield return WaitForSolve;

        var letters = GetArrayField<string>(comp, "word").Get(expectedLength: 2)[1];
        
        for (var i = 0; i < letters.Length; i++)
            yield return question(SNotMorsematics.Letter, args: [Ordinal(i + 1)]).Answers(letters[i].ToString());
    }
}
