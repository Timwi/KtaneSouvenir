using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SModuleMaze
{
    [SouvenirQuestion("Which of the following was the starting icon for {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    StartingIcon
}

public partial class SouvenirModule
{
    [SouvenirHandler("ModuleMaze", "Module Maze", typeof(SModuleMaze), "River")]
    private IEnumerator<SouvenirInstruction> ProcessModuleMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ModuleMazeModule");
        var fldSprites = GetArrayField<Sprite>(comp, "sprites", true);
        yield return WaitForSolve;

        var sprites = fldSprites.Get(expectedLength: 400);
        var translatedSprites = sprites.Select(spr => spr.TranslateSprite()).ToArray();

        addQuestions(module,
            makeQuestion(Question.ModuleMazeStartingIcon, module,
                correctAnswers: new[] { translatedSprites[Array.IndexOf(sprites, GetField<Sprite>(comp, "souvenirStart").Get())] }, allAnswers: translatedSprites));
    }
}