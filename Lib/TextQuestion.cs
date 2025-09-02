using System;
using UnityEngine;

namespace Souvenir;

public sealed class TextQuestion(string question, AnswerLayout layout, Sprite questionSprite = null, float questionSpriteRotation = 0) : QuestionBase(question)
{
    private double desiredHeightFactor => layout switch
    {
        AnswerLayout.OneColumn3Answers => 1,
        AnswerLayout.OneColumn4Answers => .825,
        AnswerLayout.TwoColumns2Answers => 1.375,
        AnswerLayout.TwoColumns4Answers => 1.1,
        AnswerLayout.ThreeColumns3Answers => 1.375,
        AnswerLayout.ThreeColumns6Answers => 1.1,
        _ => throw new InvalidOperationException("Invalid AnswerLayout."),
    };

    public override string DebugText => $"{_text}{(questionSprite == null ? "" : $" (sprite: {questionSprite.name}{(questionSpriteRotation != 0 ? $" (rotated {questionSpriteRotation}°)" : "")})")}";

    public override void SetQuestion(SouvenirModule souv)
    {
        if (questionSprite != null)
        {
            var sprite = Sprite.Create(questionSprite.texture, questionSprite.rect, new Vector2(1, .5f), questionSprite.pixelsPerUnit);
            sprite.name = questionSprite.name;
            souv.QuestionSprite.sprite = sprite;
            souv.QuestionSprite.transform.localEulerAngles = new Vector3(90, questionSpriteRotation);
            souv.QuestionSprite.gameObject.SetActive(true);
        }
        souv.SetWordWrappedText(_text, desiredHeightFactor, questionSprite != null);
    }
}
