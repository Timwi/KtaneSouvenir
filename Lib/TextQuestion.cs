using System;

namespace Souvenir;

public sealed class TextQuestion(string question, AnswerLayout layout, QuestionExtra questionExtra = null) : QuestionBase(question)
{
    private double desiredHeightFactor => layout switch
    {
        AnswerLayout.OneColumn2Answers => .59,
        AnswerLayout.OneColumn3Answers => .54,
        AnswerLayout.OneColumn4Answers => .44,
        AnswerLayout.TwoColumns2Answers => .74,
        AnswerLayout.TwoColumns4Answers => .59,
        AnswerLayout.ThreeColumns3Answers => .74,
        AnswerLayout.ThreeColumns6Answers => .59,
        _ => throw new InvalidOperationException("Invalid AnswerLayout."),
    };

    public override string DebugText => $"{_text}{(questionExtra == null ? "" : $", {questionExtra}")}";

    public override void SetQuestion(SouvenirModule souv) => souv.SetWordWrappedText(_text, desiredHeightFactor, questionExtra?.Setup(souv));
}
