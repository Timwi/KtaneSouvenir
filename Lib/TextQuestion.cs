using System;
using UnityEngine;

namespace Souvenir;

public sealed class TextQuestion(string question, AnswerLayout layout, QuestionExtra questionExtra = null) : QuestionBase(question)
{
    private double desiredHeightFactor => layout switch
    {
        AnswerLayout.OneColumn2Answers => 1.1,
        AnswerLayout.OneColumn3Answers => 1,
        AnswerLayout.OneColumn4Answers => .825,
        AnswerLayout.TwoColumns2Answers => 1.375,
        AnswerLayout.TwoColumns4Answers => 1.1,
        AnswerLayout.ThreeColumns3Answers => 1.375,
        AnswerLayout.ThreeColumns6Answers => 1.1,
        _ => throw new InvalidOperationException("Invalid AnswerLayout."),
    };

    public override string DebugText => $"{_text}{(questionExtra == null ? "" : $", {questionExtra}")}";

    public override void SetQuestion(SouvenirModule souv) => souv.SetWordWrappedText(_text, desiredHeightFactor, questionExtra?.Setup(souv));
}
