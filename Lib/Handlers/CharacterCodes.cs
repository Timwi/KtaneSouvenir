using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCharacterCodes
{
    [Question("What was the {1} character in {0}?", ThreeColumns6Answers, ExampleAnswers = ["♥", "♣", "•", "☑", "☣", "Ϣ"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Character
}

public partial class SouvenirModule
{
    [Handler("characterCodes", "Character Codes", typeof(SCharacterCodes), "NickLatkovich")]
    [ManualQuestion("What were the characters?")]
    private IEnumerator<SouvenirInstruction> ProcessCharacterCodes(ModuleData module)
    {
        var comp = GetComponent(module, "CharacterCodes");
        yield return WaitForSolve;

        var code = GetArrayField<string>(comp, "chosenLetters").Get();
        var allChars = GetStaticField<Dictionary<ushort, string>>(comp.GetType(), "characterList").Get().Values.ToArray();
        for (var i = 0; i < code.Length; i++)
            yield return question(SCharacterCodes.Character, args: [Ordinal(i + 1)]).Answers(code[i], preferredWrong: allChars);
    }
}