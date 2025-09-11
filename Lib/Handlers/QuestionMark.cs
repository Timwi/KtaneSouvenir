using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SQuestionMark
{
    [SouvenirQuestion("Which of these symbols was part of the flashing sequence in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "QuestionMarkSprites")]
    FlashedSymbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("Questionmark", "Question Mark", typeof(SQuestionMark), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessQuestionMark(ModuleData module)
    {
        var comp = GetComponent(module, "Questionmark");

        yield return WaitForSolve;

        var flashedSpritePool = GetArrayField<int>(comp, "spritePool").Get(expectedLength: 4);
        addQuestion(module, Question.QuestionMarkFlashedSymbols, correctAnswers: flashedSpritePool.Select(ix => QuestionMarkSprites[ix]).ToArray(), preferredWrongAnswers: QuestionMarkSprites);
    }
}