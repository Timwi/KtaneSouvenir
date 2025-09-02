using UnityEngine;

namespace Souvenir;

public sealed class SpriteQuestion(string questionText, Sprite questionSprite) : QuestionBase(questionText)
{
    public override string DebugText => $"(Depicted visually) {base.DebugText}";

    public override void SetQuestion(SouvenirModule souvenir)
    {
        souvenir.EntireQuestionSprite.gameObject.SetActive(true);
        souvenir.EntireQuestionSprite.sprite = questionSprite;
    }
}
