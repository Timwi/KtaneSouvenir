using UnityEngine;

namespace Souvenir;

public class SpriteAnswerStump(Sprite[] correct, Sprite[] preferredWrong, Sprite[] all) : AnswerStump<Sprite>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => _standardAnswerTypes;
    protected override AnswerSet MakeAnswerSet(Sprite[] answers, int correctIndex, AnswerLayout layout, SouvenirModule souvenir) => new SpriteAnswerSet(layout, answers, correctIndex);
}
