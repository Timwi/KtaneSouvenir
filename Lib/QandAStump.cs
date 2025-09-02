namespace Souvenir;

public class QandAStump(QuestionStump questionStump, AnswerStump answerStump)
{
    public QuestionStump QuestionStump { get; } = questionStump;
    public AnswerStump AnswerStump { get; } = answerStump;

    public QandA GenerateQandA(AnswerSet answerSet, string moduleFormat, SouvenirModule souvenir) =>
        new(QuestionStump.EnumValue, QuestionStump.MakeQuestion(moduleFormat, souvenir), answerSet);
}
