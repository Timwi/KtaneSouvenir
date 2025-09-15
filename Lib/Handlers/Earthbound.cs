using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEarthbound
{
    [SouvenirQuestion("What was the background in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Background,

    [SouvenirQuestion("Which monster was displayed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Monster
}

public partial class SouvenirModule
{
    [SouvenirHandler("EarthboundModule", "Earthbound", typeof(SEarthbound), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessEarthbound(ModuleData module)
    {
        var comp = GetComponent(module, "EarthboundScript");
        yield return WaitForSolve;

        var enemyIndex = GetIntField(comp, "enemyIndex").Get(val => val is < 0 or > 29 ? "expected range 0–29" : null);
        var enemySprites = GetArrayField<Sprite>(comp, "enemyOptions", isPublic: true).Get(expectedLength: 30).Select(sprite => sprite.TranslateSprite(sprite.name switch
        {
            "Absolutely Safe Capsule" => 350,
            "Mad Car" or "Mr Passion" => 200,
            _ => 250,
        })).ToArray();

        var backgroundMaterials = GetArrayField<Material>(comp, "backgroundOptions", isPublic: true).Get(expectedLength: 30);
        var backgroundIndex = GetIntField(comp, "usedBackgroundInt").Get(val => val is < 0 or > 29 ? "expected range 0–29" : null);

        // Get the smallest width and height to make all answers the same dimensions
        var width = backgroundMaterials.Min(m => m.mainTexture.width);
        var height = backgroundMaterials.Min(m => m.mainTexture.height);
        var backgroundSprites = backgroundMaterials.Select(material => Sprite.Create(material.mainTexture as Texture2D, new Rect(0, 0, width, height), new Vector2(0, .5f), 200f)).ToArray();

        addQuestions(module,
            makeQuestion(Question.EarthboundBackground, module, correctAnswers: new[] { backgroundSprites[backgroundIndex] }, allAnswers: backgroundSprites),
            makeQuestion(Question.EarthboundMonster, module, correctAnswers: new[] { enemySprites[enemyIndex] }, allAnswers: enemySprites));
    }
}