using System.Collections.Generic;
using UnityEngine;

namespace Souvenir;

public class SpriteAnswerStump(Sprite[] correct, Sprite[] preferredWrong, Sprite[] all, float xStretch, IEnumerable<Sprite> answerGenerator = null) : AnswerStump<Sprite>(correct, preferredWrong, all, answerGenerator)
{
    protected override InfoType[] acceptableTypes => [InfoType.Sprites];
    protected override AnswerSet MakeAnswerSet(Sprite[] answers, int correctIndex, QuestionAttribute qAttr, SouvenirModule souvenir) =>
        new SpriteAnswerSet(qAttr.Layout, answers, correctIndex, xStretch);
    public override string ToString() => Correct.Stringify();
}
