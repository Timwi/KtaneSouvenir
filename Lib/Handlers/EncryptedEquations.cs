using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEncryptedEquations
{
    [Question("Which shape was the {1} operand in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "EncryptedEquationsSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Shapes
}

public partial class SouvenirModule
{
    [Handler("EncryptedEquationsModule", "Encrypted Equations", typeof(SEncryptedEquations), "Timwi")]
    [ManualQuestion("What were the main shapes of the three operands?")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedEquations(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedEquations");
        yield return WaitForSolve;

        var equation = GetField<object>(comp, "CurrentEquation").Get();

        var fldNames = new[] { "LeftOperand", "MiddleOperand", "RightOperand" };
        var shapeTextureIndexes = fldNames.Select(fldName =>
        {
            var operand = GetField<object>(equation, fldName, isPublic: true).Get();
            var shape = GetField<object>(operand, "Shape", isPublic: true).Get();
            return GetIntField(shape, "TextureIndex", isPublic: true).Get(min: -1, max: EncryptedEquationsSprites.Length - 1);
        }).ToArray();

            if (shapeTextureIndexes.All(ix => ix == -1))
            yield return legitimatelyNoQuestion(module, "All shapes were blank.");

        for (var opIx = 0; opIx < shapeTextureIndexes.Length; opIx++)
            if (shapeTextureIndexes[opIx] != -1)
                yield return question(SEncryptedEquations.Shapes, args: [Ordinal(opIx + 1)])
                    .Answers(EncryptedEquationsSprites[shapeTextureIndexes[opIx]], preferredWrong: EncryptedEquationsSprites);
    }
}
