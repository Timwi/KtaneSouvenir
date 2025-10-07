using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEncryptionBingo
{
    [SouvenirQuestion("What was the first encoding used in {0}?", OneColumn4Answers, "Morse Code", "Tap Code", "Maritime Flags", "Semaphore", "Pigpen", "Lombax", "Braille", "Wingdings", "Zoni", "Galactic Alphabet", "Arrow", "Listening", "Regular Number", "Chinese Number", "Cube Symbols", "Runes", "New York Point", "Fontana", "ASCII Hex Code", TranslateAnswers = true)]
    Encoding
}

public partial class SouvenirModule
{
    [SouvenirHandler("encryptionBingo", "Encryption Bingo", typeof(SEncryptionBingo), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptionBingo(ModuleData module)
    {
        var comp = GetComponent(module, "encryptionBingoScript");
        var fldBall = GetField<bool>(comp, "ballOut");
        var stampedSquares = GetField<List<int>>(comp, "stampedSquares").Get();
        var encodingNames = GetArrayField<string>(comp, "encryptions").Get();

        // When the first correct(!) square is pressed, Encryption Bingo adds an entry to stampedSquares but helpfully waits .25 sec before changing any variables, including encryptionIndex
        while (!fldBall.Get() || stampedSquares.Count == 0)
            yield return null;  // don’t wait .1 sec here because it’s important to not miss the moment
        var encoding = GetIntField(comp, "encryptionIndex").Get();

        yield return WaitForSolve;

        yield return question(SEncryptionBingo.Encoding).Answers(encodingNames[encoding], preferredWrong: encodingNames);
    }
}
