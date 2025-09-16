using System.Collections.Generic;
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

        var fldNames = new[] { "LeftOperand", "MiddleOperand", "RightOperand" };
        for (var opIx = 0; opIx < fldNames.Length; opIx++)
        {
            var operand = GetField<object>(equation, fldNames[opIx], isPublic: true).Get();
            var shape = GetField<object>(operand, "Shape", isPublic: true).Get();
            if (GetIntField(shape, "TextureIndex", isPublic: true).Get(min: -1, max: EncryptedEquationsSprites.Length - 1) is int textureIx and not -1)
                yield return question(SEncryptedEquations.Shapes, args: [Ordinal(opIx + 1)])
                    .Answers(EncryptedEquationsSprites[textureIx], preferredWrong: EncryptedEquationsSprites);
        }
    }
}
