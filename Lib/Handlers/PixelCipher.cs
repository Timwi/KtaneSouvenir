using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPixelCipher
{
    [SouvenirQuestion("What was the keyword in {0}?", ThreeColumns6Answers, "HEART", "HAPPY", "HOUSE", "ARROW", "ARMOR", "ACORN", "CROSS", "CHORD", "CLOCK", "DONUT", "DELTA", "DUCKY", "EQUAL", "EMOJI", "EDGES", "LIBRA", "LUCKY", "LUNAR", "MEDAL", "MOVIE", "MUSIC", "PANDA", "PEARL", "PIANO", "PIXEL")]
    Keyword
}

public partial class SouvenirModule
{
    [SouvenirHandler("pixelcipher", "Pixel Cipher", typeof(SPixelCipher), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessPixelCipher(ModuleData module)
    {
        var comp = GetComponent(module, "pixelcipherScript");
        yield return WaitForSolve;

        var keywords = GetArrayField<string>(comp, "pixelKeyword").Get();
        var pickedKeyword = GetIntField(comp, "pickedKeyword").Get(0, keywords.Length - 1);

        yield return question(SPixelCipher.Keyword).Answers(keywords[pickedKeyword]);
    }
}