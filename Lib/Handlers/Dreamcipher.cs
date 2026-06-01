using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDreamcipher
{
    [Question("What was the {1} displayed glyph in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Glyphs,

    [Question("What was the initial binary string in {0}?", OneColumn4Answers)]
    [AnswerGenerator.Strings("16*0-1")]
    Binary
}

public partial class SouvenirModule
{
    [Handler("ksmDreamcipher", "Dreamcipher", typeof(SDreamcipher), "Espik")]
    [ManualQuestion("What were the displayed glyphs?")]
    [ManualQuestion("What was the initial binary string?")]
    private IEnumerator<SouvenirInstruction> ProcessDreamcipher(ModuleData module)
    {
        var comp = GetComponent(module, "Dreamcipher");
        var allGlyphs = GetArrayField<Sprite>(comp, "glyphs", isPublic: true).Get(expectedLength: 130).TranslateSprites(1000).ToArray();

        yield return WaitForSolve;

        var glyphIndices = GetArrayField<int>(comp, "glyphList").Get(expectedLength: 16);

        var binary = "";
        for (var i = 0; i < glyphIndices.Length; i++)
            binary += glyphIndices[i] >= 65 ? "0" : "1";

        for (var i = 0; i < 15; i++)
            yield return question(SDreamcipher.Glyphs, args: [Ordinal(i + 1)]).Answers(allGlyphs[glyphIndices[i]], all: allGlyphs);

        yield return question(SDreamcipher.Binary).Answers(binary);
    }
}
