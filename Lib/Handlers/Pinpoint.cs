using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPinpoint
{
    [SouvenirQuestion("Which point occurred in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Strings), new object[] { new string[] { "A-J" } }, typeof(AnswerGenerator.Integers), new object[] { 1, 10 })]
    Points,

    [SouvenirQuestion("Which distance occurred in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Integers), new object[] { 0, 99 }, typeof(AnswerGenerator.Strings), new object[] { new string[] { ".", "0-9", "0-9", "0-9" } })]
    Distances
}

public partial class SouvenirModule
{
    [SouvenirHandler("pinpoint", "Pinpoint", typeof(SPinpoint), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessPinpoint(ModuleData module)
    {
        var comp = GetComponent(module, "pinpointScript");
        yield return WaitForSolve;

        var dists = GetArrayField<float>(comp, "dists").Get(expectedLength: 3);
        var points = GetArrayField<int>(comp, "points").Get(expectedLength: 4); // includes target point, which we ignore
        addQuestions(module,
            makeQuestion(Question.PinpointPoints, module, correctAnswers: points.Take(3).Select(i => $"{(char) ('A' + i % 10)}{i / 10 + 1}").ToArray()),
            makeQuestion(Question.PinpointDistances, module, correctAnswers: dists.Select(dist => dist.ToString("0.000")).ToArray()));
    }
}