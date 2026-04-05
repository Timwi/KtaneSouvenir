using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRGBEncryption
{
    [Question("What was the {1} Morse code sequence in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("5*.-")]
    MorseSequence,

    [Question("What was the {1} color sequence in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("5*RGB")]
    ColorSequence
}

public partial class SouvenirModule
{
    [Handler("RGBEncryption", "RGB Encryption", typeof(SRGBEncryption), "Quinn Wuest")]
    [ManualQuestion("What were the Morse code and color sequences?")]
    private IEnumerator<SouvenirInstruction> ProcessRGBEncryption(ModuleData module)
    {
        var comp = GetComponent(module, "RGBEncryption");
        yield return WaitForSolve;

        var fldFlashingMorse = GetField<string>(comp, "SouvenirMorseFlashes").Get().Split(' ');
        var fldFlashingColors = GetField<string>(comp, "SouvenirColorFlashes").Get().Split(' ');
        for (int i = 0; i < 4; i++)
        {
            yield return question(SRGBEncryption.MorseSequence, args: [Ordinal(i + 1)]).Answers(fldFlashingMorse[i]);
            yield return question(SRGBEncryption.ColorSequence, args: [Ordinal(i + 1)]).Answers(fldFlashingColors[i]);
        }
    }
}
