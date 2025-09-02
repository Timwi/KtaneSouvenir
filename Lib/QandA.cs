using System;
using System.Collections.Generic;
using System.Linq;

namespace Souvenir;

public sealed class QandA(Enum q, QuestionBase question, AnswerSet answerSet)
{
    public const string Ordinal = "\uE047ordinal";

    public AnswerSet Answers { get; } = answerSet;
    public Enum EnumValue { get; private set; } = q;
    public int NumAnswers => Answers.NumAnswersAllowed;

    public string DebugString => $"{question.DebugText} — {DebugAnswers.Select((a, ix) => string.Format(ix == Answers.CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | ")}";
    public IEnumerable<string> DebugAnswers => Answers.DebugAnswers;

    public string ModuleName => EnumValue.GetHandlerAttribute().ModuleName;

    public bool OnPress(int ix) => Answers.OnPress(ix);

    public void SetQandAs(SouvenirModule souvenir)
    {
        question.SetQuestion(souvenir);
        Answers.SetAnswers(souvenir);
    }

    public void BlinkCorrectAnswer(bool on, SouvenirModule souvenir) => Answers.BlinkAnswer(on, souvenir, Answers.CorrectIndex);
}
