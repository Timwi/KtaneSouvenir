using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMoon
{
    [SouvenirQuestion("What was the {1} set in clockwise order in {0}?", TwoColumns4Answers, "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east", TranslateAnswers = true, Arguments = ["first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    LitUnlit
}

public partial class SouvenirModule
{
    [SouvenirHandler("moon", "Moon", typeof(SMoon), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMoon(ModuleData module)
    {
        var comp = GetComponent(module, "theMoonScript");
        yield return WaitForSolve;

        var lightIndex = GetIntField(comp, "lightIndex").Get(min: 0, max: 7);
        var qNames = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" };
        var aNames = new[] { "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east" };
        for (var i = 0; i < 8; i++)
            yield return question(SMoon.LitUnlit, args: [qNames[i]]).Answers(aNames[(i + lightIndex) % 8]);
    }
}