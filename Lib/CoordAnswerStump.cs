namespace Souvenir;

public class CoordAnswerStump(Coord[] correct, Coord[] preferredWrong, Coord[] all) : AnswerStump<Coord>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => _standardAnswerTypes;
    protected override AnswerSet MakeAnswerSet(Coord[] answers, int correctIndex, AnswerLayout layout, SouvenirModule souvenir) => new CoordAnswerSet(layout, answers, correctIndex);
}
