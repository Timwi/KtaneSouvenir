using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SStarstruck
{
    [SouvenirQuestion("Which star was present on {0}?", ThreeColumns6Answers, Type = AnswerType.DynamicFont, FontSize = 432, CharacterSize = 1 / 7f)]
    [AnswerGenerator.Strings("a-zA-Z0-9!@#$%^&*()=+_,./<>?;:[]\\{}|-")]
    Star
}

public partial class SouvenirModule
{
    [SouvenirHandler("starstruck", "Starstruck", typeof(SStarstruck), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessStarstruck(ModuleData module)
    {
        // This handler *should* ask about the color of a given star, but currently I can't turn a font character into a sprite.
        yield return WaitForSolve;

        var comp = GetComponent(module, "starstruck");
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^^&*()=+_,./<>?;:[]\\{}|-";
        var stars = GetArrayField<char>(comp, "piecePositions").Get(expectedLength: 3, validator: v => !valid.Contains(v) ? $"Expected chars in \"{valid}\"" : null);
        var text = GetArrayField<TextMesh>(comp, "bigStars", true).Get(expectedLength: 3)[0];

        addQuestions(module, makeQuestion(Question.StarstruckStar, module, text.font, text.GetComponent<Renderer>().sharedMaterial.mainTexture, correctAnswers: stars.Select(c => c.ToString()).ToArray()));
    }
}