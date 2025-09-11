using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOneLinksToAll
{
    [SouvenirQuestion("What was the starting article in {0}?", OneColumn4Answers, ExampleAnswers = ["Waves (Jade Warrior album)", "Himali Siriwardena", "Campbell Pass", "1973 Northern Ireland Assembly election", "Bravo Airways", "Adolph Hoffmann", "Australian cyclists at the Tour de France", "Lebanese Canadians", "Albert Richard Pritchard", "Mary A. Lehman"])]
    Start,
    
    [SouvenirQuestion("What was the ending article in {0}?", OneColumn4Answers, ExampleAnswers = ["Bob Kitterman", "Johannes Nevala", "Alfred Patfield", "Dublin Bay South (Dáil constituency)", "The Monkees Present", "Finding Me", "Sibora", "Operator (linguistics)", "2022 Iowa Senate election", "Ab Dang Sar, Savadkuh"])]
    End
}

public partial class SouvenirModule
{
    [SouvenirHandler("oneLinksToAllModule", "One Links To All", typeof(SOneLinksToAll), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessOneLinksToAll(ModuleData module)
    {
        var comp = GetComponent(module, "OneLinksToAllScript");

        yield return WaitForSolve;

        var start = GetField<string>(comp, "title1").Get();
        var end = GetField<string>(comp, "title2").Get();
        var path = GetListField<string>(comp, "exampleSolution").Get().ToArray();

        addQuestions(module,
            makeQuestion(Question.OneLinksToAllStart, module, correctAnswers: new[] { start }, allAnswers: path, preferredWrongAnswers: new[] { end }),
            makeQuestion(Question.OneLinksToAllEnd, module, correctAnswers: new[] { end }, allAnswers: path, preferredWrongAnswers: new[] { start }));
    }
}