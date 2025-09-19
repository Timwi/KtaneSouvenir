using UnityEngine;

namespace Souvenir;

public class SpriteAnswerStump(Sprite[] correct, Sprite[] preferredWrong, Sprite[] all) : AnswerStump<Sprite>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => [AnswerType.Sprites];
    protected override AnswerSet MakeAnswerSet(Sprite[] answers, int correctIndex, SouvenirQuestionAttribute qAttr, SouvenirModule souvenir) =>
        new SpriteAnswerSet(qAttr.Layout, answers, correctIndex);
}
