using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEncryptedEquations
{
    [SouvenirQuestion("Which shape was the {1} operand in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "EncryptedEquationsSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Shapes
}

public partial class SouvenirModule
{
    [SouvenirHandler("EncryptedEquationsModule", "Encrypted Equations", typeof(SEncryptedEquations), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedEquations(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedEquations");
        yield return WaitForSolve;

        var equation = GetField<object>(comp, "CurrentEquation").Get();

        addQuestions(module, new[] { "LeftOperand", "MiddleOperand", "RightOperand" }
            .Select(fldName => GetField<object>(equation, fldName, isPublic: true).Get())
            .Select(op => GetField<object>(op, "Shape", isPublic: true).Get())
            .Select(sh => GetIntField(sh, "TextureIndex", isPublic: true).Get(min: -1, max: EncryptedEquationsSprites.Length - 1))
            .Select((txIx, opIx) => txIx == -1 ? null : new { Shape = EncryptedEquationsSprites[txIx], Ordinal = Ordinal(opIx + 1) })
            .Where(inf => inf != null)
            .Select(inf => makeQuestion(Question.EncryptedEquationsShapes, module, formatArgs: new[] { inf.Ordinal }, correctAnswers: new[] { inf.Shape }, preferredWrongAnswers: EncryptedEquationsSprites)));
    }
}