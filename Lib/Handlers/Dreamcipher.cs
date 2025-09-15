using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDreamcipher
{
    [SouvenirQuestion("What was the decrypted word in {0}?", OneColumn4Answers, ExampleAnswers = ["asparagus", "demonstration", "fossilizing", "foursquare", "grinning", "jumpiness", "pasteboard", "prosecution", "sarcastic", "transition"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("ksmDreamcipher", "Dreamcipher", typeof(SDreamcipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessDreamcipher(ModuleData module)
    {
        var comp = GetComponent(module, "Dreamcipher");
        var wordList = JsonConvert.DeserializeObject<string[]>(GetField<TextAsset>(comp, "wordList", isPublic: true).Get().text);

        yield return WaitForSolve;

        var targetWord = GetField<string>(comp, "targetWord").Get().ToLowerInvariant();
        yield return question(SDreamcipher.Word).Answers(targetWord, preferredWrong: wordList);
    }
}