namespace Souvenir;

public class TextAnswerStump(string[] correct, string[] preferredWrong, string[] all, TextAnswerInfo info) : AnswerStump<string>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => _standardAnswerTypes;
    protected override AnswerSet MakeAnswerSet(string[] answers, int correctIndex, AnswerLayout layout, SouvenirModule souvenir) => new TextAnswerSet(layout, answers, correctIndex, info);
}
