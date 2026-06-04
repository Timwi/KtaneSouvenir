using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotMorseCode
{
    [Question("Which of these words was transmitted in {0}?", ThreeColumns6Answers, "shelf", "twine", "null", "drive", "shell", "year", "shall", "pet", "pounds", "possum", "honey", "eggplant", "hive", "brother", "query", "sister", "ying", "pit", "guidance", "anew", "yeast", "coolant", "beef", "pence", "swine", "yang", "shill")]
    Words
}

public partial class SouvenirModule
{
    [Handler("NotMorseCode", "Not Morse Code", typeof(SNotMorseCode), "Andrio Celos")]
    [ManualQuestion("What were the transmitted words?")]
    private IEnumerator<SouvenirInstruction> ProcessNotMorseCode(ModuleData module)
    {
        var component = GetComponent(module, "NotMorseCode");
        yield return WaitForSolve;

        var words = GetArrayField<string>(component, "words").Get();
        
        yield return question(SNotMorseCode.Words).Answers(words);
    }
}
