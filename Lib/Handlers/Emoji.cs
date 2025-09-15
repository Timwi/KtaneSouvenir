using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEmoji
{
    [SouvenirQuestion("What was the {1} emoji in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Emoji
}

public partial class SouvenirModule
{
    [SouvenirHandler("emoji", "Emoji", typeof(SEmoji), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessEmoji(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "emojiScript");
        var spriteSlots = GetArrayField<SpriteRenderer>(comp, "SpriteSlots", true).Get(expectedLength: 4);
        var allSprites = GetArrayField<Sprite>(comp, "EmojiSprites", true).Get(expectedLength: 625);
        var usedSprites = new[] { Array.IndexOf(allSprites, spriteSlots[0].sprite), Array.IndexOf(allSprites, spriteSlots[1].sprite) };
        allSprites = allSprites.TranslateSprites(200).ToArray();

        addQuestions(module,
            makeQuestion(Question.EmojiEmoji, module, formatArgs: new[] { "left" }, correctAnswers: new[] { allSprites[usedSprites[0]] }, preferredWrongAnswers: new[] { allSprites[usedSprites[1]] }, allAnswers: allSprites),
            makeQuestion(Question.EmojiEmoji, module, formatArgs: new[] { "right" }, correctAnswers: new[] { allSprites[usedSprites[1]] }, preferredWrongAnswers: new[] { allSprites[usedSprites[0]] }, allAnswers: allSprites));
    }
}