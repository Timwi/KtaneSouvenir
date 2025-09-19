namespace Souvenir;

public class CoordAnswerStump(Coord[] correct, Coord[] preferredWrong, Coord[] all) : AnswerStump<Coord>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => [AnswerType.Sprites];
    protected override AnswerSet MakeAnswerSet(Coord[] answers, int correctIndex, SouvenirQuestionAttribute qAttr, SouvenirModule souvenir) => new CoordAnswerSet(qAttr.Layout, answers, correctIndex);
}
