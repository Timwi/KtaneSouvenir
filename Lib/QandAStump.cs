namespace Souvenir;

public class QandAStump(QuestionStump questionStump, AnswerStump answerStump)
{
    public QuestionStump QuestionStump { get; } = questionStump;
    public AnswerStump AnswerStump { get; } = answerStump;

    public QandA GenerateQandA(AnswerSet answerSet, string moduleFormat, int numSolved, QuestionExtra questionExtraFromDiscriminator) =>
        new(QuestionStump.EnumValue, QuestionStump.MakeQuestion(moduleFormat, questionExtraFromDiscriminator), answerSet) { GeneratedAtNumSolved = numSolved };

    public override string ToString() => $"{QuestionStump} │ {AnswerStump}";
}
